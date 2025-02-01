using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.DataManager
{
    public class PatientTypeDataManager : IPatientTypeManager
    {
        public Task<PatientType> GetPatientTypeByIdAsync(Guid PatientTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PatientType>> GetPatientTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PatientType> InsertOrUpdatePatientTypeAsync(PatientType patientType)
        {
            throw new NotImplementedException();
        }
    }
}
