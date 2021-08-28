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
            return Get(null, src);
        }

        public IFileInfo Get(string serverId, string src)
        {
            var configuration = string.IsNullOrWhiteSpace(serverId) ?
                FileServerRegister.Instance.GetDefaultServer() :
                FileServerRegister.Instance.GetServer(serverId);

            if (configuration == null)
            {
                return null;
            }

            var url = new UrlBuilder(src, configuration);

            var subPath = url.GetSubPath();
            var fileInfo = configuration.Options.FileProvider.GetFileInfo(subPath);
            return fileInfo;
        }

        public string GetUrl(string src)
        {
            return GetUrl(null, src);
        }

        public string GetUrl(string serverId, string src)
        {
            var configuration = string.IsNullOrWhiteSpace(serverId) ?
                FileServerRegister.Instance.GetDefaultServer() :
                FileServerRegister.Instance.GetServer(serverId);

            if (configuration != null)
            {
                var context = _httpContextAccessor.HttpContext;
                var cache = context.Resolving<IMemoryCache>();
                var fileVersionProvider = new FileServerVersionProvider(configuration, cache);

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