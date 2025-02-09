using AspNetCoreHero.ToastNotification.Abstractions;
using Cht.HMS.Web.UI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.UI.Controllers
{
    public class LabTechnicianDashBoardController : Controller
    {
        private readonly ILabTestService _labTestService;
        private readonly INotyfService _notyfService;
        public LabTechnicianDashBoardController(ILabTestService labTestService,
          INotyfService notyfService)
        {
            _labTestService = labTestService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetLabOrders()
        {

            try
            {
                var responce = await _labTestService.GetLabOrdersAsync();

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
