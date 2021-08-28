namespace FileServerPlus.Mvc.Internal
{
    internal class UrlBuilder
    {
        public string GetSubPath()
        {
            var source = _src;

            if (string.IsNullOrWhiteSpace(source))
            {
                return source;
            }

            if (source.StartsWith("~/"))
            {
                source = source.Replace("~/", "/");
            }

            if (source.StartsWith("/") == false)
            {
                source = $"/{source}";
            }

            return source;
        }

        public string GetUrl()
        {
            if (string.IsNullOrWhiteSpace(_src))
            {
                return _src;
            }

            return $"{_configuration.Options.RequestPath}{GetSubPath()}";
        }

        private readonly FileServerConfiguration _configuration;

        private readonly string _src;

        public UrlBuilder(string src, FileServerConfiguration configuration)
        {
            _src = src;
            _configuration = configuration;
        }
    }
}