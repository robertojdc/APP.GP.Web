namespace APP.GP.Web.Model.Filtros
{
    public class RegistroInvitacionRequest
    {
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? TelefonoPersonal { get; set; }
        public string? CorreoElectronico { get; set; }
        public int? Vinculado { get; set; }
    }
}
