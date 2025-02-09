using AspNetCoreHero.ToastNotification.Abstractions;
using Cht.HMS.Web.UI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.UI.Controllers
{
    public class PharmacistDashBoardController : Controller
    {
        private readonly IPharmacyService _pharmacyService;

        private readonly INotyfService _notyfService;
        public PharmacistDashBoardController(IPharmacyService pharmacyService,
            INotyfService notyfService)
        {
            _pharmacyService = pharmacyService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetPatientPharmacyOrder()
        {
            try
            {
                var responce = await _pharmacyService.GetPatientPharmacyOrderAsync();

                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
