namespace GenerarPDFAPI.Models;

public class PruebaPDF {
    public int Identificacion { get; set; }
    public string TipoId { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string FechaInicial { get; set; } = string.Empty;
}