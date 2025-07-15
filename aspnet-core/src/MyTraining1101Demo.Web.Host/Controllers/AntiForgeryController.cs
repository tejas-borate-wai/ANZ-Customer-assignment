using Microsoft.AspNetCore.Antiforgery;

namespace MyTraining1101Demo.Web.Controllers
{
    public class AntiForgeryController : MyTraining1101DemoControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
