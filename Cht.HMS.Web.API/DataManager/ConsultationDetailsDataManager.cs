using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cht.HMS.Web.API.DataManager
{
    public class ConsultationDetailsDataManager : IConsultationDetailsManager
    {
        private readonly ApplicationDBContext _dbContext;

        public ConsultationDetailsDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ConsultationDetails>> GetConsultationDetails()
        {
            return await _dbContext.consultationDetails.ToListAsync();
        }

        public async Task<ConsultationDetails> GetConsultationDetailsByIdAsync(Guid DetailId)
        {
            return await _dbContext.consultationDetails.FindAsync(DetailId);
        }

        public async Task<ConsultationDetails> InsertOrUpdateConsultationDetails(ConsultationDetails consultationDetails)
        {
            if(consultationDetails != null)
            {
                if(consultationDetails.DetailId == Guid.Empty)
                {
                    _dbContext.consultationDetails.AddAsync(consultationDetails);
                }
                else
                {
                    var _consultationDetail = await _dbContext.consultationDetails.FindAsync(consultationDetails.DetailId);

                    if(_consultationDetail != null)
                    {
                        _consultationDetail.DetailId = consultationDetails.DetailId;
                        _consultationDetail.ConsultationId = consultationDetails.ConsultationId;
                        _consultationDetail.Diagnosis = consultationDetails.Diagnosis;
                        _consultationDetail.Treatment = consultationDetails.Treatment;
                        _consultationDetail.Advice = consultationDetails.Advice;
                        _consultationDetail.FollowUpDate = consultationDetails.FollowUpDate;
                        _consultationDetail.IsActive = consultationDetails.IsActive;
                        _consultationDetail.ModifiedBy = consultationDetails.ModifiedBy;
                        _consultationDetail.ModifiedOn = consultationDetails.ModifiedOn;
                    }
                }
            }

            await _dbContext.SaveChangesAsync();

            return consultationDetails;
        }
    }
}
