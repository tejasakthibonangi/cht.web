using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorManager _doctorManager;


        private readonly IPatientManager _patientManager;

        public DoctorController(IDoctorManager doctorManager,
            IPatientManager patientManager)
        {
            _doctorManager = doctorManager;
            _patientManager = patientManager;
        }


        [HttpGet]
        [Route("GetDoctorByIdAsync/{doctorId}")]
        public async Task<IActionResult> GetDoctorByIdAsync(Guid doctorId)
        {
            try
            {
                var responce = await _doctorManager.GetDoctorByIdAsync(doctorId);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        [Route("GetDoctorsAsync")]
        public async Task<IActionResult> GetDoctorsAsync()
        {
            try
            {
                var responce = await _doctorManager.GetDoctorsAsync();

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateDoctorAsync")]
        public async Task<IActionResult> InsertOrUpdateDoctorAsync(Doctor doctor)
        {
            try
            {
                var responce = await _doctorManager.InsertOrUpdateDoctorAsync(doctor);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
