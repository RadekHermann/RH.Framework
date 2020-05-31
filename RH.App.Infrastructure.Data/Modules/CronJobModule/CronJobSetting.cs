using System;

using RH.App.Infrastructure.Data.Modules.AuditModule;

namespace RH.App.Infrastructure.Data.Modules.CronJobModule
{
    public class CronJobSetting : IEntity, IAuditableEntity
    {
        public Guid Id { get; set; }

        public string SysName { get; set; } = null!;
        public string Expression { get; set; } = null!;

        public string? Name { get; set; }
        public string? Description { get; set; }

        public bool Enabled { get; set; }

        public DateTime? Edited { get; set; }
        public string? EditedBy { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; } = null!;
    }
}
