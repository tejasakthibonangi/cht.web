using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface IPharmacyManager
    {
        Task<Pharmacy> InsertOrUpdatePharmacyAsync(Pharmacy pharmacy);
        Task<Pharmacy> GetPharmacyByIdAsync(Guid PharmacyId);
        Task<List<Pharmacy>> GetPharmacysAsync();
    }
}
