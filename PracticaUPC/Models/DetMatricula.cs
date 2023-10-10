using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaUPC.Models
{
    public class DetMatricula
    {
        public decimal IDMATRICULA { get; set; }
        public string? CODPRODUCTO { get; set; }
        public string? SECCION { get; set; }
        public string? GRUPO { get; set; }
        public string? USUARIOCREADOR { get; set; }
        public DateTime? FECHACREACION { get; set; }
        public string? USUARIOMODIFICACION { get; set; }

        public DateTime? FECHAMODIFICACION { get; set; }
        public string? CODLINEANEGOCIO { get; set; }

        public string? CODCURSO { get; set; }
        public Matricula Matricula { get; set; }

    }

    public class DTODetMatricula
    {
        public decimal IDMATRICULA { get; set; }
        public string? CODPRODUCTO { get; set; }
        public string? SECCION { get; set; }
        public string? GRUPO { get; set; }
        public string? USUARIOCREADOR { get; set; }
        public DateTime? FECHACREACION { get; set; }
        public string? USUARIOMODIFICACION { get; set; }

        public DateTime? FECHAMODIFICACION { get; set; }
        public string? CODLINEANEGOCIO { get; set; }

        public string? CODCURSO { get; set; }
    }
}
