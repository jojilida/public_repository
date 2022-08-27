using Microsoft.AspNetCore.Mvc;

namespace LIB.Controllers
{
    public class FeedBackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
