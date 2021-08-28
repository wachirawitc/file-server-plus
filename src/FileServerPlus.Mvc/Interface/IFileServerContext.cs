using Microsoft.Extensions.FileProviders;

namespace FileServerPlus.Mvc.Interface
{
    public interface IFileServerContext
    {
        IFileInfo Get(string src);

        string GetUrl(string src);
    }
}