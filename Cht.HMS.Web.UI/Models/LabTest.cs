namespace Cht.HMS.Web.UI.Models
{
    public class LabTest
    {
        public Guid? TestId { get; set; }
        public string? TestName { get; set; }
        public string? TestDescription { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
