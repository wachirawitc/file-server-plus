using FileServerPlus.Mvc.Interface;
using FileServerPlus.Mvc.Internal.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace FileServerPlus.Mvc.Internal
{
    internal class FileServerPlusContext : IFileServerPlusContext
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

            var context = _httpContextAccessor.HttpContext;
            var url = new UrlBuilder(src, configuration, context);

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
                var fileVersionProvider = new FileServerVersionProvider(configuration, context, cache);

                var path = fileVersionProvider.AddFileVersionToPath(context.Request.PathBase, src);
                return path;
            }

            return null;
        }

        public DirectoryInfo GetWorkingDirectory()
        {
            return GetWorkingDirectory(null);
        }

        public DirectoryInfo GetWorkingDirectory(string serverId)
        {
            var configuration = string.IsNullOrWhiteSpace(serverId) ?
                FileServerRegister.Instance.GetDefaultServer() :
                FileServerRegister.Instance.GetServer(serverId);

            if (configuration?.Options?.FileProvider is not PhysicalFileProvider fileProvider)
            {
                return null;
            }

            return new DirectoryInfo(fileProvider.Root);
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileServerPlusContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}