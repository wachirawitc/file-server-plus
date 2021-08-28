using FileServerPlus.Mvc.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FileServerPlus.Mvc.Extensions
{
    public static class UrlHelperExtension
    {
        public static string FileServerContent(this IUrlHelper url, string src)
        {
            var options = FileServerRegister.Instance.GetOption(src);
            if (options != null)
            {
                var context = url.ActionContext.HttpContext;
                var cache = context.Resolving<IMemoryCache>();
                var fileVersionProvider = new FileServerVersionProvider(options.RequestPath, options.FileProvider, cache);

                var path = fileVersionProvider.AddFileVersionToPath(context.Request.PathBase, src);
                return path;
            }

            return src;
        }
    }
}