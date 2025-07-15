using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace MyTraining1101Demo.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
