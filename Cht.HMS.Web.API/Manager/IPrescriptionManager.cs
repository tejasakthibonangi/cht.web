using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface IPrescriptionManager
    {
        Task<Prescription> InsertOrUpdatePrescriptionAsync(Prescription prescription);
        Task<Prescription> GetPrescriptionByIdAsync(Guid PrescriptionId);
        Task<List<Prescription>> GetPrescriptionsAsync();
    }
}
