using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.API.Manager
{
    public interface IPharmacyOrderManager
    {
        Task<List<PharmacyOrder>> GetPharmacyOrdersAsync();
        Task<List<PharmacyOrder>> GetPharmacyOrdersAsync(Guid patientId);
        Task<PharmacyOrder> GetPharmacyOrderAsync(Guid patientId);
        Task<PatientPharmacyOrder> GetPatientPharmacyOrderAsync(Guid patientId);
        Task<List<PatientPharmacyOrder>> GetPatientPharmacyOrdersAsync(Guid patientId);
        Task<PharmacyOrder> InsertOrUpdatePharmacyOrdersAsync(PharmacyOrder pharmacyOrder);
        Task<PharmacyOrder> InsertOrUpdatePharmacyOrdersAsync(PatientPharmacyOrder pharmacyOrder);
    }
}
