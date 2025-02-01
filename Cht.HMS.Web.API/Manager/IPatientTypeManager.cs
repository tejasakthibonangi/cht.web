using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface IPatientTypeManager
    {
        Task<PatientType> InsertOrUpdatePatientTypeAsync(PatientType patientType);
        Task<PatientType> GetPatientTypeByIdAsync(Guid PatientTypeId);
        Task<List<PatientType>> GetPatientTypesAsync();
    }
}
