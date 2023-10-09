namespace PracticaUPC.Models
{
    public class DTOMatricula
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
        public Matricula DTODetMatriculaCab { get; set; }
        public List<DetMatricula> ListaDTODetMatriculaDet { get; set; }

    }
    public class DTOHeader
    {
        public string? CodigoRetorno { get; set; }
        public string? DescRetorno { get; set; }
    }
}
