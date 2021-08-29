using Microsoft.AspNetCore.Http;

namespace FileServerPlus.Mvc.Internal
{
    internal class UrlBuilder
    {
        private const string Prefix = "/";

        private const string TildePrefix = "~/";

        public string GetSubPath()
        {
            var source = _src;

            if (string.IsNullOrWhiteSpace(source))
            {
                return source;
            }

            if (source.StartsWith(TildePrefix))
            {
                var from = TildePrefix.Length;
                var to = source.Length - from;
                source = source.Substring(from, to);
            }

            if (source.StartsWith(Prefix) == false)
            {
                source = $"{Prefix}{source}";
            }

            if (string.IsNullOrWhiteSpace(PathBase) == false &&
                source.StartsWith(PathBase))
            {
                var from = PathBase.Length;
                var to = source.Length - from;
                source = source.Substring(from, to);
            }

            return source;
        }

        public string GetUrl()
        {
            if (string.IsNullOrWhiteSpace(_src))
            {
                return _src;
            }

            if (string.IsNullOrWhiteSpace(PathBase) == false)
            {
                return $"{PathBase}{_configuration.Options.RequestPath}{GetSubPath()}";
            }

            return $"{_configuration.Options.RequestPath}{GetSubPath()}";
        }

        private string PathBase => _httpContext.Request.PathBase.ToString();

        private readonly FileServerConfiguration _configuration;

        private readonly HttpContext _httpContext;

        private readonly string _src;

        public UrlBuilder(string src, FileServerConfiguration configuration, HttpContext httpContext)
        {
            _src = src;
            _configuration = configuration;
            _httpContext = httpContext;
        }
    }
}