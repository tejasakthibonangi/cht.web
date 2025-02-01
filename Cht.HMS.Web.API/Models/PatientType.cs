using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cht.HMS.Web.API.Models
{
    [Table("PatientType")]
    public class PatientType
    {
        [Key]
        public Guid PatientTypeId { get; set; } 
        public string PatientTypeName { get; set; }
        public string Description { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
