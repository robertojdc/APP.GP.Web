using APP.GP.Common.Utils;
using APP.GP.Entities;
using CFE.Persistencia.ProviderData;
using Microsoft.Extensions.Options;
using System.Data;

namespace APP.GP.FactoryData;

public class ActorRepositorio : BaseRepository
{
    private readonly string _providerDB;

    public ActorRepositorio(IOptions<DatabaseOptions> options) : base(options)
    {
        _providerDB = options.Value.ProviderDB;
    }

    public int AddActor(Actor entity)
    {
        IDbDataParameter idActor = DataFactory.GetObjParameter(_providerDB, "@IdActor", DbType.Int32, ParameterDirection.Output, -1);
        _ProviderDB.ExecuteNonQuery("spInsActor",
            [
                idActor,
                DataFactory.GetObjParameter(_providerDB, "@Nombre", DbType.String, entity.Nombre),
                DataFactory.GetObjParameter(_providerDB, "@Apellido", DbType.String, entity.Apellido),
                DataFactory.GetObjParameter(_providerDB, "@FechaNacimiento", DbType.DateTime2, entity.FechaNacimiento),
                DataFactory.GetObjParameter(_providerDB, "@Observaciones", DbType.String, entity.Observaciones)
            ]);
        return Convert.ToInt32(idActor.Value);
    }
}