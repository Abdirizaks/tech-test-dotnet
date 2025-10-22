using ClearBank.DeveloperTest.Config;
using ClearBank.DeveloperTest.Data;
using Microsoft.Extensions.Options;

namespace ClearBank.DeveloperTest.Factory;

public class DataStoreFactory(
    IOptions<DataStoreConfig> config,
    AccountDataStore mainStore,
    BackupAccountDataStore backupStore
) : IDataStoreFactory
{
    public IDataStore Create()
        => config.Value.Type == "Backup"
            ? backupStore
            : mainStore;
}