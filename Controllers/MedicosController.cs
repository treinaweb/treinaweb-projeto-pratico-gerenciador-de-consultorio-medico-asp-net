using Microsoft.AspNetCore.Mvc;

namespace SisMed.Controllers
{
    public class MedicosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}