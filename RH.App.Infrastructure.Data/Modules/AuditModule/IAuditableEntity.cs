using System;

namespace RH.App.Infrastructure.Data.Modules.AuditModule
{
    public interface IAuditableEntity
    {
        DateTime? Edited { get; set; }
        string? EditedBy { get; set; }

        DateTime Created { get; set; }
        string CreatedBy { get; set; }
    }
}
