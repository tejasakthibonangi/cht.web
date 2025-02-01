using Cht.HMS.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RadiologyController : ControllerBase
    {
        private readonly IRadiologyManager _radiologyManager;
        public RadiologyController(IRadiologyManager radiologyManager)
        {
            _radiologyManager = radiologyManager;
        }
        [HttpGet]
        [Route("GetRadiologysAsync")]
        public async Task<IActionResult> GetRadiologysAsync()
        {
            try
            {
                var responce = await _radiologyManager.GetRadiologysAsync();

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
