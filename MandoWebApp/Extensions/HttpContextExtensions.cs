namespace MandoWebApp.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetLang(this HttpContext context) => 
            context.Request.Headers["x-language"].FirstOrDefault()?.ToLower() ?? "hu";
    }
}
