namespace FileServerPlus.Mvc.Internal
{
    internal class ConfigurationSection
    {
        public string ServerId { get; set; }

        public string RootDirectory { get; set; }

        public string RequestPath { get; set; }

        public bool EnableDirectoryBrowsing { get; set; }
    }
}