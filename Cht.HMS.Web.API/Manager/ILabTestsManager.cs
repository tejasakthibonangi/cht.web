using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface ILabTestsManager
    {
        Task<LabTests> InsertOrUpdateLabTestAsync(LabTests labTests);
        Task<LabTests> GetLabTestByIdAsync(Guid TestId);
        Task<List<LabTests>> GetLabTestsAsync();
    }
}
