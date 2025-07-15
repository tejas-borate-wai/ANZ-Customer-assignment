using System.ComponentModel.DataAnnotations;

namespace MyTraining1101Demo.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}