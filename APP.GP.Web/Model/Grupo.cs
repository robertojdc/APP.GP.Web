namespace APP.GP.Web.Model;

public class Grupo
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public List<SubGrupo> SubGrupoList { get; set; }
}