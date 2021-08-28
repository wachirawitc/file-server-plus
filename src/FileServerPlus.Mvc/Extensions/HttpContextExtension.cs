using Microsoft.AspNetCore.Http;

namespace FileServerPlus.Mvc.Extensions
{
    internal static class HttpContextExtension
    {
        public static T Resolving<T>(this HttpContext context) where T : class
        {
            return context.RequestServices.GetService(typeof(T)) as T;
        }
    }
}