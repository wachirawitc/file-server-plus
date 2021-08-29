using Microsoft.AspNetCore.Mvc;

namespace DemoMulti.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}