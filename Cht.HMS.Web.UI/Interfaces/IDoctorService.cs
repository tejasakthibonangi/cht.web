using Cht.HMS.Web.UI.Models;

namespace Cht.HMS.Web.UI.Interfaces
{
    public interface IDoctorService
    {
        Task<Doctor> InsertOrUpdateDoctorAsync(Doctor doctor);
        Task<Doctor> GetDoctorByIdAsync(Guid DoctorId);
        Task<List<Doctor>> GetDoctorsAsync();
    }
}
