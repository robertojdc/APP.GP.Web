namespace APP.GP.Web.Model;

public class Categoria
{
    public Categoria()
    {
        SubCategorias = new();
    }
    public int? IdSubGrupo { get; set; }
    public int? IdCategoria { get; set; }
    public int? IdSubcategoria { get; set; }
    public string? Descripcion { get; set; }
    public int? IdGrupo { get; set; }
    public List<Categoria>? SubCategorias { get; set; }
}