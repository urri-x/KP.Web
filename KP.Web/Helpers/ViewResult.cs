using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace KP.WebApi.Helpers
{
	public class ViewResult : IViewResult
	{
		private readonly RazorTemplateService razorTemplateService;
		private readonly HttpRequestMessage requestMessage;
		private readonly string viewVirtualPath;

		public ViewResult(RazorTemplateService razorTemplateService, [NotNull] HttpRequestMessage requestMessage, [NotNull] string viewVirtualPath)
		{
			this.razorTemplateService = razorTemplateService;
			this.requestMessage = requestMessage;
			this.viewVirtualPath = viewVirtualPath;
		}

		[NotNull]
		public string ViewVirtualPath
		{
			get { return viewVirtualPath; }
		}

		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			var result = razorTemplateService.GetResult(requestMessage, viewVirtualPath);
			var response = requestMessage.CreateResponse();
			response.Content = new StringContent(result, Encoding.UTF8, "text/html");
			return Task.FromResult(response);
		}
	}

	public class ViewResult<T> : IViewResult
	{
		private readonly RazorTemplateService razorTemplateService;
		private readonly HttpRequestMessage requestMessage;
		private readonly string viewVirtualPath;
		private readonly T model;

		public ViewResult(RazorTemplateService razorTemplateService, [NotNull] HttpRequestMessage requestMessage, [NotNull] string viewVirtualPath, [NotNull] T model)
		{
			this.razorTemplateService = razorTemplateService;
			this.requestMessage = requestMessage;
			this.viewVirtualPath = viewVirtualPath;
			this.model = model;
		}

		[NotNull]
		public string ViewVirtualPath
		{
			get { return viewVirtualPath; }
		}

		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			var result = razorTemplateService.GetResult(requestMessage, viewVirtualPath, model);
			var response = requestMessage.CreateResponse();
			response.Content = new StringContent(result, Encoding.UTF8, "text/html");
			return Task.FromResult(response);
		}
	}
}
