using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;
using System.Threading;
using JetBrains.Annotations;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace KP.WebApi.Helpers
{
	public class RazorTemplateService 
	{
		private readonly ConcurrentDictionary<string, CompiledViewData> compiledViews = new ConcurrentDictionary<string, CompiledViewData>(StringComparer.OrdinalIgnoreCase);
		private readonly Lazy<IRazorEngineService> razor = new Lazy<IRazorEngineService>(CreateRazor, LazyThreadSafetyMode.ExecutionAndPublication);


		[NotNull]
		private static IRazorEngineService CreateRazor()
		{
			return RazorEngineService.Create(new TemplateServiceConfiguration
			{
				BaseTemplateType = typeof(TemplateBase<>)
			});
		}

		[NotNull]
		public string GetResult([NotNull] HttpRequestMessage request, [NotNull] string virtualPath)
		{
			return GetResult(request, virtualPath, null, null);
		}

		[NotNull]
		public string GetResult<T>([NotNull] HttpRequestMessage request, [NotNull] string virtualPath, [NotNull] T model)
		{
			return GetResult(request, virtualPath, typeof(T), model);
		}

		[NotNull]
		private string GetResult([NotNull] HttpRequestMessage request, [NotNull] string virtualPath, [CanBeNull] Type modelType, [CanBeNull] object model)
		{
			var templatePath = ServerPathProvider.MapPath(virtualPath);
			if (templatePath == null)
				throw new InvalidOperationException(string.Format("Couldn't map path {0}", virtualPath));
			templatePath = Path.GetFullPath(templatePath);
			var compiledViewData = compiledViews.GetOrAdd(templatePath, s => new CompiledViewData(razor.Value, s, modelType));
			return compiledViewData.GetResult(request, model);
		}

		private class CompiledViewData
		{
			private readonly IRazorEngineService razor;
			private readonly string templatePath;
			private readonly Type modelType;
			private readonly object locker = new object();
			private DateTime? cachedLastWriteTimeUtc;
			private volatile ITemplateKey templateKey;

			public CompiledViewData([NotNull] IRazorEngineService razor, [NotNull] string templatePath, [CanBeNull] Type modelType)
			{
				this.razor = razor;
				this.templatePath = templatePath;
				this.modelType = modelType;
			}

			[NotNull]
			public string GetResult([NotNull] HttpRequestMessage request, [CanBeNull] object model)
			{
				var lastWriteTimeUtc = File.GetLastWriteTimeUtc(templatePath);
				if (cachedLastWriteTimeUtc != lastWriteTimeUtc)
				{
					lock (locker)
					{
						if (cachedLastWriteTimeUtc != lastWriteTimeUtc)
						{
							var template = File.ReadAllText(templatePath);
							var templateSource = new LoadedTemplateSource(template, templatePath);
							templateKey = new NameOnlyTemplateKey(templatePath + Guid.NewGuid(), ResolveType.Global, null);
							razor.AddTemplate(templateKey, templateSource);
							razor.Compile(templateKey, modelType);
							cachedLastWriteTimeUtc = lastWriteTimeUtc;
						}
					}
				}
				var viewBag = new DynamicViewBag();
				viewBag.AddValue("__request", request);
				return razor.Run(templateKey, modelType, model, viewBag);
			}
		}

		public class TemplateBase : RazorEngine.Templating.TemplateBase
		{
			[NotNull]
			public HttpRequestMessage Request
			{
				get
				{
					var request = ViewBag.__request as HttpRequestMessage;
					if (request == null)
						throw new InvalidOperationException(string.Format("Couldn't get HttpRequestMessage for view {0}", GetType().Name));
					return request;
				}
			}

			public override string ResolveUrl(string path)
			{
				return path.StartsWith("~")
					? Request.BuildExternalUrl(path.Substring(1))
					: path;
			}
		}

		public class TemplateBase<T> : RazorEngine.Templating.TemplateBase<T>
		{
			[NotNull]
			public HttpRequestMessage Request
			{
				get
				{
					var request = ViewBag.__request as HttpRequestMessage;
					if (request == null)
						throw new InvalidOperationException(string.Format("Couldn't get HttpRequestMessage for view {0}", GetType().Name));
					return request;
				}
			}

			public override string ResolveUrl(string path)
			{
				return path.StartsWith("~")
					? Request.BuildExternalUrl(path.Substring(1))
					: path;
			}
		}
	}
}
