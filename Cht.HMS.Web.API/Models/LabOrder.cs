using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cht.HMS.Web.API.Models
{
    [Table("LabOrder")]
    public class LabOrder
    {
        [Key]
        public Guid LabOrderId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? ConsultationId { get; set; }
        public DateTimeOffset? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
