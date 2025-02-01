using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.DataManager
{
    public class MedicalConsultationDataManager : IMedicalConsultationManager
    {
        public Task<MedicalConsultation> GetMedicalConsultationByIdAsync(Guid ConsultationId)
        {
            throw new NotImplementedException();
        }

        public Task<List<MedicalConsultation>> GetMedicalConsultationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MedicalConsultation> InsertOrUpdateMedicalConsultationAsync(MedicalConsultation medicalConsultation)
        {
            throw new NotImplementedException();
        }
    }
}
