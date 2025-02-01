using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cht.HMS.Web.API.Models
{
    [Table("Radiology")]
    public class Radiology
    {
        [Key]
        public Guid RadiologyId { get; set; }
        public Guid PatientId { get; set; }
        public Guid TestId { get; set; }
        public string Result { get; set; }
        public DateTime? TestDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
