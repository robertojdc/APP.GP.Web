using APP.GP.Entities;
using APP.GP.FactoryData;

namespace APP.GP.Business.Facade;

public sealed class TableroFacade
{
    private readonly ActorRepositorio _tableroRepositorio;

    public TableroFacade(ActorRepositorio tableroRepositorio)
    {
        _tableroRepositorio = tableroRepositorio;
    }

    public int AddTablero(Actor _request)
    {
        return _tableroRepositorio.AddActor(_request);
    }
}