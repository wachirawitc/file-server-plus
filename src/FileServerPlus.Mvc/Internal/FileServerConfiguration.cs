using Microsoft.AspNetCore.Builder;

namespace FileServerPlus.Mvc.Internal
{
    internal class FileServerConfiguration
    {
        public string ServerId { get; set; }

        public FileServerOptions Options { get; set; }
    }
}