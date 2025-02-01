using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cht.HMS.Web.API.Models
{
    [Table("Pharmacy")]
    public class Pharmacy
    {
        [Key]
        public Guid PharmacyId { get; set; }
        public Guid PrescriptionId { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
