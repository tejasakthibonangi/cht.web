using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.DataManager
{
    public class PrescriptionDataManager : IPrescriptionManager
    {
        public Task<Prescription> GetPrescriptionByIdAsync(Guid PrescriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Prescription>> GetPrescriptionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Prescription> InsertOrUpdatePrescriptionAsync(Prescription prescription)
        {
            throw new NotImplementedException();
        }
    }
}
