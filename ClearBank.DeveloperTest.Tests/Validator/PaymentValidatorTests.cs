using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validator;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validator;

public class PaymentValidatorTests
{
    private readonly PaymentValidator _sut = new();

    [Fact]
    public void IsValid_Bacs_ShouldReturnTrue_WhenAccountAllowsBacs()
    {
        // Arrange
        var request = new MakePaymentRequest { PaymentScheme = PaymentScheme.Bacs };
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
        };

        // Act
        var result = _sut.IsValid(request, account);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsValid_Bacs_ShouldReturnFalse_WhenAccountIsNull()
    {
        // Arrange
        var request = new MakePaymentRequest { PaymentScheme = PaymentScheme.Bacs };

        // Act
        var result = _sut.IsValid(request, null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsValid_Bacs_ShouldReturnFalse_WhenAccountDoesNotAllowBacs()
    {
        // Arrange
        var request = new MakePaymentRequest { PaymentScheme = PaymentScheme.Bacs };
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps
        };

        // Act
        var result = _sut.IsValid(request, account);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_FasterPayments_ShouldReturnTrue_WhenBalanceIsSufficient_AndSchemeAllowed()
    {
        // Arrange
        var request = new MakePaymentRequest
        {
            PaymentScheme = PaymentScheme.FasterPayments,
            Amount = 100
        };

        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
            Balance = 200
        };

        // Act
        var result = _sut.IsValid(request, account);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsValid_FasterPayments_ShouldReturnFalse_WhenAccountIsNull()
    {
        // Arrange
        var request = new MakePaymentRequest { PaymentScheme = PaymentScheme.FasterPayments };

        // Act
        var result = _sut.IsValid(request, null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsValid_FasterPayments_ShouldReturnFalse_WhenBalanceIsInsufficient()
    {
        // Arrange
        var request = new MakePaymentRequest
        {
            PaymentScheme = PaymentScheme.FasterPayments,
            Amount = 500
        };

        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
            Balance = 100
        };

        // Act
        var result = _sut.IsValid(request, account);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsValid_FasterPayments_ShouldReturnFalse_WhenSchemeNotAllowed()
    {
        // Arrange
        var request = new MakePaymentRequest { PaymentScheme = PaymentScheme.FasterPayments };
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
            Balance = 1000
        };

        // Act
        var result = _sut.IsValid(request, account);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_Chaps_ShouldReturnTrue_WhenAccountLive_AndSchemeAllowed()
    {
        // Arrange
        var request = new MakePaymentRequest { PaymentScheme = PaymentScheme.Chaps };

        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
            Status = AccountStatus.Live
        };

        // Act
        var result = _sut.IsValid(request, account);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsValid_Chaps_ShouldReturnFalse_WhenAccountIsNull()
    {
        // Arrange
        var request = new MakePaymentRequest { PaymentScheme = PaymentScheme.Chaps };

        // Act
        var result = _sut.IsValid(request, null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsValid_Chaps_ShouldReturnFalse_WhenAccountNotLive()
    {
        // Arrange
        var request = new MakePaymentRequest { PaymentScheme = PaymentScheme.Chaps };
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
            Status = AccountStatus.Disabled
        };

        // Act
        var result = _sut.IsValid(request, account);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsValid_Chaps_ShouldReturnFalse_WhenSchemeNotAllowed()
    {
        // Arrange
        var request = new MakePaymentRequest { PaymentScheme = PaymentScheme.Chaps };
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
            Status = AccountStatus.Live
        };

        // Act
        var result = _sut.IsValid(request, account);

        // Assert
        result.Should().BeFalse();
    }
}