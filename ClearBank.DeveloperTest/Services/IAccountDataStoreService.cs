using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services;

public interface IAccountDataStoreService
{
    Account GetAccount(string debtorAccountNumber);
    void UpdateAccount(Account account);
}