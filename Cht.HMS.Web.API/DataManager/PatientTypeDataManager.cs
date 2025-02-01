using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cht.HMS.Web.API.DataManager
{
    public class PatientTypeDataManager : IPatientTypeManager
    {
        private readonly ApplicationDBContext _dBContext;
        public PatientTypeDataManager(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public Task<PatientType> GetPatientTypeByIdAsync(Guid PatientTypeId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PatientType>> GetPatientTypesAsync()
        {
            return await _dBContext.patientTypes.ToListAsync();
        }

        public Task<PatientType> InsertOrUpdatePatientTypeAsync(PatientType patientType)
        {
            throw new NotImplementedException();
        }
    }
}
