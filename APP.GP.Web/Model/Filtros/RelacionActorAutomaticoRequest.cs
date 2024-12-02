namespace APP.GP.Web.Model.Filtros
{
    public class RelacionActorAutomaticoRequest
    {
        public int IdUsuario { get; set; }
        public bool Todos { get; set; }

        public bool Nombre { get; set; }

        public bool Apellidos { get; set; }

        public bool CorreoElectronico { get; set; }

        public bool Telefono { get; set; }
    }
}
