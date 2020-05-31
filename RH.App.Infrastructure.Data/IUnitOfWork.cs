using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using RH.App.Infrastructure.Data.Modules.AuditModule;
using RH.App.Infrastructure.Data.Modules.CronJobModule;

namespace RH.App.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync([NotNull] string actionBy, CancellationToken cancellationToken);

        DbSet<Audit> Audits { get; set; }
        DbSet<CronJobSetting> CronJobSettings { get; set; }
    }
}
