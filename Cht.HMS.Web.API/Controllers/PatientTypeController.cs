using Cht.HMS.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientTypeController : ControllerBase
    {
        private readonly IPatientTypeManager _patientTypeManager;
        public PatientTypeController(IPatientTypeManager patientTypeManager)
        {
            _patientTypeManager = patientTypeManager;
        }

        [HttpGet]
        [Route("GetPatientTypesAsync")]
        public async Task<IActionResult> GetPatientTypesAsync()
        {
            try
            {
                var responce = await _patientTypeManager.GetPatientTypesAsync();

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
