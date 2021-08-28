using System.IO;

namespace FileServerPlus.Mvc
{
    public class FileServerConfiguration
    {
        public string AbsoluteDirectory { get; set; }

        public string AbsoluteFile { get; set; }

        public FileInfo File { get; set; }
    }
}