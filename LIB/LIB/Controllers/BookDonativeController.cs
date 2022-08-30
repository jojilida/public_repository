using Microsoft.AspNetCore.Mvc;

namespace LIB.Controllers
{
    public class BookDonativeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
