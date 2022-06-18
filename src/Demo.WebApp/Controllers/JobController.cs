using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApp.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
