using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Publisher
{
	public class RequestBodyInterceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public RequestBodyInterceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			context.Request.EnableRewind();
			await _next(context).ConfigureAwait(false);
		}
	}
}