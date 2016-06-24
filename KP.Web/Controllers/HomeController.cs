using KP.WebApi.Helpers;

namespace KP.WebApi.Controllers
{
    public class HomeController : ApiViewController
    {
        public IViewResult Get()
        {
            return View("~/Views/Home.cshtml");
        }
    }
}
