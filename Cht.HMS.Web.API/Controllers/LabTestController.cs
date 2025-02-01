using Cht.HMS.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestController : ControllerBase
    {
        private readonly ILabTestsManager _labTestsManager;
        public LabTestController(ILabTestsManager labTestsManager)
        {
            _labTestsManager=labTestsManager;
        }

        [HttpGet]
        [Route("GetLabTestsAsync")]
        public async Task<IActionResult> GetLabTestsAsync()
        {
            try
            {
                var responce = await _labTestsManager.GetLabTestsAsync();

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
