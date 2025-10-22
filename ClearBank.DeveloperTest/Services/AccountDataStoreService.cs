using ClearBank.DeveloperTest.Factory;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services;

public class AccountDataStoreService(IDataStoreFactory dataStore) : IAccountDataStoreService
{
    public Account GetAccount(string debtorAccountNumber)
    {
        var datastore = dataStore.Create();
        return datastore.GetAccount(debtorAccountNumber);
    }

    public void UpdateAccount(Account account)
    {
        var datastore = dataStore.Create();
        datastore.UpdateAccount(account);
    }
}