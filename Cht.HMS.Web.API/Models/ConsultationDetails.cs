using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cht.HMS.Web.API.Models
{
    [Table("ConsultationDetails")]
    public class ConsultationDetails
    {
        [Key]
        public Guid DetailId { get; set; }
        public Guid ConsultationId { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Advice { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
