using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface IMedicalConsultationManager
    {
        Task<MedicalConsultation> InsertOrUpdateMedicalConsultationAsync(MedicalConsultation medicalConsultation);
        Task<MedicalConsultation> GetMedicalConsultationByIdAsync(Guid ConsultationId);
        Task<List<MedicalConsultation>> GetMedicalConsultationsAsync();
    }
}
