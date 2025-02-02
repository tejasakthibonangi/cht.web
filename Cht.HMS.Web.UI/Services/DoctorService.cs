using Cht.HMS.Web.UI.Factory;
using Cht.HMS.Web.UI.Interfaces;
using Cht.HMS.Web.UI.Models;
using System;

namespace Cht.HMS.Web.UI.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepositoryFactory _repository;

        public DoctorService(IRepositoryFactory repository)
        {
            _repository = repository;
        }
        public  async Task<Doctor> GetDoctorByIdAsync(Guid doctorId)
        {
            var uri = Path.Combine("Doctor/GetDoctorByIdAsync", doctorId.ToString());
            return await _repository.SendAsync<Doctor>(HttpMethod.Get, uri);
        }

        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await _repository.SendAsync<List<Doctor>>(HttpMethod.Get, "Doctor/GetDoctorsAsync");
        }

        public async Task<Doctor> InsertOrUpdateDoctorAsync(Doctor doctor)
        {
            return await _repository.SendAsync<Doctor, Doctor>(HttpMethod.Post, "Doctor/InsertOrUpdateDoctorAsync", doctor);
        }
    }
}
