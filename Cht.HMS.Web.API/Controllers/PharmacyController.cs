using Cht.HMS.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyManager _pharmacyManager;
        public PharmacyController(IPharmacyManager pharmacyManager)
        {
            _pharmacyManager = pharmacyManager;
        }
        [HttpGet]
        [Route("GetPharmacysAsync")]
        public async Task<IActionResult> GetPharmacysAsync()
        {
            try
            {
                var responce = await _pharmacyManager.GetPharmacysAsync();

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetPharmacyOrdersAsync")]
        public async Task<IActionResult> GetPharmacyOrdersAsync()
        {
            try
            {
                var responce = await _pharmacyManager.GetPatientPharmacyOrderAsync();

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
