using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.API.Manager
{
    public interface IPharmacyManager
    {
        Task<Pharmacy> InsertOrUpdatePharmacyAsync(Pharmacy pharmacy);
        Task<Pharmacy> GetPharmacyByIdAsync(Guid PharmacyId);
        Task<List<Pharmacy>> GetPharmacysAsync();
        Task<List<PharmacyOrderInfirmation>> GetPatientPharmacyOrderAsync();
    }
}
