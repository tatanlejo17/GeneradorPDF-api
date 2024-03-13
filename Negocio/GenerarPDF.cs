namespace GenerarPDFAPI.Negocio;

using GenerarPDFAPI.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
 
public class CertificadoUno {
    // Crear propiedad para patrón Singgleton
    public static CertificadoUno? Current { get; }

    // Propiedades
    public int Identificacion { get; set; }
    public string TipoId { get; set; }
    public string Nombre { get; set; }
    public string FechaInicial { get; set; }

    // Constructor
    public CertificadoUno(PruebaPDF pruebaPDF)
    {
         Identificacion = pruebaPDF.Identificacion;
         TipoId = pruebaPDF.TipoId;
         Nombre = pruebaPDF.Nombre;
         FechaInicial = pruebaPDF.FechaInicial;
    }

    // Métodos
    public string Generar() {
        return GenerarPDF();
    }

    private string GenerarPDF() {
    
        QuestPDF.Settings.License = LicenseType.Community;

        var rutaDestino = @"C:\Users\LENOVO\Desktop\PruebaPDF\PruebaCertificado.pdf";
        var FechaAfiliacion = DateTime.Parse(FechaInicial).AddMonths(-3).ToString().Substring(1, 11);
        var FechaRetiro = "No Registrada";

        // Método para crear el PDF
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.Letter);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header()
                    .Row(tit => {
                        tit.RelativeItem().Column(c => {
                            // Logo superior
                            c.Item()
                                .Width(250)
                                .TranslateX(50, Unit.Millimetre)
                                .Image("Assets/Image/LogoSuperior.webp")
                                .UseOriginalImage()
                                .FitWidth()
                                .WithCompressionQuality(ImageCompressionQuality.High);

                            c.Item()
                                .PaddingTop(20, Unit.Millimetre)
                                .AlignCenter()
                                .Text("EL PROGRAMA DE EPS DE LA CAJA DE COMPENSACIÓN FAMILIAR COMPENSAR\nNIT: 860.066.942-7")
                                .SemiBold().FontSize(12).FontColor(Colors.Black);
                        });
                    });
                    
                
                page.Content()
                    .PaddingVertical(15, Unit.Millimetre)
                    .Row(f => {
                        f.RelativeItem().Column(c => {
                            c.Item()
                                .Text($"Que el(la) señor(a) {Nombre} identificado(a) con {TipoId} - {Identificacion}, se encuentra Afiliado al Plan de Beneficios de Salud PBS, de la EPS Compensar por la empresa Compensar con NIT 860010595, en calidad de dependiente según información contenida a la fecha en nuestra base de datos.");

                            c.Item()
                                .Height(1, Unit.Centimetre);

                            c.Item().Table(t1 => {
                                t1.ColumnsDefinition(c => {
                                    c.RelativeColumn(2f);
                                });

                                t1.Header(header => {
                                    header.Cell()
                                        .Border(1)
                                        .BorderColor("#444")
                                        .Background("#EEEDEB")
                                        .AlignCenter()
                                        .Padding(1)
                                        .Text("Información Afiliación")
                                        .Bold()
                                        .FontSize(10);
                                });

                                c.Item().Table(t2 => {
                                    t2.ColumnsDefinition(c2 => {
                                        c2.RelativeColumn();
                                        c2.RelativeColumn();
                                    });

                                    t2.Header(header => {
                                        header.Cell()
                                            .Border(1)
                                            .BorderColor("#444")
                                            .Background("#EEEDEB")
                                            .AlignCenter()
                                            .Padding(1)
                                            .Text("Fecha Afiliación")
                                            .Bold()
                                            .FontSize(10);

                                        header.Cell()
                                            .Border(1)
                                            .BorderColor("#444")
                                            .Background("#EEEDEB")
                                            .AlignCenter()
                                            .Padding(1)
                                            .Text("Fecha Retiro")
                                            .Bold()
                                            .FontSize(10);
                                    });

                                    t2.Cell()
                                        .Border(1)
                                        .BorderColor("#444")
                                        .Background(Colors.White)
                                        .AlignCenter()
                                        .Padding(1)
                                        .Text($"{FechaAfiliacion}")
                                        .FontSize(10);

                                    t2.Cell()
                                        .Border(1)
                                        .BorderColor("#444")
                                        .Background(Colors.White)
                                        .AlignCenter()
                                        .Padding(1)
                                        .Text($"{FechaRetiro}")
                                        .FontSize(10);
                                });
                            });

                            c.Item()
                                .Height(1, Unit.Centimetre);

                            c.Item()
                                .Text($"El presente certificado se expíde a solicitud del (la) interesado(a) a los 11 días del mes de Marzo de 2024.")
                                /* .FontSize(9) */
                                .Italic();

                            c.Item()
                                .Height(5, Unit.Millimetre);

                            c.Item()
                                .Text($"Información sujeta a verificación por parte de Compensar EPS, cualquier aclaración con gusto será atendida en la línea 4441234. Documento no válido como autorización de traslado ni aclaración de multiafiliación en el SGSSS.")
                                /* .FontSize(9) */
                                .Italic();

                            c.Item()
                                .Height(2, Unit.Centimetre);

                            c.Item()
                                .Row(r => {
                                    r.RelativeItem().Column(c => {
                                        c.Item()
                                            .Text($"Cordialmente");
                                        
                                        c.Item()
                                            .Text($"COMPENSAR EPS")
                                            .SemiBold()
                                            .FontSize(11)
                                            .FontColor("#E8751A");
                                    });
                                });

                            c.Item()
                                .Height(5, Unit.Millimetre);

                            c.Item()
                                .Row(r => {
                                    r.RelativeItem().Column(c => {
                                        c.Item()
                                            .Text($"Elaboró");
                                        
                                        c.Item()
                                            .Text($"Unysis")
                                            .SemiBold()
                                            .FontSize(13)
                                            .FontColor("#0D9276");
                                    });
                                });
                        });
                    });
                
                page.Footer()
                    .AlignLeft()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                        x.DefaultTextStyle( x => x.FontSize(8));
                    });
            }); 
        })
        .GeneratePdf(rutaDestino);

        return rutaDestino;
    }
}