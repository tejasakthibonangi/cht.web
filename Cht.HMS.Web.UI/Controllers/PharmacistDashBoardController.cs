using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.UI.Controllers
{
    public class PharmacistDashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
