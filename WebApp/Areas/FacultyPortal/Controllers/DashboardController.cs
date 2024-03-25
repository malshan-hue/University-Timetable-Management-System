using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.FacultyPortal.Controllers
{
    public class DashboardController : Controller
    {
        [Area("FacultyPortal")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
