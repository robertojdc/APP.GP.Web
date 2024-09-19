namespace APP.GP.Web.Model;

using System;
using System.ComponentModel.DataAnnotations;

public class Actor
{
    public Actor()
    {
        SubCategorias = new();
    }
    public int? IdActor { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string? Nombre { get; set; }
    public string? ApellidoPaterno { get; set; }
    public string? ApellidoMaterno { get; set; }

    public string? CargoActual { get; set; }

    [DataType(DataType.Date)]
    public DateTime? FechaNacimiento { get; set; }

    public string? DomicilioLaboral { get; set; }

    public string? DomicilioParticular { get; set; }

    [Phone]
    public string? TelefonoLaboral { get; set; }

    [Phone]
    public string? TelefonoPersonal { get; set; }

    [EmailAddress]
    public string? CorreoElectronico { get; set; }

    [Url]
    public string? PaginaWeb { get; set; }

    public string? Afinidad { get; set; }

    public string? Compromiso { get; set; }

    public string? Comentarios { get; set; }

    public IFormFile? Foto { get; set; }
    public string? FotoBase64 { get; set; }

    public List<Categoria> SubCategorias { get; set; }
    public string? inputRedSocial { get; set; }
    public List<string>? RedesSociales { get; set; }
    public string? Grupos { get; set; }
}
