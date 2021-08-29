using FileServerPlus.Mvc.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FileServerPlus.Mvc.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddFileServerPlus(this IServiceCollection services)
        {
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddTransient<IFileServerPlusContext, FileServerPlusContext>();
        }
    }
}