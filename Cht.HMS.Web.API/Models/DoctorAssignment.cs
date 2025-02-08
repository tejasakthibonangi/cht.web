using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cht.HMS.Web.API.Models
{
    [Table("DoctorAssignment")]
    public class DoctorAssignment
    {
        [Key]
        public Guid AssignmentId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public TimeSpan? AssignmentTime { get; set; }
        public Guid? AssignedBy { get; set; }
        public string Remarks { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
