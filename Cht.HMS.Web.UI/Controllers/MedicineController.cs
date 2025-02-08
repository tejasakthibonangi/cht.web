using AspNetCoreHero.ToastNotification.Abstractions;
using Cht.HMS.Web.UI.Interfaces;
using Cht.HMS.Web.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Cht.HMS.Web.UI.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IMedicineService _mediicineService;
        private readonly INotyfService _notyfService;
        public MedicineController(IMedicineService medicineService,
            INotyfService notyfService)
        {
            _mediicineService = medicineService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicines()
        {
            try
            {
                var response = await _mediicineService.GetMedicinesAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
