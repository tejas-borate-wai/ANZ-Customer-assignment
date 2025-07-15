using Abp.Authorization;
using MyTraining1101Demo.Authorization.Roles;
using MyTraining1101Demo.Authorization.Users;

namespace MyTraining1101Demo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
