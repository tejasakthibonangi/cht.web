using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.UI.Controllers
{
    public class ExecutiveDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
