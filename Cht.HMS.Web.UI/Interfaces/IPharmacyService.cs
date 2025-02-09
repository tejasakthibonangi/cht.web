using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.UI.Interfaces
{
    public interface IPharmacyService
    {
        Task<List<PharmacyOrderInfirmation>> GetPatientPharmacyOrderAsync();
    }
}
