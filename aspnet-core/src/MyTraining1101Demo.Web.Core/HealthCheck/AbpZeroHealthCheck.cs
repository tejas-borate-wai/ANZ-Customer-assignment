using Microsoft.Extensions.DependencyInjection;
using MyTraining1101Demo.HealthChecks;

namespace MyTraining1101Demo.Web.HealthCheck
{
    public static class AbpZeroHealthCheck
    {
        public static IHealthChecksBuilder AddAbpZeroHealthCheck(this IServiceCollection services)
        {
            var builder = services.AddHealthChecks();
            builder.AddCheck<MyTraining1101DemoDbContextHealthCheck>("Database Connection");
            builder.AddCheck<MyTraining1101DemoDbContextUsersHealthCheck>("Database Connection with user check");
            builder.AddCheck<CacheHealthCheck>("Cache");

            // add your custom health checks here
            // builder.AddCheck<MyCustomHealthCheck>("my health check");

            return builder;
        }
    }
}
