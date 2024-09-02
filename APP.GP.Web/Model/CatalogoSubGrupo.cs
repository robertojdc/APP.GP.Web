namespace APP.GP.Web.Model
{
    public class CatalogoSubGrupo
    {
        public CatalogoSubGrupo()
        {
            IdSubGrupo = 0;
            IdGrupo = 0;
            Nombre = string.Empty;
            NombreGrupo = string.Empty;
        }

        public int IdSubGrupo { get; set; }

        public string Nombre { get; set; }

        public int IdGrupo { get; set; }

        public string? NombreGrupo { get; set; }
    }
}
