using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace FileServerPlus.Mvc.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseFileServerPlus(this IApplicationBuilder app, DirectoryInfo physicalDirectory, string requestPath)
        {
            UseFileServerPlus(app, physicalDirectory, requestPath, false);
        }

        public static void UseFileServerPlus(this IApplicationBuilder app, DirectoryInfo physicalDirectory, string requestPath, bool enableDirectoryBrowsing)
        {
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

            var fileProvider = new PhysicalFileProvider(physicalDirectory.FullName);

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = fileProvider,
                RequestPath = requestPath,
                EnableDirectoryBrowsing = enableDirectoryBrowsing
            });
        }
    }
}