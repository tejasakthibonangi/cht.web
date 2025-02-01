using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cht.HMS.Web.API.Models
{
    [Table("PatientRegistration")]
    public class PatientRegistration
    {
        [Key]
        public Guid PatientId { get; set; }
        public DateTime DateOfVisit { get; set; }
        public TimeSpan TimeOfVisit { get; set; }
        public string ComingFrom { get; set; }
        public string Reference { get; set; }
        public string PatientName { get; set; }
        public string PhoneNo { get; set; }
        public string AlternatePhoneNo { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public string BP { get; set; }
        public float? Sugar { get; set; }
        public float? Temperature { get; set; }
        public string HealthIssues { get; set; }
        public Guid DoctorAssignedId { get; set; }
        public decimal Fee { get; set; }
        public string PreparedBy { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; } 
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
