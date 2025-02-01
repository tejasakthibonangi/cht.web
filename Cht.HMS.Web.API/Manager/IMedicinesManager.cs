using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface IMedicinesManager
    {
        Task<Medicines> InsertOrUpdateMedicineAsync(Medicines medicines);
        Task<Medicines> GetMedicineByIdAsync(Guid MedicineId);
        Task<List<Medicines>> GetMedicinesAsync();
    }
}
