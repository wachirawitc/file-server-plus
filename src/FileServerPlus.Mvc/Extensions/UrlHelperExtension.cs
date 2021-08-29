using FileServerPlus.Mvc.Internal;
using FileServerPlus.Mvc.Internal.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FileServerPlus.Mvc.Extensions
{
    public static class UrlHelperExtension
    {
        public static string FileServerContent(this IUrlHelper url, string src)
        {
            var configuration = FileServerRegister.Instance.GetDefaultServer();
            if (configuration != null)
            {
                var context = url.ActionContext.HttpContext;
                var cache = context.Resolving<IMemoryCache>();
                var fileVersionProvider = new FileServerVersionProvider(configuration, context, cache);

                var path = fileVersionProvider.AddFileVersionToPath(context.Request.PathBase, src);
                return path;
            }

            return src;
        }

        public static string FileServerContent(this IUrlHelper url, string serverId, string src)
        {
            var configuration = FileServerRegister.Instance.GetServer(serverId);
            if (configuration != null)
            {
                var context = url.ActionContext.HttpContext;
                var cache = context.Resolving<IMemoryCache>();
                var fileVersionProvider = new FileServerVersionProvider(configuration, context, cache);

                var path = fileVersionProvider.AddFileVersionToPath(context.Request.PathBase, src);
                return path;
            }

            return src;
        }
    }
}