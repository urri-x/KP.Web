using System.Web.Http;
using JetBrains.Annotations;

namespace KP.WebApi.Helpers
{
    public abstract class ApiViewController : ApiController
    {
        [NotNull]
        protected IViewResult View([NotNull] string viewVirtualPath)
        {
            var razorTemplateService = new RazorTemplateService();
            return new ViewResult(razorTemplateService, Request, viewVirtualPath);
        }

        [NotNull]
        protected IViewResult View<T>([NotNull] string viewVirtualPath, [NotNull] T model)
        {
            var razorTemplateService = new RazorTemplateService();
            return new ViewResult<T>(razorTemplateService, Request, viewVirtualPath, model);
        }
    }
}