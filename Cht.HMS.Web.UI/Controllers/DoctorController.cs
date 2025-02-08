using AspNetCoreHero.ToastNotification.Abstractions;
using Cht.HMS.Web.UI.Interfaces;
using Cht.HMS.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.UI.Controllers
{
    public class DoctorController : Controller
    {

        private readonly IDoctorService _doctorService;
        private readonly INotyfService _notyfService;
        public DoctorController(IDoctorService doctorService,
            INotyfService notyfService)
        {
            _doctorService = doctorService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            try
            {
                var doctors = await _doctorService.GetDoctorsAsync();
                return Json(new { data = doctors });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DoctorConsutation(Guid patientId)
        {
            return View();
        }

        // POST: Doctor/InsertOrUpdate
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateDoctor([FromBody] Doctor doctor)
        {
            try
            {
                // Call the service method to insert or update the doctor
                var response = await _doctorService.InsertOrUpdateDoctorAsync(doctor);

                // Return success response with the data
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                // Log error and notify user (using notification service)
                _notyfService.Error(ex.Message);

                // Re-throw or return error as needed
                throw ex;
            }
        }
    }
}
