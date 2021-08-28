using FileServerPlus.Mvc.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace FileServerPlus.Mvc.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseFileServerPlus(this IApplicationBuilder app, string fileServerId, DirectoryInfo physicalDirectory, string requestPath)
        {
            UseFileServerPlus(app, fileServerId, physicalDirectory, requestPath, false);
        }

        public static void UseFileServerPlus(this IApplicationBuilder app, string fileServerId, DirectoryInfo physicalDirectory, string requestPath, bool enableDirectoryBrowsing)
        {
            if (string.IsNullOrWhiteSpace(fileServerId))
            {
                throw new ArgumentNullException(nameof(fileServerId));
            }

            if (physicalDirectory == null)
            {
                throw new ArgumentNullException(nameof(physicalDirectory));
            }

            if (Directory.Exists(physicalDirectory.FullName) == false)
            {
                throw new DirectoryNotFoundException(physicalDirectory.FullName);
            }

            if (string.IsNullOrWhiteSpace(requestPath))
            {
                throw new ArgumentNullException(nameof(requestPath));
            }

            if (FileServerRegister.Instance.IsExistsServerId(fileServerId))
            {
                throw new ArgumentException($"Duplicate file server id '{fileServerId}'");
            }

            if (FileServerRegister.Instance.IsExistsRequestPath(requestPath))
            {
                throw new ArgumentException($"Duplicate request path '{requestPath}'");
            }

            var fileProvider = new PhysicalFileProvider(physicalDirectory.FullName);

            var options = new FileServerOptions();
            options.FileProvider = fileProvider;
            options.RequestPath = requestPath;
            options.EnableDirectoryBrowsing = enableDirectoryBrowsing;

            FileServerRegister.Instance.Add(fileServerId, options);

            app.UseFileServer(options);
        }
    }
}