using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validator;

public class PaymentValidator : IPaymentValidator
{
    public bool IsValid(MakePaymentRequest request, Account account)
    {
        switch (request.PaymentScheme)
        {
            case PaymentScheme.Bacs:
                if (account == null)
                {
                    return false;
                }
                if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
                {
                    return false;
                }
                break;

            case PaymentScheme.FasterPayments:
                if (account == null)
                {
                    return false;
                }
                if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
                {
                    return false;
                }
                if (account.Balance < request.Amount)
                {
                    return false;
                }
                break;

            case PaymentScheme.Chaps:
                if (account == null)
                {
                    return false;
                }
                if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
                {
                    return false;
                }
                if (account.Status != AccountStatus.Live)
                {
                    return false;
                }
                break;
        }

        return true;
    }
}