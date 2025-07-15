using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MyTraining1101Demo.EntityFrameworkCore;

namespace MyTraining1101Demo.HealthChecks
{
    public class MyTraining1101DemoDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public MyTraining1101DemoDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("MyTraining1101DemoDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("MyTraining1101DemoDbContext could not connect to database"));
        }
    }
}
