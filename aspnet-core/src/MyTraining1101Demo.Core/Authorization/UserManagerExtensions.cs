using System.Threading.Tasks;
using Abp.Authorization.Users;
using MyTraining1101Demo.Authorization.Users;

namespace MyTraining1101Demo.Authorization
{
    public static class UserManagerExtensions
    {
        public static async Task<User> GetAdminAsync(this UserManager userManager)
        {
            return await userManager.FindByNameAsync(AbpUserBase.AdminUserName);
        }
    }
}
