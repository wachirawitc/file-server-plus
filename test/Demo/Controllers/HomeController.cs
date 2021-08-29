using FileServerPlus.Mvc.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var fileInfo1 = _fileServerPlusContext.Get(@"/cat.jpg");
            var fileInfo2 = _fileServerPlusContext.Get(@"~/cat.jpg");
            return View();
        }

        private readonly IFileServerPlusContext _fileServerPlusContext;

        public HomeController(IFileServerPlusContext fileServerPlusContext)
        {
            _fileServerPlusContext = fileServerPlusContext;
        }
    }
}