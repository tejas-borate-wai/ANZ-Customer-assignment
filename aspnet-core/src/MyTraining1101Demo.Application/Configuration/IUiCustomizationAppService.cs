using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using MyTraining1101Demo.Configuration.Dto;

namespace MyTraining1101Demo.Configuration
{
    public interface IUiCustomizationSettingsAppService : IApplicationService
    {
        Task<List<ThemeSettingsDto>> GetUiManagementSettings();

        Task UpdateUiManagementSettings(ThemeSettingsDto settings);

        Task UpdateDefaultUiManagementSettings(ThemeSettingsDto settings);

        Task UseSystemDefaultSettings();
    }
}
