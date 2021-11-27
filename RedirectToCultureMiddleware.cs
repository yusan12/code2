using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;

namespace code2
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
            // Your code here...
            System.Diagnostics.Debug.WriteLine(context.Request.Path);

            var isEn = context.Request.Path.StartsWithSegments("/en");
            var isJa = context.Request.Path.StartsWithSegments("/ja");

            if (isEn)
            {
                System.Diagnostics.Debug.WriteLine("This is an ENGLISH page.");
                await _next(context);
            }
            else if (isJa)
            {
                System.Diagnostics.Debug.WriteLine("This is a JAPANESE page.");
                await _next(context);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Unknown language.");
                var icic = System.StringComparison.InvariantCultureIgnoreCase;
                var al = context.Request.GetTypedHeaders().AcceptLanguage;
                foreach (var item in al)
                {
                    System.Diagnostics.Debug.Write(item.Value.Value);
                    System.Diagnostics.Debug.Write(" ");
                    System.Diagnostics.Debug.WriteLine(item.Quality);
                }
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

                context.Response.Redirect("en", false);
            }
        }
    }

}
