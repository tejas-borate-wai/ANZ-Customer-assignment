using System.Threading.Tasks;
using Abp.Webhooks;

namespace MyTraining1101Demo.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
