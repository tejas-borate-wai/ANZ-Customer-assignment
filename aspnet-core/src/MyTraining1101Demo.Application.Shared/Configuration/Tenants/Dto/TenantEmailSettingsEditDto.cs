using Abp.Auditing;
using MyTraining1101Demo.Configuration.Dto;

namespace MyTraining1101Demo.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}