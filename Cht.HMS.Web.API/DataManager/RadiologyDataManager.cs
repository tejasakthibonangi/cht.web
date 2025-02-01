using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cht.HMS.Web.API.DataManager
{
    public class RadiologyDataManager : IRadiologyManager
    {
        private readonly ApplicationDBContext _dBContext;
        public RadiologyDataManager(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public Task<Radiology> GetDoctorByIdAsync(Guid RadiologyId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Radiology>> GetRadiologysAsync()
        {
            return await _dBContext.radiologies.ToListAsync();
        }

        public Task<Radiology> InsertOrUpdateDoctorAsync(Radiology radiology)
        {
            throw new NotImplementedException();
        }
    }
}
