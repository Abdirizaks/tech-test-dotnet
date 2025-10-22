using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validator;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService(IAccountDataStoreService accountDataStoreService, IPaymentValidator paymentValidator) : IPaymentService
    {
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = accountDataStoreService.GetAccount(request.DebtorAccountNumber);
            var isValid = paymentValidator.IsValid(request, account);
            
            if (isValid)
            {
                account.Balance -= request.Amount;
                accountDataStoreService.UpdateAccount(account);
            }
            
            return new MakePaymentResult();;
        }
    }
}
