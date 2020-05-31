using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using RH.App.Infrastructure.Data;
using RH.App.Infrastructure.Data.Modules.AuditModule;
using RH.App.Infrastructure.Data.Modules.AuditModule.Attributes;
using RH.App.Infrastructure.Data.Modules.CronJobModule;
using RH.App.Infrastructure.Data.Modules.SolfDeleteModule;
using RH.App.Infrastructure.Extensions;

namespace RH.App.Infrastructure
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        public DbSet<Audit> Audits { get; set; } = null!;
        public DbSet<CronJobSetting> CronJobSettings { get; set; } = null!;

        public UnitOfWork([NotNull] DbContextOptions<UnitOfWork> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);

                if (entityType.ClrType is ISoftDeleteEntity softDelete)
                {
                    modelBuilder.Entity(entityType.ClrType).AddQueryFilter<ISoftDeleteEntity>(f => !f.IsDeleted);
                }
            }
        }

        public async Task<int> SaveChangesAsync([NotNull] string actionBy, CancellationToken cancellationToken)
        {
            var auditEntries = this.OnBeforeSaveChanges(actionBy);
            var result = await this.SaveChangesAsync(cancellationToken);
            await this.OnAfterSaveChanges(auditEntries, cancellationToken);

            return result;
        }

        private IEnumerable<AuditEntry> OnBeforeSaveChanges([NotNull] string actionBy)
        {
            ChangeTracker.DetectChanges();

            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Unchanged || entry.State == EntityState.Detached)
                    continue;

                if (entry.Entity is IAuditableEntity auditable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditable.Created = DateTime.Now;
                            auditable.CreatedBy = actionBy;
                            break;
                        case EntityState.Modified:
                            auditable.Edited = DateTime.Now;
                            auditable.EditedBy = actionBy;
                            break;
                    }
                }

                bool tmpSoftDelete = false;

                if (entry.Entity is ISoftDeleteEntity softDelete)
                {
                    if (entry.State == EntityState.Deleted)
                    {
                        tmpSoftDelete = true;

                        entry.State = EntityState.Modified;
                        softDelete.IsDeleted = true;
                        softDelete.Deleted = DateTime.Now;
                        softDelete.DeletedBy = actionBy;
                    }
                }

                if (entry.Entity.GetType().GetCustomAttribute<IgnoreAuditAttribute>() == null)
                {
                    var auditEntry = new AuditEntry(entry)
                    {
                        ActionBy = actionBy
                    };

                    auditEntries.Add(auditEntry);

                    foreach (var property in entry.Properties)
                    {
                        if (property.IsTemporary)
                        {
                            auditEntry.TemporaryProperties.Add(property);
                            continue;
                        }

                        string propertyName = property.Metadata.Name;
                        if (property.Metadata.IsPrimaryKey())
                        {
                            auditEntry.KeyValues[propertyName] = property.CurrentValue;
                            continue;
                        }

                        if (property.Metadata.PropertyInfo.GetCustomAttribute<IgnoreAuditAttribute>() == null)
                        {
                            switch (entry.State)
                            {
                                case EntityState.Added:
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                    auditEntry.AuditType = AuditType.Create;
                                    break;
                                case EntityState.Modified:
                                    if (property.IsModified)
                                    {
                                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                                        auditEntry.AuditType = tmpSoftDelete ? AuditType.Delete : AuditType.Update;

                                        auditEntry.ChangedColumns.Add(propertyName);
                                    }
                                    break;
                                case EntityState.Deleted:
                                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                                    auditEntry.AuditType = AuditType.Delete;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                this.Audits.AddRange(auditEntries.Where(w => !w.HasTemporaryProperties).Select(s => s.ToAudit()));
                return auditEntries.Where(w => w.HasTemporaryProperties);
            }

            return auditEntries;
        }

        public async Task OnAfterSaveChanges([NotNull] IEnumerable<AuditEntry> auditEntries, CancellationToken cancellationToken)
        {
            if (!auditEntries.Any())
                return;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var property in auditEntry.TemporaryProperties)
                {
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[property.Metadata.Name] = property.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[property.Metadata.Name] = property.CurrentValue;
                    }
                }
            }

            this.Audits.AddRange(auditEntries.Select(s => s.ToAudit()));
            await this.SaveChangesAsync(cancellationToken);
        }
    }
}
