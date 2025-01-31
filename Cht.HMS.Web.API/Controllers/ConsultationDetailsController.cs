using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Cht.HMS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationDetailsController : ControllerBase
    {
        private readonly IConsultationDetailsManager _ConsultationDetailsManager;

        public ConsultationDetailsController(IConsultationDetailsManager consultationDetailsManager)
        {
            _ConsultationDetailsManager = consultationDetailsManager;
        }

        [HttpGet]
        [Route("GetConsultationDetails")]
        public async Task<IActionResult> GetConsultationDetails()
        {
            try
            {
                var responce = await _ConsultationDetailsManager.GetConsultationDetails();

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetConsultationDetailsByIdAsync")]
        public async Task<IActionResult> GetConsultationDetailsByIdAsync(Guid DetailId)
        {
            try
            {
                var responce = await _ConsultationDetailsManager.GetConsultationDetailsByIdAsync(DetailId);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateConsultationDetails")]
        public async Task<IActionResult> InsertOrUpdateConsultationDetails(ConsultationDetails consultationDetails)
        {
            try
            {
                var responce = await _ConsultationDetailsManager.InsertOrUpdateConsultationDetails(consultationDetails);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
