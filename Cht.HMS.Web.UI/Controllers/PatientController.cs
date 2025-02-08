using AspNetCoreHero.ToastNotification.Abstractions;
using Cht.HMS.Web.UI.Interfaces;
using Cht.HMS.Web.UI.Models;
using Cht.HMS.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Cht.HMS.Web.UI.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly INotyfService _notyfService;
        public PatientController(IPatientService patientService,
            INotyfService notyfService)
        {
            _patientService = patientService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientRegistrations()
        {
            try
            {
                var response = await _patientService.GetPatientRegistrationsAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPatientDetails(string patientId)
        {
            try
            {
                var response = await _patientService.GetPatientDetailsAsync(Guid.Parse(patientId));
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPatientsByDoctor(string doctorId)
        {
            try
            {
                var response = await _patientService.GetPatientByDoctorAsync(Guid.Parse(doctorId));
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdatePatientRegistration([FromBody] PatientRegistration registration)
        {
            try
            {
                // Call your service method to insert or update the patient registration.
                var response = await _patientService.PatientRegistrationAsync(registration);

                // Return a success response with the data.
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                // Log the error and notify the user.
                _notyfService.Error(ex.Message);

                // Re-throw the exception or return an error response as per your requirement.
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpsertPatientConsultationDetails([FromBody] PatientInformation registration)
        {
            try
            {
                // Call your service method to insert or update the patient registration.
                var response = await _patientService.UpsertPatientConsultationDetailsAsync(registration);

                // Return a success response with the data.
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                // Log the error and notify the user.
                _notyfService.Error(ex.Message);

                // Re-throw the exception or return an error response as per your requirement.
                throw ex;
            }
        }

    }
}
