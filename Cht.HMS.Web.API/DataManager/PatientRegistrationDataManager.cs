using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.DataManager
{
    public class PatientRegistrationDataManager : IPatientRegistrationManager
    {
        public Task<PatientRegistration> GetPatientRegistrationByIdAsync(Guid PatientId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PatientRegistration>> GetPatientRegistrationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PatientRegistration> InsertOrUpdatePatientRegistrationAsync(PatientRegistration patientRegistration)
        {
            throw new NotImplementedException();
        }
    }
}
