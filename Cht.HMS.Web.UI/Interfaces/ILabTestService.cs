using Cht.HMS.Web.UI.Models;

namespace Cht.HMS.Web.UI.Interfaces
{
    public interface ILabTestService
    {
        Task<LabTest> InsertOrUpdateLabTestAsync(LabTest labTest);
        Task<LabTest> GetLabTestByIdAsync(Guid testId);
        Task<List<LabTest>> GetLabTestsAsync();
    }
}
