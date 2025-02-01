using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Cht.HMS.Web.API.DBConfiguration
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //from here add your db sets 
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<ConsultationDetails> consultationDetails { get; set; }
        public DbSet<IDoctorAssignmentManager> doctorAssignmentManagers { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<LabTests> labTests { get; set; }
        public DbSet<MedicalConsultation> medicalConsultations { get; set; }
        public DbSet<Medicines> medicines { get; set; }
        public DbSet<PatientRegistration> patientRegistrations { get; set; }
        public DbSet<PatientType> patientTypes { get; set; }
        public DbSet<PaymentType> paymentTypes { get; set; }
        public DbSet<Pharmacy> pharmacys { get; set; }
        public DbSet<Prescription> prescriptions {  get; set; }
        public DbSet<Radiology> radiologies { get; set; }

    }
}
