namespace APP.GP.Web.Model.Eventos;
public class EnvioInvitacion
{
    public int IdEnvioInvitacion { get; set; }
    public int IdInvitacion { get; set; }
    public DateTime FechaEnvio { get; set; }
    public int TipoEnvio { get; set; }
    public string DatosEnvio { get; set; }
}
