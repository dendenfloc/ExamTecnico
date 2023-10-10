namespace PracticaUPC.Models
{
    public class DTOMatri
    {
        public DTOHeader DTOHeader { get; set; }
        public ListaDTODetMatriculaOBJ ListaDTODetMatriculaOBJ { get; set; }

    }
    //public class DTODetMatriculaCab
    //{
    //    public Matricula Matricula { get; set; }
    //}

    public class ListaDTODetMatriculaOBJ
    {
        public DTOMatricula DTODetMatriculaCab { get; set; }
        public List<DTODetMatricula> ListaDTODetMatriculaDet { get; set; }

    }
    public class DTOHeader
    {
        public string? CodigoRetorno { get; set; }
        public string? DescRetorno { get; set; }
    }
}
