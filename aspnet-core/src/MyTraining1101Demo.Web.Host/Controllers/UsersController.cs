using Abp.AspNetCore.Mvc.Authorization;
using MyTraining1101Demo.Authorization;
using MyTraining1101Demo.Storage;
using Abp.BackgroundJobs;

namespace MyTraining1101Demo.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}