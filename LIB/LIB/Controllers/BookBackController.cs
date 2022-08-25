using Microsoft.AspNetCore.Mvc;

namespace LIB.Controllers
{
    public class BookBackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
