namespace Cht.HMS.Web.UI.Models
{
    public class PatientRegistration
    {
        public Guid? PatientId { get; set; }  // Nullable Guid
        public DateTime? DateOfVisit { get; set; }  // Nullable DateTime
        public TimeSpan? TimeOfVisit { get; set; }  // Nullable TimeSpan
        public string ComingFrom { get; set; }  // Nullable string
        public string Reference { get; set; }  // Nullable string
        public string PatientName { get; set; }  // Nullable string
        public string PhoneNo { get; set; }  // Nullable string
        public string AlternatePhoneNo { get; set; }  // Nullable string
        public string Email { get; set; }  // Nullable string
        public string Gender { get; set; }  // Nullable string
        public DateTime? DOB { get; set; }  // Nullable DateTime
        public float? Height { get; set; }  // Nullable float
        public float? Weight { get; set; }  // Nullable float
        public string BP { get; set; }  // Nullable string
        public float? Sugar { get; set; }  // Nullable float
        public float? Temperature { get; set; }  // Nullable float
        public string HealthIssues { get; set; }  // Nullable string
        public Guid? DoctorAssignedId { get; set; }  // Nullable Guid
        public decimal? Fee { get; set; }  // Nullable decimal
        public string PreparedBy { get; set; }  // Nullable string
        public Guid? CreatedBy { get; set; }  // Nullable Guid
        public DateTimeOffset? CreatedOn { get; set; }  // Nullable DateTimeOffset
        public Guid? ModifiedBy { get; set; }  // Nullable Guid
        public DateTimeOffset? ModifiedOn { get; set; }  // Nullable DateTimeOffset
        public bool? IsActive { get; set; } = true;  // Nullable boolean (with default value)
    }

}
