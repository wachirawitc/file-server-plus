using Microsoft.Extensions.FileProviders;
using System.IO;

namespace FileServerPlus.Mvc.Interface
{
    public interface IFileServerPlusContext
    {
        IFileInfo Get(string src);

        IFileInfo Get(string serverId, string src);

        string GetUrl(string src);

        string GetUrl(string serverId, string src);

        DirectoryInfo GetWorkingDirectory();

        DirectoryInfo GetWorkingDirectory(string serverId);
    }
}