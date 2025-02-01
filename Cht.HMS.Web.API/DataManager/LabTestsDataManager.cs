using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.DataManager
{
    public class LabTestsDataManager : ILabTestsManager
    {
        public Task<LabTests> GetLabTestByIdAsync(Guid TestId)
        {
            throw new NotImplementedException();
        }

        public Task<List<LabTests>> GetLabTestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LabTests> InsertOrUpdateLabTestAsync(LabTests labTests)
        {
            throw new NotImplementedException();
        }
    }
}
