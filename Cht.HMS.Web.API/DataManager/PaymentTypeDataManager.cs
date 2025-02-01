using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.DataManager
{
    public class PaymentTypeDataManager : IPaymentTypeManager
    {
        public Task<PaymentType> GetPaymentTypeByIdAsync(Guid PaymentTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PaymentType>> GetPaymentTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PaymentType> InsertOrUpdatePaymentTypeAsync(PaymentType paymentType)
        {
            throw new NotImplementedException();
        }
    }
}
