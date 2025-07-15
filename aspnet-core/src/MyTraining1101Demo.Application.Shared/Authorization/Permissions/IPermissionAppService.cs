using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyTraining1101Demo.Authorization.Permissions.Dto;

namespace MyTraining1101Demo.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
