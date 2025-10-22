using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validator;

public class PaymentValidator : IPaymentValidator
{
    public bool IsValid(MakePaymentRequest request, Account scheme)
    {
        return true;
    }
}