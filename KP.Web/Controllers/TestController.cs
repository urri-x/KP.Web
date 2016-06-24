using KP.WebApi.Helpers;

namespace KP.WebApi.Controllers
{
    public class TestController : ApiViewController
    {
        public IViewResult GetTests()
        {
            return View("~/Views/Test.cshtml");
        }
    }
}
