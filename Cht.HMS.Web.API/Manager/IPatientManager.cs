using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface IPatientManager
    {
        Task<PatientRegistration> PatientRegistrationAsync(PatientRegistration registration);

        Task<List<PatientRegistration>> GetPatientRegistrationsAsync();

        Task<List<PatientRegistration>> GetPatientRegistrationsAsync(string inputString);
    }
}
