using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface IDoctorManager
    {
        Task<Doctor> InsertOrUpdateDoctorAsync(Doctor doctor);
        Task<Doctor> GetDoctorByIdAsync(Guid DoctorId);
        Task<List<Doctor>> GetDoctorsAsync();
    }
}
