using System.Threading.Tasks;
using Abp.Application.Services;

namespace MyTraining1101Demo.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
