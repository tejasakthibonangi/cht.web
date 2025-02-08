namespace Cht.HMS.Web.Utility
{
    public class PatientInformation
    {
        public PatientInformation()
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
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;
        public PatientCunsultation patientCunsultation { get; set; }
        public PatientPharmacyOrder patientPharmacyOrder { get; set; }
        public PatientLabOrder patientLabOrder { get; set; }
    }

    public class PatientCunsultation
    {
        public PatientCunsultation()
        {
            patientConsultationDetails = new PatientConsultationDetails();
        }
        public Guid? ConsultationId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public DateTimeOffset? ConsultationDate { get; set; }
        public TimeSpan? ConsultationTime { get; set; }
        public string? Symptoms { get; set; }
        public string? Diagnosis { get; set; }
        public string? Remarks { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public PatientConsultationDetails patientConsultationDetails { get; set; }
    }

    public class PatientConsultationDetails
    {
        public Guid? DetailId { get; set; }
        public Guid? ConsultationId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Advice { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }

    public class PatientPharmacyOrder
    {
        public PatientPharmacyOrder()
        {
            patientPharmacyOrderDetails = new List<PatientPharmacyOrderDetail>();
        }
        public Guid? OrderId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? ConsultationId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public int ItemsQty { get; set; }
        public decimal? TotalAmount { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public List<PatientPharmacyOrderDetail> patientPharmacyOrderDetails { get; set; }
    }
    public class PatientPharmacyOrderDetail
    {
        public Guid? OrderDetailId { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }

    public class PatientLabOrder
    {
        public PatientLabOrder()
        {
            patientLabOrderDetails = new List<PatientLabOrderDetail>();
        }
        public Guid? LabOrderId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? ConsultationId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public List<PatientLabOrderDetail> patientLabOrderDetails { get; set; }
    }
    public class PatientLabOrderDetail
    {
        public Guid? LabOrderDetailId { get; set; }
        public Guid? LabOrderId { get; set; }
        public Guid? TestId { get; set; }
        public int Quantity { get; set; }
        public decimal? PricePerUnit { get; set; }
        public decimal? TotalPrice { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
