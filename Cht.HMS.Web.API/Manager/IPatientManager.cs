using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.API.Manager
{
    public interface IPatientManager
    {
        Task<PatientRegistration> PatientRegistrationAsync(PatientRegistration registration);

        Task<List<PatientRegistration>> GetPatientRegistrationsAsync();

        Task<List<PatientRegistration>> GetPatientRegistrationsAsync(string inputString);

        Task<List<PatientInformation>> GetPatientInformationsAsync(string inputString);

        Task<List<PatientInformation>> GetPatientInformationsByDoctorAsync(Guid doctorId);

        Task<PatientInformation> GetPatientDetailsAsync(Guid patientId);

        Task<PatientInformation> UpsertPatientConsultationDetailsAsync(PatientInformation patientInformation);



    }
}
