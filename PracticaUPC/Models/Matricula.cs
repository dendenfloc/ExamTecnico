using System.ComponentModel.DataAnnotations;

namespace PracticaUPC.Models
{
    public class Matricula
    {
        [Key]
        public decimal? IDMATRICULA { get; set; }
        public string? CODLINEANEGOCIO { get; set; }
        public string? CODMODALEST { get; set; }
        public string? CODPERIODO { get; set; }
        public string? CODALUMNO { get; set; }
        public string? USUARIOCREADOR { get; set; }
        public DateTime? FECHACREACION { get; set; }
        public string? USUARIOMODIFICACION { get; set; }

        public DateTime? FECHAMODIFICACION { get; set; }

        public virtual ICollection<DetMatricula>? DetMatriculas { get; set; }

    }

    public class DTOMatricula
    {
        public decimal IDMATRICULA { get; set; }
        public string? CODLINEANEGOCIO { get; set; }
        public string? CODMODALEST { get; set; }
        public string? CODPERIODO { get; set; }
        public string? CODALUMNO { get; set; }
        public string? USUARIOCREADOR { get; set; }
        public DateTime? FECHACREACION { get; set; }
    }
}
