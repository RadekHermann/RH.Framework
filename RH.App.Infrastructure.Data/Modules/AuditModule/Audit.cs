using System;

namespace RH.App.Infrastructure.Data.Modules.AuditModule
{
    public class Audit : IEntity
    {
        public Guid Id { get; set; }

        public string AuditType { get; set; } = null!;

        public string TableName { get; set; } = null!;

        public string KeyValues { get; set; } = null!;

        public string? OldValues { get; set; }
        public string? NewValues { get; set; }

        public string? ChangedColumns { get; set; }

        public DateTime Action { get; set; }
        public string ActionBy { get; set; } = null!;
    }

    public enum AuditType
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }
}
