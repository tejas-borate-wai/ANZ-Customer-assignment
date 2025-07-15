using System.Threading.Tasks;
using Abp.Application.Services;
using MyTraining1101Demo.Sessions.Dto;

namespace MyTraining1101Demo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
