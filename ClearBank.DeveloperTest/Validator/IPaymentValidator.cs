using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validator;

public interface IPaymentValidator
{
    bool IsValid(MakePaymentRequest request, Account scheme);
}