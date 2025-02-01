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

        public DoctorController(IDoctorManager doctorManager)
        {
            _doctorManager = doctorManager;
        }

        [HttpGet]
        [Route("GetDoctorByIdAsync")]
        public async Task<IActionResult> GetDoctorByIdAsync(Guid DoctorId)
        {
            try
            {
                var responce = await _doctorManager.GetDoctorByIdAsync(DoctorId);

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
