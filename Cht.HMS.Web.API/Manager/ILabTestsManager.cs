using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.API.Manager
{
    public interface ILabTestsManager
    {
        Task<LabTests> InsertOrUpdateLabTestAsync(LabTests labTests);
        Task<LabTests> GetLabTestByIdAsync(Guid TestId);
        Task<List<LabTests>> GetLabTestsAsync();

        Task<List<LabOrderInfirmation>> GetLabOrdersAsync();
    }
}
