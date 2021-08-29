using Microsoft.Extensions.FileProviders;

namespace FileServerPlus.Mvc.Interface
{
    public interface IFileServerPlusContext
    {
        IFileInfo Get(string src);

        IFileInfo Get(string serverId, string src);

        string GetUrl(string src);

        string GetUrl(string serverId, string src);
    }
}