﻿using Cht.HMS.Web.UI.Factory;
using Cht.HMS.Web.UI.Interfaces;
using Cht.HMS.Web.UI.Models;
using Cht.HMS.Web.Utility;

namespace Cht.HMS.Web.UI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepositoryFactory _repository;

        public PatientService(IRepositoryFactory repository)
        {
            _repository = repository;
        }

        public async Task<List<PatientRegistration>> GetPatientByDoctorAsync(Guid doctorId)
        {
            var uri = Path.Combine("Patient/GetPatientByDoctorAsync", doctorId.ToString());
            return await _repository.SendAsync<List<PatientRegistration>>(HttpMethod.Get, uri);
        }

        public async Task<PatientInformation> GetPatientDetailsAsync(Guid patientId)
        {
            var uri = Path.Combine("Patient/GetPatientDetailsAsync", patientId.ToString());
            return await _repository.SendAsync<PatientInformation>(HttpMethod.Get, uri);
        }

        public async Task<List<PatientInformation>> GetPatientRegistrationsAsync()
        {
            return await _repository.SendAsync<List<PatientInformation>>(HttpMethod.Get, "Patient/GetPatientRegistrationsAsync");
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
        public async Task<PatientInformation> UpsertPatientConsultationDetailsAsync(PatientInformation registration)
        {
            return await _repository.SendAsync<PatientInformation, PatientInformation>(HttpMethod.Post, "Patient/UpsertPatientConsultationDetailsAsync", registration);
        }
    }
}
