
using Microsoft.Extensions.DependencyInjection;

namespace RH.App.Core.CronJob.Extensions
{
    public static class IServiceCollectionExt
    {
        public static IServiceCollection RegisterCronJobs(this IServiceCollection services)
        {
            services.AddHostedService<CronJobText>();


            return services;
        }
    }
}
