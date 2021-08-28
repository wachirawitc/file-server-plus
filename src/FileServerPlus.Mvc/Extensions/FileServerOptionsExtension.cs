using Microsoft.AspNetCore.Builder;

namespace FileServerPlus.Mvc.Extensions
{
    internal static class FileServerOptionsExtension
    {
        public static string GetSubPath(this FileServerOptions options, string src)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                return src;
            }

            var subPath = src
                .Replace("~", string.Empty)
                .Replace(options.RequestPath, string.Empty);

            return subPath;
        }
    }
}