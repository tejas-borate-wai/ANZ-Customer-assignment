using System.Threading.Tasks;
using Abp.Application.Services;
using MyTraining1101Demo.Editions.Dto;
using MyTraining1101Demo.MultiTenancy.Dto;

namespace MyTraining1101Demo.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}