using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cht.HMS.Web.API.Models
{
    [Table("MedicalConsultation")]
    public class MedicalConsultation
    {
        [Key]
        public Guid ConsultationId { get; set; }
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
    }
}
