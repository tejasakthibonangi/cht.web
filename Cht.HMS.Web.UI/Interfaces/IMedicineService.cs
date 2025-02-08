using Cht.HMS.Web.UI.Models;

namespace Cht.HMS.Web.UI.Interfaces
{
    public interface IMedicineService
    {
        Task<Medicine> InsertOrUpdateMedicineAsync(Medicine medicine);
        Task<Medicine> GetMedicineByIdAsync(Guid medicineId);
        Task<List<Medicine>> GetMedicinesAsync();
    }
}
