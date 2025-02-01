using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cht.HMS.Web.API.DataManager
{
    public class MedicinesDataManager : IMedicinesManager
    {
        private readonly ApplicationDBContext _dBContext;
        public MedicinesDataManager(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public Task<Medicines> GetMedicineByIdAsync(Guid MedicineId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Medicines>> GetMedicinesAsync()
        {
            return await _dBContext.medicines.ToListAsync();
        }

        public Task<Medicines> InsertOrUpdateMedicineAsync(Medicines medicines)
        {
            throw new NotImplementedException();
        }
    }
}
