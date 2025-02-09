﻿
namespace Cht.HMS.Web.Utility
{
    public class PharmacyOrderInfirmation 
    {
        public PharmacyOrderInfirmation()
        {
            patientCunsultation = new PatientCunsultation();
        }
        public Guid? PatientId { get; set; }
        public DateTime? DateOfVisit { get; set; }
        public TimeSpan? TimeOfVisit { get; set; }
        public string? ComingFrom { get; set; }
        public string? Reference { get; set; }
        public string? PatientName { get; set; }
        public string? PhoneNo { get; set; }
        public string? AlternatePhoneNo { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string? BP { get; set; }
        public decimal? Sugar { get; set; }
        public decimal? Temperature { get; set; }
        public string? HealthIssues { get; set; }
        public Guid? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public decimal? Fee { get; set; }
        public string? PreparedBy { get; set; }
        public string? CurrentStatus { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public PatientCunsultation patientCunsultation { get; set; }
        public PatientPharmacyOrder patientPharmacyOrder { get; set; }
    }
}
