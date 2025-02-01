using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface IDoctorAssignmentManager
    {
        Task<DoctorAssignment> InsertOrUpdateDoctorAssignmentAsync(DoctorAssignment doctorAssignment);
        Task<DoctorAssignment> GetDoctorAssignmentByIdAsync(Guid AssignmentId);
        Task<List<DoctorAssignment>> GetDoctorAssignmentsAsync();
    }
}
