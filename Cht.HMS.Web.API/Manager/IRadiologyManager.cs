using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface IRadiologyManager
    {
        Task<Radiology> InsertOrUpdateDoctorAsync(Radiology radiology);
        Task<Radiology> GetDoctorByIdAsync(Guid RadiologyId);
        Task<List<Radiology>> GetRadiologysAsync();
    }
}
