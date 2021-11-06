using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Yotsuba
{
    public static class RedirectToCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRedirectToCulture(
            this IApplicationBuilder builder)
        {

            return builder.UseMiddleware<RedirectToCultureMiddleware>();

        }
    }

    public sealed class RedirectToCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectToCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var isEn = context.Request.Path.StartsWithSegments("/en");
            var isJa = context.Request.Path.StartsWithSegments("/ja");

            if (isEn || isJa)
            {
                await _next(context);
            }
            else
            {
                var icic = System.StringComparison.InvariantCultureIgnoreCase;

                var acceptLanguageQuery = context
                    .Request
                    .GetTypedHeaders()
                    .AcceptLanguage
                    .OrderByDescending(x => x.Quality.GetValueOrDefault(1));

                foreach (var item in acceptLanguageQuery)
                { 
                    if (item.Value.Value.StartsWith("ja", icic))
                    {
                        context.Response.Redirect("ja", false);
                        return;
                    }
                    else if (item.Value.Value.StartsWith("en", icic))
                    {
                        context.Response.Redirect("en", false);
                        return;
                    }
                }

                // Unknown language: redirect to the English page.
                context.Response.Redirect("en", false);

            }

        }

    }
}