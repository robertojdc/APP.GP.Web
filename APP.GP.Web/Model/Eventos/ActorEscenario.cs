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

public class ActorEscenarioSimpl
{
    public int? IdActor { get; set; }
    public string? Nombre { get; set; }
    public string? ApellidoPaterno { get; set; }
    public string? ApellidoMaterno { get; set; }
    public string? CargoActual { get; set; }
    public string? Fila { get; set; }
    public int? Columna { get; set; }
    public int IdDisposicion { get; set; }


}
