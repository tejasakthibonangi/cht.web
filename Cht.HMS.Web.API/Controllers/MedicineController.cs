using Cht.HMS.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cht.HMS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicinesManager _medicinesManager;
        public MedicineController(IMedicinesManager medicinesManager)
        {
            _medicinesManager = medicinesManager;
        }

        [HttpGet]
        [Route("GetMedicinesAsync")]
        public async Task<IActionResult> GetMedicinesAsync()
        {
            try
            {
                var responce = await _medicinesManager.GetMedicinesAsync();

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
