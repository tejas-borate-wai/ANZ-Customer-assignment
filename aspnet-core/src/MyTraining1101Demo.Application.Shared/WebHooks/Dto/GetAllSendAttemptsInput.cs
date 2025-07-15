using MyTraining1101Demo.Dto;

namespace MyTraining1101Demo.WebHooks.Dto
{
    public class GetAllSendAttemptsInput : PagedInputDto
    {
        public string SubscriptionId { get; set; }
    }
}
