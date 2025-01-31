namespace Cht.HMS.Web.UI.Models
{
    public class Role
    {
        public Guid? RoleId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
