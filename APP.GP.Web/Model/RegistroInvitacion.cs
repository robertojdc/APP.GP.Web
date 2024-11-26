namespace APP.GP.Web.Model;

public class RegistroInvitacion
{
    public int? IdRegistroInvitacion { get; set; }
    public string? Nombre { get; set; }
    public string? ApellidoPaterno { get; set; }
    public string? ApellidoMaterno { get; set; }
    public string? NombreCompleto { get; set; }
    public string? TelefonoPersonal { get; set; }
    public string? CorreoElectronico { get; set; }
    public string? Cargo { get; set; }
    public int? IdActor { get; set; }
    public string? NombreCompletoActor { get; set; }
    public string? CargoActual { get; set; }
    public string? TelefonoParticular { get; set; }
    public string? TelefonoLaboral { get; set; }
    public string? CorreoElectronicoActor { get; set; }
    public bool? Propuesta { get; set; }
    public bool? Vinculado { get; set; }
    public int? OpcionVinculacion { get; set; }
}
