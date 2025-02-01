using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.DataManager
{
    public class DoctorAssignmentDataManager : IDoctorAssignmentManager
    {
        public Task<DoctorAssignment> GetDoctorAssignmentByIdAsync(Guid AssignmentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<DoctorAssignment>> GetDoctorAssignmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DoctorAssignment> InsertOrUpdateDoctorAssignmentAsync(DoctorAssignment doctorAssignment)
        {
            throw new NotImplementedException();
        }
    }
}
