using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using System;

namespace FileServerPlus.Mvc
{
    public class FileServerVersionProvider : IFileVersionProvider
    {
        public string AddFileVersionToPath(PathString requestPathBase, string path)
        {
            var resolvedPath = path;

            var queryStringOrFragmentStartIndex = path.IndexOfAny(QueryStringAndFragmentTokens);
            if (queryStringOrFragmentStartIndex != -1)
            {
                resolvedPath = path[..queryStringOrFragmentStartIndex];
            }

            if (Uri.TryCreate(resolvedPath, UriKind.Absolute, out var uri) && !uri.IsFile)
            {
                return path;
            }

            if (!_cache.TryGetValue(path, out string value))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions();
                cacheEntryOptions.AddExpirationToken(FileProvider.Watch(resolvedPath));

                var fileInfo = FileServerRegister.Instance.GetFile(resolvedPath);

                if (!fileInfo.Exists &&
                    requestPathBase.HasValue &&
                    resolvedPath.StartsWith(requestPathBase.Value, StringComparison.OrdinalIgnoreCase))
                {
                    var requestPathBaseRelativePath = resolvedPath[requestPathBase.Value.Length..];
                    cacheEntryOptions.AddExpirationToken(FileProvider.Watch(requestPathBaseRelativePath));
                    fileInfo = FileProvider.GetFileInfo(requestPathBaseRelativePath);
                }

                value = fileInfo.Exists ? QueryHelpers.AddQueryString(path, VersionKey, GetHashForFile(fileInfo)) : path;

                value = _cache.Set(path, value, cacheEntryOptions);
            }

            return value;
        }

        private const string VersionKey = "v";

        private static string GetHashForFile(IFileInfo fileInfo)
        {
            return "abc";
        }

        public IFileProvider FileProvider { get; }

        private static readonly char[] QueryStringAndFragmentTokens = { '?', '#' };

        private readonly IMemoryCache _cache;

        public FileServerVersionProvider(IFileProvider fileProvider, IMemoryCache cache)
        {
            FileProvider = fileProvider;
            _cache = cache;
        }
    }
}