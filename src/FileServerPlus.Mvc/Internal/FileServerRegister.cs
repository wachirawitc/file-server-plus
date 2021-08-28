using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Linq;

namespace FileServerPlus.Mvc.Internal
{
    internal sealed class FileServerRegister
    {
        public void Add(FileServerOptions fileServerOptions)
        {
            _fileServerOptions.Add(fileServerOptions);
        }

        public FileServerOptions GetOption(string absoluteUrl)
        {
            if (string.IsNullOrWhiteSpace(absoluteUrl))
            {
                return null;
            }

            var paths = GetPaths(absoluteUrl);
            var requestPath = $"/{paths.FirstOrDefault()}";
            var option = _fileServerOptions.FirstOrDefault(x => x.RequestPath == requestPath);
            return option;
        }

        public FileServerOptions GetOptionWithRequestPath(string requestPath)
        {
            var option = _fileServerOptions.FirstOrDefault(x => x.RequestPath == requestPath);
            return option;
        }

        public bool IsExistsRequestPath(string requestPath)
        {
            return _fileServerOptions.Any(x => x.RequestPath == requestPath);
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

        private readonly List<FileServerOptions> _fileServerOptions;

        private FileServerRegister()
        {
            _fileServerOptions = new List<FileServerOptions>();
        }
    }
}