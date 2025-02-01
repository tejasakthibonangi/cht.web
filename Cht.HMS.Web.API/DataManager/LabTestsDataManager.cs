using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cht.HMS.Web.API.DataManager
{
    public class LabTestsDataManager : ILabTestsManager
    {
        private readonly ApplicationDBContext _dBContext;
        public LabTestsDataManager(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public Task<LabTests> GetLabTestByIdAsync(Guid TestId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LabTests>> GetLabTestsAsync()
        {
            return await _dBContext.labTests.ToListAsync();
        }

        public Task<LabTests> InsertOrUpdateLabTestAsync(LabTests labTests)
        {
            throw new NotImplementedException();
        }
    }
}
