namespace APP.GP.Web.Model.DTO
{
    public class DetalleInvitacionDto
    {
        public int IdRegistroInvitacion { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? TelefonoParticular { get; set; }
        public string? CorreoElectronico { get; set; }
        public int IdActor { get; set; }
        public string? FotoBase64 { get; set; }
        public string? NombreActor { get; set; }
        public string? ApellidoPaternoActor { get; set; }
        public string? ApellidoMaternoActor { get; set; }
        public string? TelefonoPersonal { get; set; }
        public string? TelefonoLaboral { get; set; }
        public string? CargoActual { get; set; }
        public string? DomicilioLaboral { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? CorreoElectronicoActor { get; set; }
        public string? inputRedSocial { get; set; }
        public string? PaginaWeb { get; set; }

        public string? Afinidad { get; set; }

        public string? Compromiso { get; set; }

        public string? Comentarios { get; set; }
    }
}
