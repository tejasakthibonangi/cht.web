using Cht.HMS.Web.UI.Factory;
using Cht.HMS.Web.UI.Interfaces;
using Cht.HMS.Web.UI.Models;

namespace Cht.HMS.Web.UI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepositoryFactory _repository;

        public PatientService(IRepositoryFactory repository)
        {
            _repository = repository;
        }
        public async Task<List<PatientRegistration>> GetPatientRegistrationsAsync()
        {
            return await _repository.SendAsync<List<PatientRegistration>>(HttpMethod.Get, "Patient/GetPatientRegistrationsAsync");
        }

        public async Task<List<PatientRegistration>> GetPatientRegistrationsAsync(string inputString)
        {
            var uri = Path.Combine("Patient/SearchPatientRegistrationsAsync", inputString.ToString());
            return await _repository.SendAsync<List<PatientRegistration>>(HttpMethod.Get, uri);
        }

        public async Task<PatientRegistration> PatientRegistrationAsync(PatientRegistration registration)
        {
            return await _repository.SendAsync<PatientRegistration, PatientRegistration>(HttpMethod.Post, "Patient/InsertOrUpdatePatientRegistrationAsync", registration);
        }
    }
}
