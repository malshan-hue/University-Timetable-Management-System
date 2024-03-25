using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.AdminPortal.Controllers
{
    [Area("AdminPortal")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
