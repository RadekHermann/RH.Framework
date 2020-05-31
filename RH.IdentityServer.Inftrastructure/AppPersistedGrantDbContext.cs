
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;

using Microsoft.EntityFrameworkCore;

namespace RH.IdentityServer.Inftrastructure
{
    public class AppPersistedGrantDbContext : PersistedGrantDbContext<AppPersistedGrantDbContext>
    {
        public AppPersistedGrantDbContext(DbContextOptions<AppPersistedGrantDbContext> options, OperationalStoreOptions storeOptions) : base(options, storeOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }
        }
    }
}
