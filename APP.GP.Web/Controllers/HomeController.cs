using APP.GP.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APP.GP.Web.Controllers;

public class HomeController : Controller
{
    private readonly GrupoService _grupoService;

    public HomeController(GrupoService grupoService)
    {
        _grupoService = grupoService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View("Tablero");
    }

    [HttpGet]
    public IActionResult Place()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Default()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Tablero()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Invite()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Actor()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ObtenerGrupos()
    {
        var grupos = await _grupoService.GetAllTablero();
        return Json(grupos);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ObtenerCategoriaGrupo(int idSubGrupo)
    {
        var grupos = await _grupoService.GetCategoriasAsync(idSubGrupo);
        return Json(grupos);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ObtenerSubCategoria(int idCategoria)
    {
        var grupos = await _grupoService.GetSubCategoriasAsync(idCategoria);
        return Json(grupos);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ObtenerActoresCategoria()
    {
        var grupos = await _grupoService.GetCatalogoGruposAsync();
        return Json(grupos);
    }

    public JsonResult GetContentData()
    {
        //Estructura inicial
        var data = new
        {
            titleGroup = "PODERES",
            colClasses = new[] { "col-xl-9", "col-lg-12", "col-md-12", "col-sm-12", "col-12" },
            sections = new[]
            {
                new
                {
                    header = "FEDERAL",
                    colDivClasses = new[] { "col-xl-4", "col-lg-4", "col-md-4", "col-sm-6", "col-12" },
                    columns = new[] { "Ejecutivo", "Legislativo", "Judicial", "Autónomos" },
                    rows = new[]
                    {
                        new[] { new { text = "Oficina de la presidencia", popupId = 1 }, new { text = "Diputados", popupId = 1 }, new { text = "SCJN", popupId = 1 }, new { text = "CNDH", popupId = 1 } },
                        new[] { new { text = "APF", popupId = 1 }, new { text = "Senadores", popupId = 1 }, new { text = "PJF", popupId = 1 }, new { text = "INEGI", popupId = 1 } },
                        new[] { new { text = "Administración Pública Federal", popupId = 9 }, new { text = "ASF", popupId = 1 }, new { text = "CJF", popupId = 1 }, new { text = "UNAM", popupId = 1 } }
                    }
                },
                new
                {
                    header = "ESTATAL",
                    colDivClasses = new[] { "col-xl-4", "col-lg-4", "col-md-4", "col-sm-6", "col-12" },
                    columns = new[] { "Ejecutivo", "Legislativo", "Judicial", "Autónomos" },
                    rows = new[]
                    {
                        new[] { new { text = "Oficina de la presidencia", popupId = 1 }, new { text = "Diputados", popupId = 1 }, new { text = "SCJN", popupId = 1 }, new { text = "CNDH", popupId = 1 } },
                        new[] { new { text = "APF", popupId = 1 }, new { text = "Senadores", popupId = 1 }, new { text = "PJF", popupId = 9 }, new { text = "INEGI", popupId = 1 } },
                        new[] { new { text = "APF", popupId = 1 }, new { text = "ASF", popupId = 1 }, new { text = "CJF", popupId = 1 }, new { text = "UNAM", popupId = 1 } }
                    }
                },
                new
                {
                    header = "MUNICIPAL",
                    colDivClasses = new[] { "col-xl-4", "col-lg-4", "col-md-4", "col-sm-6", "col-12" },
                    columns = new[] { "Ejecutivo", "Legislativo", "Judicial", "Autónomos" },
                    rows = new[]
                    {
                        new[] { new { text = "Oficina de la presidencia", popupId = 1 }, new { text = "Diputados", popupId = 1 }, new { text = "SCJN", popupId = 1 }, new { text = "CNDH", popupId = 1 } },
                        new[] { new { text = "APF", popupId = 1 }, new { text = "Senadores", popupId = 1 }, new { text = "PJF", popupId = 1 }, new { text = "INEGI", popupId = 1 } },
                        new[] { new { text = "APF", popupId = 1 }, new { text = "ASF", popupId = 1 }, new { text = "CJF", popupId = 1 }, new { text = "UNAM", popupId = 1 } }
                    }
                }
            },
            titleGroupState = "GOBIERNOS ESTATALES",
            stateColClasses = new[] { "col-xl-3", "col-lg-12", "col-md-12", "col-sm-12", "col-12" },
            statePowers = new
            {
                header = "PODERES",
                colDivClasses = new[] { "col-12" },
                columns = new[] { "Ejecutivo", "Legislativo", "Judicial" },
                rows = new[]
                {
                    new[] { new { text = "Oficina de la presidencia", popupId = 37 }, new { text = "Diputados", popupId = 1 }, new { text = "SCJN", popupId = 1 } },
                    new[] { new { text = "APF", popupId = 1 }, new { text = "Senadores", popupId = 1 }, new { text = "PJF", popupId = 1 } },
                    new[] { new { text = "APF", popupId = 1 }, new { text = "ASF", popupId = 1 }, new { text = "CJF", popupId = 1 } }
                }
            },
            otherSections = new[]
            {
                new
                {
                    titleGroup = "ORGANISMOS ELECTORALES",
                    colClasses = new[] { "col-xl-3", "col-lg-4", "col-md-6", "col-sm-6", "col-12" },
                    colDivClasses = new[] { "col-12", "mb-3" },
                    header = "",
                    columns = new[] { "", "", "", "" },
                    rows = new[]
                    {
                        new[] { new { text = "Nacional", popupId = 1 }, new { text = "INE", popupId = 1 }, new { text = "Tribunal Electoral", popupId = 1 }, new { text = "FEPADE", popupId = 1 } },
                        new[] { new { text = "Estatal", popupId = 1 }, new { text = "OPLE", popupId = 1 }, new { text = "TRIFE", popupId = 1 }, new { text = "", popupId = 1 } },
                        new[] { new { text = "Diputados", popupId = 53 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 } }
                    }
                },
                new
                {
                    titleGroup = "SECTOR POLÍTICO",
                    colClasses = new[] { "col-xl-3", "col-lg-4", "col-md-6", "col-sm-6", "col-12" },
                    colDivClasses = new[] { "col-12", "mb-3" },
                    header = "",
                    columns = new[] { "", "", "", "" },
                    rows = new[]
                    {
                        new[] { new { text = "Nacional", popupId = 1 }, new { text = "INE", popupId = 1 }, new { text = "Tribunal Electoral", popupId = 1 }, new { text = "FEPADE", popupId = 1 } },
                        new[] { new { text = "Estatal", popupId = 1 }, new { text = "OPLE", popupId = 1 }, new { text = "TRIFE", popupId = 1 }, new { text = "", popupId = 1 } },
                        new[] { new { text = "Diputados", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 } }
                    }
                },
                new
                {
                    titleGroup = "RELACIONES INTERNACIONALES",
                    colClasses = new[] { "col-xl-3", "col-lg-4", "col-md-6", "col-sm-6", "col-12" },
                    colDivClasses = new[] { "col-12", "mb-3" },
                    header = "",
                    columns = new[] { "", "", "", "" },
                    rows = new[]
                    {
                        new[] { new { text = "Nacional", popupId = 1 }, new { text = "INE", popupId = 1 }, new { text = "Tribunal Electoral", popupId = 1 }, new { text = "FEPADE", popupId = 1 } },
                        new[] { new { text = "Estatal", popupId = 1 }, new { text = "OPLE", popupId = 1 }, new { text = "TRIFE", popupId = 1 }, new { text = "", popupId = 1 } },
                        new[] { new { text = "Diputados", popupId = 69 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 } }
                    }
                },
                new
                {
                    titleGroup = "MIGRANTES",
                    colClasses = new[] { "col-xl-3", "col-lg-4", "col-md-6", "col-sm-6", "col-12" },
                    colDivClasses = new[] { "col-12", "mb-3" },
                    header = "",
                    columns = new[] { "", "", "", "" },
                    rows = new[]
                    {
                        new[] { new { text = "Nacional", popupId = 1 }, new { text = "INE", popupId = 1 }, new { text = "Tribunal Electoral", popupId = 1 }, new { text = "FEPADE", popupId = 1 } },
                        new[] { new { text = "Estatal", popupId = 1 }, new { text = "OPLE", popupId = 1 }, new { text = "TRIFE", popupId = 1 }, new { text = "", popupId = 1 } },
                        new[] { new { text = "Diputados", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 } }
                    }
                },
                new
                {
                    titleGroup = "ORGANISMOS INTERNACIONALES",
                    colClasses = new[] { "col-xl-3", "col-lg-4", "col-md-6", "col-sm-6", "col-12" },
                    colDivClasses = new[] { "col-12", "mb-3" },
                    header = "",
                    columns = new[] { "", "", "", "" },
                    rows = new[]
                    {
                        new[] { new { text = "Nacional", popupId = 1 }, new { text = "INE", popupId = 1 }, new { text = "Tribunal Electoral", popupId = 1 }, new { text = "FEPADE", popupId = 1 } },
                        new[] { new { text = "Estatal", popupId = 1 }, new { text = "OPLE", popupId = 1 }, new { text = "TRIFE", popupId = 1 }, new { text = "", popupId = 1 } },
                        new[] { new { text = "Diputados", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 } }
                    }
                },
                new
                {
                    titleGroup = "SECTOR PRODUCTIVO",
                    colClasses = new[] { "col-xl-3", "col-lg-4", "col-md-6", "col-sm-6", "col-12" },
                    colDivClasses = new[] { "col-12", "mb-3" },
                    header = "",
                    columns = new[] { "", "", "", "" },
                    rows = new[]
                    {
                        new[] { new { text = "Nacional", popupId = 1 }, new { text = "INE", popupId = 1 }, new { text = "Tribunal Electoral", popupId = 1 }, new { text = "FEPADE", popupId = 1 } },
                        new[] { new { text = "Estatal", popupId = 1 }, new { text = "OPLE", popupId = 1 }, new { text = "TRIFE", popupId = 1 }, new { text = "", popupId = 1 } },
                        new[] { new { text = "Diputados", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 } }
                    }
                },
                new
                {
                    titleGroup = "MEDIOS DE COMUNICACIÓN",
                    colClasses = new[] { "col-xl-3", "col-lg-4", "col-md-6", "col-sm-6", "col-12" },
                    colDivClasses = new[] { "col-12", "mb-3" },
                    header = "",
                    columns = new[] { "", "", "", "" },
                    rows = new[]
                    {
                        new[] { new { text = "", popupId = 1 }, new { text = "Locales", popupId = 1 }, new { text = "Nacionales", popupId = 1 }, new { text = "Internacionales", popupId = 1 } },
                        new[] { new { text = "Impresos", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 } },
                        new[] { new { text = "Electrónicos", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 } },
                        new[] { new { text = "Digitales", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 }, new { text = "", popupId = 1 } }
                    }
                }
            }
        };

        return Json(data);
    }

    public JsonResult GetPopupContent(int popupId)
    {
        //Segundo nivel de detalle
        if (popupId == 2)
        {
            var data = new
            {
                titleGroup = "Información de Contacto de El Sol de Puebla",
                details = new[]
                {
                    new { label = "Nombre", value = "El Sol de Puebla" },
                    new { label = "Domicilio", value = "3 Oriente 201, col. Centro" },
                    new { label = "Teléfono", value = "5 14 33 00" },
                    new { label = "Aniversario", value = "05-may" },
                    new { label = "Digital", value = "Web: <a href='https://www.elsoldepuebla.com.mx/'>https://www.elsoldepuebla.com.mx/</a><br>Twitter: @@elsoldepuebla1<br>Facebook: elsoldpuebla" }
                },
                contactsTitle = "Contactos",
                contacts = new[]
                {
                    new { ProductID = 1, ProductName = "Serafín Arellano Salazar", Description = "Director" },
                    new { ProductID = 2, ProductName = "Jorge Rodríguez Corona", Description = "Sub Director" },
                    new { ProductID = 3, ProductName = "Salvador Ríos Gómez", Description = "Jefe de Información" },
                    new { ProductID = 4, ProductName = "Elena Domínguez", Description = "Reportera" },
                    new { ProductID = 5, ProductName = "Mary Carmen Ávila", Description = "Reportera" },
                    new { ProductID = 6, ProductName = "Karen Meza", Description = "Reportera" }
                }
            };

            return Json(data);
        }
        else // Primer nivel de detalle
        {
            var data = new
            {
                titleGroup = "IMPRESOS",
                sections = new[]
                {
                    new
                    {
                        header = "LOCALES",
                        rows = new[]
                        {
                            new[]
                            {
                                new { text = "El Sol de Puebla", popupId = 101 },
                                new { text = "El Heraldo de Puebla", popupId = 102 },
                                new { text = "La Jornada de Oriente", popupId = 103 },
                                new { text = "Milenio", popupId = 104 }
                            },
                            new[]
                            {
                                new { text = "Cambio", popupId = 105 },
                                new { text = "Intolerancia", popupId = 106 },
                                new { text = "La Opinión Diario de la Mañana", popupId = 107 },
                                new { text = "Diario 24 Horas", popupId = 108 }
                            },
                            new[]
                            {
                                new { text = "El Popular", popupId = 109 },
                                new { text = "Puntual", popupId = 110 },
                                new { text = "Contra Réplica", popupId = 111 },
                                new { text = "", popupId = 0 }
                            }
                        }
                    }
                }
            };

            return Json(data);
        }
    }

    public JsonResult GetContactDetails(int productId)
    {
        var contacts = new[]
        {
            new { ProductID = 1, ProductName = "Serafín Arellano Salazar", Paterno = "Arellano", Materno = "Salazar", Cumple = "02-oct", Partido = "Ninguno", Grupo = "AArmenta", Afinidad = "Aliado", TelefonoLaboral = "2224549030", TelefonoCasa = "", Celular1 = "22.24.69.09.95", Correo = "gerardo.jean@gmail.com", Correo2 = "gjeanc@televisa.com.mx", DomicilioOficina = "Calle Zacatlán No.42, Col. La Paz", DomicilioResidencia = "Av. 33 Pte. 169, Villa la Noria, 72410 Heroica Puebla de Zaragoza, Pue.", DomicilioDescanso = "C. Pedro de Alvarado, San Jose, 62550 Cuernavaca, Mor.", Description = "Director - El Sol de Puebla" },
            new { ProductID = 2, ProductName = "Jorge Rodríguez Corona", Paterno = "", Materno = "", Cumple = "", Partido = "", Grupo = "", Afinidad = "", TelefonoLaboral = "", TelefonoCasa = "", Celular1 = "", Correo = "", Correo2 = "", DomicilioOficina = "", DomicilioResidencia = "", DomicilioDescanso = "", Description = "Sub Director" },
            new { ProductID = 3, ProductName = "Salvador Ríos Gómez", Paterno = "", Materno = "", Cumple = "", Partido = "", Grupo = "", Afinidad = "", TelefonoLaboral = "", TelefonoCasa = "", Celular1 = "", Correo = "", Correo2 = "", DomicilioOficina = "", DomicilioResidencia = "", DomicilioDescanso = "", Description = "Jefe de Información" },
            new { ProductID = 4, ProductName = "Elena Domínguez", Paterno = "", Materno = "", Cumple = "", Partido = "", Grupo = "", Afinidad = "", TelefonoLaboral = "", TelefonoCasa = "", Celular1 = "", Correo = "", Correo2 = "", DomicilioOficina = "", DomicilioResidencia = "", DomicilioDescanso = "", Description = "Reportera" },
            new { ProductID = 5, ProductName = "Mary Carmen Ávila", Paterno = "", Materno = "", Cumple = "", Partido = "", Grupo = "", Afinidad = "", TelefonoLaboral = "", TelefonoCasa = "", Celular1 = "", Correo = "", Correo2 = "", DomicilioOficina = "", DomicilioResidencia = "", DomicilioDescanso = "", Description = "Reportera" },
            new { ProductID = 6, ProductName = "Karen Meza", Paterno = "", Materno = "", Cumple = "", Partido = "", Grupo = "", Afinidad = "", TelefonoLaboral = "", TelefonoCasa = "", Celular1 = "", Correo = "", Correo2 = "", DomicilioOficina = "", DomicilioResidencia = "", DomicilioDescanso = "", Description = "Reportera" }
        };

        var contact = contacts.FirstOrDefault(c => c.ProductID == productId);
        return Json(contact);
    }
}