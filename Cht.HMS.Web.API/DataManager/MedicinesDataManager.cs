using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.DataManager
{
    public class MedicinesDataManager : IMedicinesManager
    {
        public Task<Medicines> GetMedicineByIdAsync(Guid MedicineId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Medicines>> GetMedicinesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Medicines> InsertOrUpdateMedicineAsync(Medicines medicines)
        {
            throw new NotImplementedException();
        }
    }
}
