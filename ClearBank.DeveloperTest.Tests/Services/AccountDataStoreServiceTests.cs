using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factory;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services;

public class AccountDataStoreServiceTests
{
    private readonly IDataStoreFactory _dataStoreFactory;
    private readonly IDataStore _dataStore;
    private readonly AccountDataStoreService _sut;

    public AccountDataStoreServiceTests()
    {
        _dataStoreFactory = Substitute.For<IDataStoreFactory>();
        _dataStore = Substitute.For<IDataStore>();
        
        _dataStoreFactory.Create().Returns(_dataStore);

        _sut = new AccountDataStoreService(_dataStoreFactory);
    }

    [Fact]
    public void GetAccount_ShouldReturnAccountFromDataStore()
    {
        // Arrange
        var expectedAccount = new Account { AccountNumber = "12345", Balance = 100 };
        _dataStore.GetAccount("12345").Returns(expectedAccount);

        // Act
        var result = _sut.GetAccount("12345");

        // Assert
        result.Should().Be(expectedAccount);
        _dataStore.Received(1).GetAccount("12345");
        _dataStoreFactory.Received(1).Create();
    }

    [Fact]
    public void UpdateAccount_ShouldCallUpdateOnDataStore()
    {
        // Arrange
        var account = new Account { AccountNumber = "67890", Balance = 500 };

        // Act
        _sut.UpdateAccount(account);

        // Assert
        _dataStore.Received(1).UpdateAccount(account);
        _dataStoreFactory.Received(1).Create();
    }
}