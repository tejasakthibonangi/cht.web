using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
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
    }
}
