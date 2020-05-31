using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;

using Newtonsoft.Json;

namespace RH.App.Infrastructure.Data.Modules.AuditModule
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entityEntry)
        {
            EntityEntry = entityEntry;
        }

        public EntityEntry EntityEntry { get; }

        public AuditType AuditType { get; set; }
        public string ActionBy { get; set; } = null!;

        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();

        public List<string> ChangedColumns { get; } = new List<string>();

        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public Audit ToAudit()
        {
            return new Audit
            {
                Action = DateTime.Now,
                ActionBy = this.ActionBy,
                AuditType = this.AuditType.ToString(),
                ChangedColumns = this.ChangedColumns.Any() ? JsonConvert.SerializeObject(this.ChangedColumns) : null,
                KeyValues = JsonConvert.SerializeObject(this.KeyValues),
                NewValues = this.NewValues.Any() ? JsonConvert.SerializeObject(this.NewValues) : null,
                OldValues = this.OldValues.Any() ? JsonConvert.SerializeObject(this.OldValues) : null,
                TableName = this.EntityEntry.Metadata.GetTableName()
            };
        }
    }
}
