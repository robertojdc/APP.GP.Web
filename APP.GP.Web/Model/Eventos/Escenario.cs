namespace APP.GP.Web.Model.Eventos;
public class Escenario
{
    public int IdEscenario { get; set; }
    public int IdEvento { get; set; }
    public string NombreEscenario { get; set; }
    public string Descripcion { get; set; }

    // Relación con Evento
    public Evento Evento { get; set; }

    // Relación con Disposiciones
    public ICollection<Disposicion> Disposiciones { get; set; }
}