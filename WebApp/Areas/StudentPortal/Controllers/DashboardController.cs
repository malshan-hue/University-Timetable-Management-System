using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.StudentPortal.Controllers
{
    public class DashboardController : Controller
    {
        [Area("StudentPortal")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
