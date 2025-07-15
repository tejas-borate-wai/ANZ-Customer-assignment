using System.Threading.Tasks;
using MyTraining1101Demo.Sessions.Dto;

namespace MyTraining1101Demo.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
