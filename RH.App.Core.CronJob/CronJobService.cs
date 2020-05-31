using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Cronos;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RH.App.Common.Extensions;
using RH.App.Infrastructure.Data.Modules.CronJobModule;

namespace RH.App.Core.CronJob
{
    public abstract class CronJobService : BackgroundService
    {
        public string CronJobName => this.GetType().Name;

        private static Dictionary<string, CronJobSetting?> SettingMap { get; } = new Dictionary<string, CronJobSetting?>();

        private System.Timers.Timer? cronTimer;
        private System.Timers.Timer? settingTimer;

        private readonly IServiceScopeFactory scopeFactory;
        protected CronJobSetting? CurrentSetting = null;

        protected abstract string DefaultExpression { get; }

        protected CronJobService([NotNull] IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this.RefreshSetting(stoppingToken);
            await this.ScheduleJob(stoppingToken);
        }

        protected abstract Task DoWork(IServiceScope scope, CancellationToken cancellationToken);

        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            this.cronTimer?.Stop();
            this.LoadSetting();

            var expression = CronExpression.Parse(this.CurrentSetting?.Expression ?? this.DefaultExpression, CronFormat.IncludeSeconds);
            var offset = DateTimeExt.OffsetNow();

            var next = expression.GetNextOccurrence(offset, TimeZoneInfo.Local);
            if (next.HasValue)
            {
                var delay = next.Value - offset;

                this.cronTimer = new System.Timers.Timer(delay.TotalMilliseconds) { AutoReset = false };
                this.cronTimer.Elapsed += async (sender, args) =>
                {
                    if (this.CurrentSetting?.Enabled ?? true)
                    {
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            using var scope = this.scopeFactory.CreateScope();
                            await DoWork(scope, cancellationToken);
                        }

                        if (!cancellationToken.IsCancellationRequested)
                        {
                            await this.ScheduleJob(cancellationToken);
                        }
                    }
                };

                this.cronTimer.Start();
            }

            await Task.CompletedTask;
        }

        private void LoadSetting()
        {
            this.CurrentSetting = GetSetting(CronJobName);
        }

        private Task RefreshSetting(CancellationToken cancellationToken)
        {
            this.settingTimer?.Stop();

            var expression = CronExpression.Parse("*/5 * * * * *", CronFormat.IncludeSeconds);
            var offset = DateTimeExt.OffsetNow();

            var next = expression.GetNextOccurrence(offset, TimeZoneInfo.Local);
            if (next.HasValue)
            {
                var delay = next.Value - offset;
                this.settingTimer = new System.Timers.Timer(delay.TotalMilliseconds) { AutoReset = false };
                this.settingTimer.Elapsed += async (sender, args) =>
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        var newSetting = GetSetting(this.CronJobName);
                        if ((this.CurrentSetting?.Enabled ?? true) != (newSetting?.Enabled ?? true) || (this.CurrentSetting?.Expression ?? this.DefaultExpression) != (newSetting?.Expression ?? this.DefaultExpression))
                        {
                            await this.ResetCron(cancellationToken);
                        }

                        if (!cancellationToken.IsCancellationRequested)
                        {
                            await this.RefreshSetting(cancellationToken);
                        }
                    }
                };

                this.settingTimer.Start();
            }

            return Task.CompletedTask;
        }

        private async Task ResetCron(CancellationToken cancellationToken)
        {
            this.cronTimer?.Stop();
            await this.ScheduleJob(cancellationToken);
        }

        protected static CronJobSetting? GetSetting([NotNull] string cronJobName)
        {
            SettingMap.TryGetValue(cronJobName, out CronJobSetting? setting);
            return setting;
        }

        protected static void SetSetting([NotNull] string cronJobName, CronJobSetting? setting)
        {
            SettingMap.AddOrUpdate(cronJobName, setting);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.cronTimer?.Stop();
            this.settingTimer?.Stop();

            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            this.cronTimer?.Dispose();
            this.settingTimer?.Dispose();

            base.Dispose();
        }
    }
}
