using APP.GP.Common.Utils;
using CFE.Persistencia.ProviderData;
using Microsoft.Extensions.Options;

namespace APP.GP.FactoryData;

public class BaseRepository
{
    protected DataConsumer _ProviderDB;

    protected BaseRepository(IOptions<DatabaseOptions> options)
    {
        var databaseOptions = options.Value;
        _ProviderDB = DataFactory.GetNewInstance(databaseOptions.StringConectionDB, databaseOptions.ProviderDB);
        _ProviderDB.AutoOpenAndCloseConnectionForDataReader = true;
    }
}