using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;
using Microsoft.EntityFrameworkCore;

namespace Cht.HMS.Web.API.DataManager
{
    public class DoctorDataManager : IDoctorManager
    {
        private readonly ApplicationDBContext _dbContext;

        public DoctorDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Doctor> GetDoctorByIdAsync(Guid DoctorId)
        {
            return await _dbContext.doctors.FindAsync(DoctorId);
        }

        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await _dbContext.doctors.ToListAsync();
        }

        public async Task<Doctor> InsertOrUpdateDoctorAsync(Doctor doctor)
        {
            var doctorRole = await _dbContext.roles.Where(x => x.Name == "Doctor").FirstOrDefaultAsync();
            if (doctor != null)
            {
                if (doctor.DoctorId == Guid.Empty)
                {
                    await _dbContext.doctors.AddAsync(doctor);


                    var password = ChtHashSalt.GenerateSaltedHash("Admin@2025");

                    User dbuser = new User()
                    {
                        FirstName = doctor.DoctorName,
                        LastName = doctor.DoctorName,
                        Email = doctor.Email,
                        Phone = doctor.Phone,
                        RoleId = doctorRole.RoleId,
                        PasswordHash = password.Hash,
                        PasswordSalt = password.Salt,
                        IsBlocked = false,
                        LastPasswordChangedOn = DateTime.Now,
                        CreatedBy = doctor.CreatedBy,
                        CreatedOn = doctor.CreatedOn,
                        ModifiedBy = doctor.ModifiedBy,
                        ModifiedOn = doctor.ModifiedOn,
                        IsActive = doctor.IsActive
                    };
                }
                else
                {
                    var _doctor = await _dbContext.doctors.FindAsync(doctor.DoctorId);

                    if (_doctor != null)
                    {
                        _doctor.DoctorName = _doctor.DoctorName;
                        _doctor.Specialty = doctor.Specialty;
                        _doctor.Phone = doctor.Phone;
                        _doctor.Email = doctor.Email;
                        _doctor.IsActive = doctor.IsActive;
                        _doctor.ModifiedBy = doctor.ModifiedBy;
                        _doctor.ModifiedOn = doctor.ModifiedOn;
                    }
                }
            }

            await _dbContext.SaveChangesAsync();



            return doctor;
        }
    }
}
