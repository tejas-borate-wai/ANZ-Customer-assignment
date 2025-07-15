using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;

namespace MyTraining1101Demo.Configuration.Host.Dto
{
    public class SendTestEmailInput
    {
        [Required]
        [MaxLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}