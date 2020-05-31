using System;

namespace RH.App.Infrastructure.Data.Modules.SolfDeleteModule
{
    public interface ISoftDeleteEntity
    {
        bool IsDeleted { get; set; }

        DateTime? Deleted { get; set; }
        string? DeletedBy { get; set; }
    }
}
