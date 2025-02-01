using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.DataManager
{
    public class RadiologyDataManager : IRadiologyManager
    {
        public Task<Radiology> GetDoctorByIdAsync(Guid RadiologyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Radiology>> GetDoctorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Radiology> InsertOrUpdateDoctorAsync(Radiology radiology)
        {
            throw new NotImplementedException();
        }
    }
}
