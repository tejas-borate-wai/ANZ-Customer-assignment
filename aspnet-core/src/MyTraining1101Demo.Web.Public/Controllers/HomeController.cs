using Microsoft.AspNetCore.Mvc;
using MyTraining1101Demo.Web.Controllers;

namespace MyTraining1101Demo.Web.Public.Controllers
{
    public class HomeController : MyTraining1101DemoControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}