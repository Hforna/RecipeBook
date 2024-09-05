using System.Globalization;

namespace ProjectAspNet.Middlewares
{
    public class CultureInfoMiddleware
    {
        private readonly RequestDelegate _next;
        public CultureInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var acceptedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var requestLanguage = context.Request.Headers.AcceptLanguage;
            var currentCulture = new CultureInfo("us");

            if(!string.IsNullOrEmpty(requestLanguage) && acceptedLanguages.Any(x => x.Equals(requestLanguage)))
            {
                currentCulture = new CultureInfo(requestLanguage!);
            }
            CultureInfo.CurrentCulture = currentCulture;
            CultureInfo.CurrentUICulture = currentCulture;

            await _next(context);
        }
    }
}
