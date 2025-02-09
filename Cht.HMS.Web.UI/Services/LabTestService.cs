using Cht.HMS.Web.UI.Factory;
using Cht.HMS.Web.UI.Interfaces;
using Cht.HMS.Web.UI.Models;
using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.UI.Services
{
    public class LabTestService : ILabTestService
    {
        private readonly IRepositoryFactory _repository;

        public LabTestService(IRepositoryFactory repository)
        {
            _repository = repository;
        }

        public async Task<List<LabOrderInfirmation>> GetLabOrdersAsync()
        {
            return await _repository.SendAsync<List<LabOrderInfirmation>>(HttpMethod.Get, "LabTest/GetLabOrdersAsync");
        }

        public async Task<LabTest> GetLabTestByIdAsync(Guid testId)
        {
            var uri = Path.Combine("LabTest/GetLabTestByIdAsync", testId.ToString());
            return await _repository.SendAsync<LabTest>(HttpMethod.Get, uri);
        }

        public async Task<List<LabTest>> GetLabTestsAsync()
        {
            return await _repository.SendAsync<List<LabTest>>(HttpMethod.Get, "LabTest/GetLabTestsAsync");
        }

        public async Task<LabTest> InsertOrUpdateLabTestAsync(LabTest labTest)
        {
            return await _repository.SendAsync<LabTest, LabTest>(HttpMethod.Post, "LabTest/InsertOrUpdateLabTestAsync", labTest);
        }
    }
}
