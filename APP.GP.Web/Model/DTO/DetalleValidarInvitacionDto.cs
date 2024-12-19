namespace APP.GP.Web.Model.DTO
{
    public class DetalleValidarInvitacionDto
    {
        public int IdDisposicion { get; set; }

        public string? Zona { get; set; }
        public string Fila { get; set; }

        public int Columna { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string CargoActual { get; set; }

        public string NombreEscenario { get; set; }

        public string DescripcionEscenario { get; set; }

        public string NombreEvento { get; set; }

        public string DescripcionEvento { get; set; }

        public DateTime FechaEvento { get; set; }

        public bool Acceso { get; set; }

        public DateTime? FechaAcceso { get; set; }

        public string? Fotografia { get; set; }
    }
}
