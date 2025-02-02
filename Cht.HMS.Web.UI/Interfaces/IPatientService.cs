using Cht.HMS.Web.UI.Models;

namespace Cht.HMS.Web.UI.Interfaces
{
    public interface IPatientService
    {
        Task<PatientRegistration> PatientRegistrationAsync(PatientRegistration registration);

        Task<List<PatientRegistration>> GetPatientRegistrationsAsync();

        Task<List<PatientRegistration>> GetPatientRegistrationsAsync(string inputString);
    }
}
