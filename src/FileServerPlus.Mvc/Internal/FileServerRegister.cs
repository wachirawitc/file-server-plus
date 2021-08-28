using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileServerPlus.Mvc.Internal
{
    internal sealed class FileServerRegister
    {
        public void Add(string serverId, FileServerOptions fileServerOptions)
        {
            _fileServerOptions.Add(new FileServerConfiguration
            {
                ServerId = serverId,
                Options = fileServerOptions
            });
        }

        public FileServerConfiguration GetDefaultServer()
        {
            return _fileServerOptions.FirstOrDefault();
        }

        public FileServerConfiguration GetServer(string serverId)
        {
            if (string.IsNullOrWhiteSpace(serverId))
            {
                return null;
            }

            var configuration = _fileServerOptions.FirstOrDefault(x => string.Equals(x.ServerId, serverId, StringComparison.CurrentCultureIgnoreCase));
            return configuration;
        }

        public bool IsExistsRequestPath(string requestPath)
        {
            return _fileServerOptions.Any(x => x.Options.RequestPath == requestPath);
        }

        public bool IsExistsServerId(string fileServerId)
        {
            return _fileServerOptions.Any(x => x.ServerId == fileServerId);
        }

        private static IEnumerable<string> GetPaths(string absoluteUrl)
        {
            absoluteUrl = absoluteUrl.Replace("~", string.Empty);
            var paths = absoluteUrl.Split('/')
                .Where(x => string.IsNullOrWhiteSpace(x) == false);

            return paths;
        }

        public static FileServerRegister Instance => _instance ??= new FileServerRegister();

        private static FileServerRegister _instance;

        private readonly List<FileServerConfiguration> _fileServerOptions;

        private FileServerRegister()
        {
            _fileServerOptions = new List<FileServerConfiguration>();
        }
    }
}