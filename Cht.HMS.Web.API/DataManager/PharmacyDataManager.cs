using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cht.HMS.Web.API.DataManager
{
    public class PharmacyDataManager : IPharmacyManager
    {
        private readonly ApplicationDBContext _dBContext;
        public PharmacyDataManager(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public Task<Pharmacy> GetPharmacyByIdAsync(Guid PharmacyId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Pharmacy>> GetPharmacysAsync()
        {
            return await _dBContext.pharmacys.ToListAsync();
        }

        public Task<Pharmacy> InsertOrUpdatePharmacyAsync(Pharmacy pharmacy)
        {
            throw new NotImplementedException();
        }
    }
}
