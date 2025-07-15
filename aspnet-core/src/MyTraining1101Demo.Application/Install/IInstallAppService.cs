using System.Threading.Tasks;
using Abp.Application.Services;
using MyTraining1101Demo.Install.Dto;

namespace MyTraining1101Demo.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}