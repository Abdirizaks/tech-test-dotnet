using ClearBank.DeveloperTest.Config;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services;

public class AccountDataStoreServiceTests
{
    [Fact]
    public void GetMainAccount_When_AccountIdExists_ReturnAccount()
    {
        // Arrange
        var dataStoreConfig = Options.Create(new DataStoreConfig(){Type="Main"});
        var dataStore = new AccountDataStore();

        var sut = new AccountDataStoreService(dataStoreConfig, dataStore);
        
        // Act
        var result = sut.GetAccount("12345");

        // Assert
        result.Should().NotBeNull();
    }
    
    [Fact]
    public void GetBackUpAccount_When_AccountIdExists_ReturnAccount()
    {
        // Arrange
        var dataStoreConfig = Options.Create(new DataStoreConfig(){Type="Backup"});
        var dataStore = new BackupAccountDataStore();

        var sut = new AccountDataStoreService(dataStoreConfig, dataStore);
        
        // Act
        var result = sut.GetAccount("12345");

        // Assert
        result.Should().NotBeNull();
        
    }
    
    [Fact]
    public void UpdateMainAccount_When_AccountIdExists_ReturnAccount()
    {
        // Arrange
        var dataStoreConfig = Options.Create(new DataStoreConfig(){Type="Main"});
        var dataStore = Substitute.For<IDataStore>();

        var sut = new AccountDataStoreService(dataStoreConfig, dataStore);
        
        // Act
        sut.UpdateAccount(Arg.Any<Account>());

        // Assert
        dataStore.Received(1).UpdateAccount(Arg.Any<Account>());
    }
    
    [Fact]
    public void UpdateBackupAccount_When_AccountIdExists_ReturnAccount()
    {
        // Arrange
        var dataStoreConfig = Options.Create(new DataStoreConfig(){Type="Backup"});
        var dataStore = Substitute.For<IDataStore>();

        var sut = new AccountDataStoreService(dataStoreConfig, dataStore);
        
        // Act
        sut.UpdateAccount(Arg.Any<Account>());

        // Assert
        dataStore.Received(1).UpdateAccount(Arg.Any<Account>());
    }
}