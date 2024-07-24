using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APP.GP.Web.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    public JsonResult GetPopupContent(int popupId)
    {
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
        else
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