namespace APP.GP.Web.Model.DTO
{
    public class OpcionVinculacionDto
    {
        public long IdOpcionVinculacion { get; set; }
        public long IdRegistroInvitacion { get; set; }
        public int IdActor { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string CargoActual { get; set; }
        public string TelefonoLaboral { get; set; }
        public string TelefonoParticular { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
