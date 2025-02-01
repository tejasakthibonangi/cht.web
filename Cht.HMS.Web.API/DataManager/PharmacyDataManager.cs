using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.DataManager
{
    public class PharmacyDataManager : IPharmacyManager
    {
        public Task<Pharmacy> GetPharmacyByIdAsync(Guid PharmacyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Pharmacy>> GetPharmacysAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Pharmacy> InsertOrUpdatePharmacyAsync(Pharmacy pharmacy)
        {
            throw new NotImplementedException();
        }
    }
}
