using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.UI.Controllers
{
    public class DoctorDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
