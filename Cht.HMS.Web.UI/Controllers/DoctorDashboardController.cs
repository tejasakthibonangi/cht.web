using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.UI.Controllers
{
    public class DoctorDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DoctorConsutation(Guid patientId)
        {
            return View("~/Views/Doctor/DoctorConsutation.cshtml");
        }
    }
}
