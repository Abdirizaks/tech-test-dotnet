using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validator;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services;

public class PaymentServiceTests
{
    private readonly IAccountDataStoreService _accountDataStoreService;
    private readonly IPaymentValidator _paymentValidator;
    private readonly PaymentService _sut;

    public PaymentServiceTests()
    {
        _accountDataStoreService = Substitute.For<IAccountDataStoreService>();
        _paymentValidator = Substitute.For<IPaymentValidator>();
        _sut = new PaymentService(_accountDataStoreService, _paymentValidator);
    }

    [Fact]
    public void MakePayment_ShouldDeductAmountAndUpdateAccount_WhenValidationPasses()
    {
        // Arrange
        var request = new MakePaymentRequest
        {
            DebtorAccountNumber = "42",
            Amount = 50
        };

        var account = new Account
        {
            AccountNumber = "42",
            Balance = 100
        };

        _accountDataStoreService.GetAccount("42").Returns(account);
        _paymentValidator.IsValid(request, account).Returns(true);

        // Act
        var result = _sut.MakePayment(request);

        // Assert
        account.Balance.Should().Be(50);
        _accountDataStoreService.Received(1).UpdateAccount(account);
        _paymentValidator.Received(1).IsValid(request, account);
        result.Should().NotBeNull();
    }

    [Fact]
    public void MakePayment_ShouldNotUpdateAccount_WhenValidationFails()
    {
        // Arrange
        var request = new MakePaymentRequest
        {
            DebtorAccountNumber = "118-118",
            Amount = 20
        };

        var account = new Account
        {
            AccountNumber = "118-118",
            Balance = 200
        };

        _accountDataStoreService.GetAccount("118-118").Returns(account);
        _paymentValidator.IsValid(request, account).Returns(false);

        // Act
        var result = _sut.MakePayment(request);

        // Assert
        account.Balance.Should().Be(200);
        _accountDataStoreService.DidNotReceive().UpdateAccount(Arg.Any<Account>());
        result.Should().NotBeNull();
    }
}