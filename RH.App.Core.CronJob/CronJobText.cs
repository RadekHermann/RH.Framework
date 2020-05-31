using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RH.App.Core.CronJob
{
    public class CronJobText : CronJobService
    {
        private readonly ILogger<CronJobText> logger;

        public CronJobText([NotNull] IServiceScopeFactory scopeFactory, ILogger<CronJobText> logger) : base(scopeFactory)
        {
            this.logger = logger;
        }

        protected override string DefaultExpression => "*/10 * * * * *";

        protected override Task DoWork(IServiceScope scope, CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"{DateTime.Now:HH:mm:ss}");
            return Task.CompletedTask;
        }
    }
}
