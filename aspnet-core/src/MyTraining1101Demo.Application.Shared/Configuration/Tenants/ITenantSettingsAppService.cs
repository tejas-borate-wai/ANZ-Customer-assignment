using System.Threading.Tasks;
using Abp.Application.Services;
using MyTraining1101Demo.Configuration.Tenants.Dto;

namespace MyTraining1101Demo.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
