using Cht.HMS.Web.UI.Factory;
using Cht.HMS.Web.UI.Interfaces;
using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.UI.Services
{
    public class PharmacyService : IPharmacyService
    {
        private readonly IRepositoryFactory _repository;

        public PharmacyService(IRepositoryFactory repository)
        {
            _repository = repository;
        }
        public async Task<List<PharmacyOrderInfirmation>> GetPatientPharmacyOrderAsync()
        {
            return await _repository.SendAsync<List<PharmacyOrderInfirmation>>(HttpMethod.Get, "Pharmacy/GetPharmacyOrdersAsync");
        }
    }
}
