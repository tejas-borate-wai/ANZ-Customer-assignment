using System.Threading.Tasks;
using MyTraining1101Demo.Authorization.Users;

namespace MyTraining1101Demo.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
