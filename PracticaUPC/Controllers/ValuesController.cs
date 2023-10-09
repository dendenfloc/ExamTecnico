using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticaUPC.Models;

namespace PracticaUPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ModelContext _context;

        public ValuesController(ModelContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public IActionResult Listar(int idmatri)
        {
            try
            {
                DTOMatricula oRespuesta = new DTOMatricula();
                DTOHeader oDTOHeader = new DTOHeader();
                ListaDTODetMatriculaOBJ ListaDTODetMatriculaOBJ = new ListaDTODetMatriculaOBJ();

                var matricula = _context.Matriculas.Where(t=> t.IDMATRICULA.Equals(idmatri)).FirstOrDefault();
                ListaDTODetMatriculaOBJ.DTODetMatriculaCab = matricula;

                oDTOHeader.CodigoRetorno = "Correcto";

                var detalles = _context.DetMatriculas.Where(t => t.IDMATRICULA.Equals(idmatri)).ToList();
                ListaDTODetMatriculaOBJ.ListaDTODetMatriculaDet = detalles;

                oRespuesta.ListaDTODetMatriculaOBJ = ListaDTODetMatriculaOBJ;
                oRespuesta.DTOHeader = oDTOHeader;

                return Ok(oRespuesta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
