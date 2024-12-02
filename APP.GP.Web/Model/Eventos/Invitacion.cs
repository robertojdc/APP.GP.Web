namespace APP.GP.Web.Model.Eventos;

public class Invitacion
{
    public int IdInvitacion { get; set; }
    public int IdDisposicion { get; set; }
    public DateTime Fecha { get; set; }
    public string CodigoQR { get; set; }
    public bool Estatus { get; set; }
    public string? CuerpoCorreo { get; set; }
}
