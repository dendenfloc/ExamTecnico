using System.ComponentModel.DataAnnotations;

namespace PracticaUPC.Models
{
    public class Curso
    {
        public string? CODLINEANEGOCIO { get; set; }
        public string? CODCURSO { get; set; }
        public string? DESCCURSO { get; set; }
        public string? USUARIOCREADOR { get; set; }
        public DateTime? FECHACREACION { get; set; }
        public string? USUARIOMODIFICACION { get; set; }

        public DateTime? FECHAMODIFICACION { get; set; }
    }
    public class DTOCurso
    {
        public string? CODCURSO { get; set; }
        public string? DESCCURSO { get; set; }
    }
}
