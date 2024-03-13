using GenerarPDFAPI.Models;
using GenerarPDFAPI.Negocio;
using Microsoft.AspNetCore.Mvc;

namespace GenerarPDFAPI.Controllers;

[ApiController]
[Route("api/[Controller]")] 
public class GenerarPDF {
    // Method Post
    [HttpPost]
    public ActionResult<string> GenPDF(PruebaPDF pruebaPDF)
    {
        var CertificadoUno = new CertificadoUno(pruebaPDF);
        var ruta = CertificadoUno.Generar();

        return new ObjectResult(ruta) { StatusCode = 201 };
    }
}