using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface IPatientRegistrationManager
    {
        Task<PatientRegistration> InsertOrUpdatePatientRegistrationAsync(PatientRegistration patientRegistration);
        Task<PatientRegistration> GetPatientRegistrationByIdAsync(Guid PatientId);
        Task<List<PatientRegistration>> GetPatientRegistrationsAsync();
    }
}
