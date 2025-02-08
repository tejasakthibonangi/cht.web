using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cht.HMS.Web.API.DataManager
{
    public class PatientDataManager : IPatientManager
    {
        private readonly ApplicationDBContext _dbContext;
        public PatientDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PatientInformation> GetPatientDetailsAsync(Guid patientId)
        {
            var patientInformation = new PatientInformation();

            var patient = await _dbContext.patientRegistrations.FindAsync(patientId);

            if (patient != null)
            {
                var doctor = await _dbContext.doctors
                    .Where(x => x.DoctorId == patient.DoctorAssignedId)
                    .FirstOrDefaultAsync();

                var patientCunsultation = await _dbContext.medicalConsultations
                    .Where(x => x.PatientId == patient.PatientId)
                    .FirstOrDefaultAsync();

                patientInformation = new PatientInformation
                {
                    PatientId = patient.PatientId,
                    DateOfVisit = patient.DateOfVisit,
                    TimeOfVisit = patient.TimeOfVisit,
                    ComingFrom = patient.ComingFrom,
                    Reference = patient.Reference,
                    PatientName = patient.PatientName,
                    PhoneNo = patient.PhoneNo,
                    AlternatePhoneNo = patient.AlternatePhoneNo,
                    Email = patient.Email,
                    Gender = patient.Gender,
                    DOB = patient.DOB,
                    Height = patient.Height,
                    Weight = patient.Weight,
                    BP = patient.BP,
                    Sugar = patient.Sugar,
                    Temperature = patient.Temperature,
                    HealthIssues = patient.HealthIssues,
                    DoctorId = patient.DoctorAssignedId,
                    Fee = patient.Fee,
                    PreparedBy = patient.PreparedBy,
                    CurrentStatus = patient.CurrentStatus,
                    CreatedBy = patient.CreatedBy,
                    CreatedOn = patient.CreatedOn,
                    ModifiedBy = patient.ModifiedBy,
                    ModifiedOn = patient.ModifiedOn,
                    IsActive = patient.IsActive,
                    DoctorName = doctor?.DoctorName ?? "Unknown Doctor"
                };
                if (patientCunsultation != null)
                {
                    var patientCunsultationDetails = await _dbContext.consultationDetails
                        .Where(x => x.ConsultationId == patientCunsultation.ConsultationId)
                        .FirstOrDefaultAsync();

                    patientInformation.patientCunsultation = new PatientCunsultation()
                    {
                        PatientId = patient.PatientId,
                        ConsultationDate = patientCunsultation.ConsultationDate,
                        ConsultationTime = patientCunsultation.ConsultationTime,
                        ConsultationId = patientCunsultation.ConsultationId,
                        Diagnosis = patientCunsultation.Diagnosis,
                        CreatedBy = patientCunsultation.CreatedBy,
                        CreatedOn = patientCunsultation.CreatedOn,
                        DoctorId = patientCunsultation.DoctorId,
                        Symptoms = patientCunsultation.Symptoms,
                        IsActive = patientCunsultation.IsActive,
                        ModifiedBy = patientCunsultation.ModifiedBy,
                        Remarks = patientCunsultation.Remarks,
                        ModifiedOn = patientCunsultation.ModifiedOn
                    };

                    if (patientCunsultationDetails != null)
                    {
                        patientInformation.patientCunsultation.patientConsultationDetails = new PatientConsultationDetails()
                        {
                            ConsultationId = patientCunsultationDetails.ConsultationId,
                            Advice = patientCunsultationDetails.Advice,
                            CreatedBy = patientCunsultationDetails.CreatedBy,
                            CreatedOn = patientCunsultationDetails.CreatedOn,
                            DetailId = patientCunsultationDetails.DetailId,
                            Diagnosis = patientCunsultationDetails.Diagnosis,
                            FollowUpDate = patientCunsultationDetails.FollowUpDate,
                            IsActive = patientCunsultationDetails.IsActive,
                            ModifiedBy = patientCunsultationDetails.ModifiedBy,
                            ModifiedOn = patientCunsultationDetails.ModifiedOn,
                            Treatment = patientCunsultationDetails.Treatment
                        };
                    }
                }

            }

            return patientInformation;
        }

        public async Task<List<PatientInformation>> GetPatientInformationsAsync(string inputString = "")
        {

            var doctors = await _dbContext.doctors.ToListAsync();

            return await PreparePatientRegistrationsAsync(doctors, inputString);
        }

        public async Task<List<PatientInformation>> GetPatientInformationsByDoctorAsync(Guid doctorId)
        {

            var doctors = await _dbContext.doctors.Where(x => x.UserId == doctorId).ToListAsync();

            var patients = await PreparePatientRegistrationsAsync(doctors, string.Empty);

            var doctorIds = doctors.Select(d => d.DoctorId).ToList();

            var relatedPatients = patients.Where(p => doctorIds.Contains(p.DoctorId.Value)).ToList();

            return relatedPatients;
        }

        public async Task<List<PatientRegistration>> GetPatientRegistrationsAsync()
        {
            return await _dbContext.patientRegistrations.ToListAsync();
        }

        public async Task<List<PatientRegistration>> GetPatientRegistrationsAsync(string inputString)
        {
            var query = _dbContext.patientRegistrations.AsQueryable();

            if (!string.IsNullOrEmpty(inputString))
            {
                query = query.Where(x =>
                    (x.PatientName.Contains(inputString)) ||
                    (x.PhoneNo.Contains(inputString)) ||
                    (x.AlternatePhoneNo.Contains(inputString)) ||
                    (x.Email.Contains(inputString)) ||
                    (x.ComingFrom.Contains(inputString)) ||
                    (x.Reference.Contains(inputString)) ||
                    (x.Gender.Contains(inputString)) ||
                    (x.HealthIssues.Contains(inputString))
                );
            }

            return await query.ToListAsync();
        }


        public async Task<PatientRegistration> PatientRegistrationAsync(PatientRegistration registration)
        {
            if (registration.PatientId == Guid.Empty)
            {
                await _dbContext.patientRegistrations.AddAsync(registration);
            }
            else
            {
                var existingRegistration = await _dbContext.patientRegistrations.FindAsync(registration.PatientId);

                if (existingRegistration != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingRegistration, registration, nameof(PatientRegistration.CreatedBy), nameof(PatientRegistration.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingRegistration, registration, nameof(PatientRegistration.CreatedBy), nameof(PatientRegistration.CreatedOn));

                    }
                }
            }

            await _dbContext.SaveChangesAsync();

            return registration;
        }

        private async Task<List<PatientInformation>> PreparePatientRegistrationsAsync(List<Doctor> doctors, string inputString)
        {
            List<PatientInformation> patientInformation = new List<PatientInformation>();

            List<PatientRegistration> patientRegistrations = new List<PatientRegistration>();

            var query = _dbContext.patientRegistrations.AsQueryable();

            if (!string.IsNullOrEmpty(inputString))
            {
                query = query.Where(x =>
                    (x.PatientName.Contains(inputString)) ||
                    (x.PhoneNo.Contains(inputString)) ||
                    (x.AlternatePhoneNo.Contains(inputString)) ||
                    (x.Email.Contains(inputString)) ||
                    (x.ComingFrom.Contains(inputString)) ||
                    (x.Reference.Contains(inputString)) ||
                    (x.Gender.Contains(inputString)) ||
                    (x.HealthIssues.Contains(inputString))
                );
            }

            patientRegistrations = await query.ToListAsync();

            patientInformation = (from patient in patientRegistrations
                                  join doctor in doctors on patient.DoctorAssignedId equals doctor.DoctorId into doctorInfo
                                  from doctorJoinInfo in doctorInfo.DefaultIfEmpty()
                                  select new PatientInformation
                                  {
                                      PatientId = patient.PatientId,
                                      DateOfVisit = patient.DateOfVisit,
                                      TimeOfVisit = patient.TimeOfVisit,
                                      ComingFrom = patient.ComingFrom,
                                      Reference = patient.Reference,
                                      PatientName = patient.PatientName,
                                      PhoneNo = patient.PhoneNo,
                                      AlternatePhoneNo = patient.AlternatePhoneNo,
                                      Email = patient.Email,
                                      Gender = patient.Gender,
                                      DOB = patient.DOB,
                                      Height = patient.Height,
                                      Weight = patient.Weight,
                                      BP = patient.BP,
                                      Sugar = patient.Sugar,
                                      Temperature = patient.Temperature,
                                      HealthIssues = patient.HealthIssues,
                                      DoctorId = patient.DoctorAssignedId,
                                      Fee = patient.Fee,
                                      PreparedBy = patient.PreparedBy,
                                      CurrentStatus = patient.CurrentStatus,
                                      CreatedBy = patient.CreatedBy,
                                      CreatedOn = patient.CreatedOn,
                                      ModifiedBy = patient.ModifiedBy,
                                      ModifiedOn = patient.ModifiedOn,
                                      IsActive = patient.IsActive,
                                      DoctorName = doctorJoinInfo?.DoctorName ?? "Unknown Doctor",
                                  }).OrderByDescending(x => x.CreatedOn).ToList();

            return patientInformation;
        }

        private PatientInformation MapToPatientInformation(PatientRegistration patient, Doctor doctor)
        {
            return new PatientInformation
            {
                PatientId = patient.PatientId,
                DateOfVisit = patient.DateOfVisit,
                TimeOfVisit = patient.TimeOfVisit,
                ComingFrom = patient.ComingFrom,
                Reference = patient.Reference,
                PatientName = patient.PatientName,
                PhoneNo = patient.PhoneNo,
                AlternatePhoneNo = patient.AlternatePhoneNo,
                Email = patient.Email,
                Gender = patient.Gender,
                DOB = patient.DOB,
                Height = patient.Height,
                Weight = patient.Weight,
                BP = patient.BP,
                Sugar = patient.Sugar,
                Temperature = patient.Temperature,
                HealthIssues = patient.HealthIssues,
                DoctorId = patient.DoctorAssignedId,
                Fee = patient.Fee,
                PreparedBy = patient.PreparedBy,
                CurrentStatus = patient.CurrentStatus,
                CreatedBy = patient.CreatedBy,
                CreatedOn = patient.CreatedOn,
                ModifiedBy = patient.ModifiedBy,
                ModifiedOn = patient.ModifiedOn,
                IsActive = patient.IsActive,
                DoctorName = doctor?.DoctorName ?? "Unknown Doctor"
            };
        }
    }
}
