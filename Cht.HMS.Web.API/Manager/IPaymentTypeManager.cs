using Cht.HMS.Web.API.Models;

namespace Cht.HMS.Web.API.Manager
{
    public interface IPaymentTypeManager
    {
        Task<PaymentType> InsertOrUpdatePaymentTypeAsync(PaymentType paymentType);
        Task<PaymentType> GetPaymentTypeByIdAsync(Guid PaymentTypeId);
        Task<List<PaymentType>> GetPaymentTypesAsync();
    }
}
