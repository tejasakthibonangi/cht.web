using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.API.Manager
{
    public interface ILabOrderManager
    {
        Task<List<LabOrder>> GetLabOrdersAsync();

        Task<List<PatientLabOrder>> GetLabOrdersAsync(Guid patientId);

        Task<PatientLabOrder> GetPatientLabOrderAsync(Guid patientId);

        Task<PatientLabOrder> InsertOrUpdatePatientLabOrderAsync(PatientLabOrder patientLabOrder);
    }
}
