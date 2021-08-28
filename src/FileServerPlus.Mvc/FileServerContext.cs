using FileServerPlus.Mvc.Extensions;
using FileServerPlus.Mvc.Interface;
using FileServerPlus.Mvc.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;

namespace FileServerPlus.Mvc
{
    public class FileServerContext : IFileServerContext
    {
        public IFileInfo Get(string src)
        {
            var option = FileServerRegister.Instance.GetOption(src);
            if (option == null)
            {
                return null;
            }

            var subPath = src
                .Replace("~", string.Empty)
                .Replace(option.RequestPath, string.Empty);

            var fileInfo = option.FileProvider.GetFileInfo(subPath);

            return fileInfo;
        }

        public string GetUrl(string src)
        {
            var options = FileServerRegister.Instance.GetOption(src);
            if (options != null)
            {
                var context = _httpContextAccessor.HttpContext;
                var cache = context.Resolving<IMemoryCache>();
                var fileVersionProvider = new FileServerVersionProvider(options.RequestPath, options.FileProvider, cache);

                var path = fileVersionProvider.AddFileVersionToPath(context.Request.PathBase, src);
                return path;
            }

            return src;
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileServerContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}