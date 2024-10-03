namespace APP.GP.Web.Model.Eventos;

public class ActorEscenario
{
    public int? IdActorEscenario { get; set; }
    public int IdActor { get; set; }
    public int IdEscenario { get; set; }
    public int? IdDisposicion { get; set; }

    // Relación con Escenario
    public Escenario? Escenario { get; set; }

    // Relación con Disposicion
    public Disposicion? Disposicion { get; set; }

    // Relación con Actor (este modelo lo tienes ya definido)
    public Actor? Actor { get; set; }
}
