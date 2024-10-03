namespace APP.GP.Web.Model.Eventos;
public class Evento
{
    public int IdEvento { get; set; }
    public string NombreEvento { get; set; }
    public DateTime FechaEvento { get; set; }
    public string Descripcion { get; set; }

    // Relación con Escenarios
    public ICollection<Escenario> Escenarios { get; set; }
}
