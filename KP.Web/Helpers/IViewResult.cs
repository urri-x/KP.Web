using System.Web.Http;
namespace KP.WebApi.Helpers
{
    public interface IViewResult : IHttpActionResult
    {
        string ViewVirtualPath { get; }
    }
}