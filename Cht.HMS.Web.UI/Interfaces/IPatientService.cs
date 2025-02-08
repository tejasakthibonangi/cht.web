using Cht.HMS.Web.UI.Models;
using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.UI.Interfaces
{
    public interface IPatientService
    {
        Task<PatientRegistration> PatientRegistrationAsync(PatientRegistration registration);

        Task<List<PatientInformation>> GetPatientRegistrationsAsync();

        Task<List<PatientRegistration>> GetPatientRegistrationsAsync(string inputString);
        Task<List<PatientRegistration>> GetPatientByDoctorAsync(Guid doctorId);

        Task<PatientInformation> GetPatientDetailsAsync(Guid patientId);
    }
}
