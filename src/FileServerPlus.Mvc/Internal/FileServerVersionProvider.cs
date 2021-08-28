using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using System;
using System.Security.Cryptography;

namespace FileServerPlus.Mvc.Internal
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

                var subPath = resolvedPath.Replace(_requestPath, string.Empty);
                var fileInfo = FileProvider.GetFileInfo(subPath);

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
            using var sha256Hash = SHA256.Create();
            using var readStream = fileInfo.CreateReadStream();
            var hash = sha256Hash.ComputeHash(readStream);
            return WebEncoders.Base64UrlEncode(hash);
        }

        private readonly string _requestPath;

        public IFileProvider FileProvider { get; }

        private static readonly char[] QueryStringAndFragmentTokens = { '?', '#' };

        private readonly IMemoryCache _cache;

        public FileServerVersionProvider(string requestPath, IFileProvider fileProvider, IMemoryCache cache)
        {
            _requestPath = requestPath;
            FileProvider = fileProvider;
            _cache = cache;
        }
    }
}