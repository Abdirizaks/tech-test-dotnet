using ClearBank.DeveloperTest.Config;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.Options;

namespace ClearBank.DeveloperTest.Services;

public class AccountDataStoreService(IOptions<DataStoreConfig> dataStoreConfig, IDataStore dataStore) : IAccountDataStoreService
{
    public Account GetAccount(string debtorAccountNumber)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateAccount(Account account)
    {
        throw new System.NotImplementedException();
    }
}