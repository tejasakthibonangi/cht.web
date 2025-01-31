using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface IConsultationDetailsManager
    {
        Task<List<ConsultationDetails>> GetConsultationDetails();
        Task<ConsultationDetails> GetConsultationDetailsByIdAsync(Guid DetailId);
        Task<ConsultationDetails> InsertOrUpdateConsultationDetails(ConsultationDetails consultationDetails);
    }
}
