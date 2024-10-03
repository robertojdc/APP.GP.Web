namespace APP.GP.Web.Model.Eventos;

public class Disposicion
{
    public int IdDisposicion { get; set; }
    public int IdEscenario { get; set; }
    public int Fila { get; set; }
    public int Columna { get; set; }
    public int? IdActor { get; set; }

    // Relación con Escenario
    //public Escenario Escenario { get; set; }

    // Relación opcional con ActorEscenario
    //public ICollection<ActorEscenario> ActorEscenarios { get; set; }

    public string NombreActor { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public string FotoBase64 { get; set; }
}
