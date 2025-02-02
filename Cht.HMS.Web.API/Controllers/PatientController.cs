using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientManager _patientManager;
        public PatientController(IPatientManager patientManager)
        {
            _patientManager = patientManager;
        }

        [HttpGet]
        [Route("GetPatientRegistrationsAsync")]
        public async Task<IActionResult> GetPatientRegistrationsAsync()
        {
            try
            {
                // Calling the service method to fetch all patient registrations
                var response = await _patientManager.GetPatientRegistrationsAsync();

                // Returning a successful response with the fetched patient registrations
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Returning an internal server error with the exception message
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("SearchPatientRegistrationsAsync/{inputString}")]
        public async Task<IActionResult> SearchPatientRegistrationsAsync(string inputString)
        {
            try
            {
                // Calling the service method to fetch patient registrations filtered by the input string
                var response = await _patientManager.GetPatientRegistrationsAsync(inputString);

                // Returning a successful response with the filtered patient registrations
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Returning an internal server error with the exception message
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdatePatientRegistrationAsync")]
        public async Task<IActionResult> InsertOrUpdatePatientRegistrationAsync(PatientRegistration registration)
        {
            try
            {
                // Calling the service method to insert or update the patient registration
                var response = await _patientManager.PatientRegistrationAsync(registration);

                // Returning a successful response with the inserted or updated patient registration data
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Returning an internal server error with the exception message
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
