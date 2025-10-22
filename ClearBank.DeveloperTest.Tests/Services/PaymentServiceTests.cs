using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validator;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services;

public class PaymentServiceTests
{
    [Fact]
    public void MakePayment_ShouldLookupAccount_From_CorrectDataStore()
    {
        // Arrange
        var mockDataStore = Substitute.For<IAccountDataStoreService>();
        var mockPaymentValidator = Substitute.For<IPaymentValidator>();
        
        var expectedAccount = new Account { AccountNumber = "12345" };
        mockDataStore.GetAccount("12345").Returns(expectedAccount);

        var request = new MakePaymentRequest
        {
            DebtorAccountNumber = "12345",
            PaymentScheme = PaymentScheme.Bacs,
            Amount = 100
        };

        var sut = new PaymentService(mockDataStore, mockPaymentValidator);

        // Act
        var result = sut.MakePayment(request);

        // Assert
        mockDataStore.Received(1).GetAccount("12345");
        result.Success.Should().BeTrue();
    }

    [Fact]
    public void MakePayment_ShouldReturnFalse_When_AccountIsDisabled()
    {
        // Arrange
        var mockDataStore = Substitute.For<IAccountDataStoreService>();
        var mockPaymentValidator = Substitute.For<IPaymentValidator>();
        
        var validAccount = new Account
        {
            AccountNumber = "ABC123",
            Balance = 1000,
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
            Status = AccountStatus.Disabled
        };
        
        var request = new MakePaymentRequest
        {
            DebtorAccountNumber = "00001",
            Amount = 100,
            PaymentScheme = PaymentScheme.Chaps,
        };
        
        mockDataStore.GetAccount(Arg.Any<string>()).Returns(validAccount);
        
        // Act
        var sut = new PaymentService(mockDataStore, mockPaymentValidator);
        var result = sut.MakePayment(request);

        // Assert
        result.Success.Should().BeFalse();
        mockDataStore.Received(1).GetAccount(Arg.Any<string>());
        mockPaymentValidator.Received(1).IsValid(Arg.Any<MakePaymentRequest>(), Arg.Any<Account>());
    }

    [Fact]
    public void MakePayment_ShouldDeductAmount_And_UpdateAccount_When_Valid()
    {
        // Arrange
        var mockDataStore = Substitute.For<IAccountDataStoreService>();
        var mockPaymentValidator = Substitute.For<IPaymentValidator>();
        
        var validAccount = new Account
        {
            AccountNumber = "ABC123",
            Balance = 1000,
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
            Status = AccountStatus.Live
        };

        var request = new MakePaymentRequest
        {
            DebtorAccountNumber = "ABC123",
            Amount = 250,
            PaymentScheme = PaymentScheme.FasterPayments
        };

        mockDataStore.GetAccount(Arg.Any<string>()).Returns(validAccount);
        
        var sut = new PaymentService(mockDataStore, mockPaymentValidator);

        // Act
        var result = sut.MakePayment(request);

        // Assert
        result.Success.Should().BeTrue();
        validAccount.Balance.Should().Be(750);
        mockDataStore.Received(1).GetAccount(Arg.Any<string>());
        mockDataStore.Received(1).UpdateAccount(Arg.Is<Account>(a => a.Balance == 750));
        mockPaymentValidator.Received(1).IsValid(Arg.Any<MakePaymentRequest>(), Arg.Any<Account>());
    }
}