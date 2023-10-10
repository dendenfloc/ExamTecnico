using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                DTOMatri oRespuesta = new DTOMatri();
                DTOHeader oDTOHeader = new DTOHeader();
                //ListaDTODetMatriculaOBJ ListaDTODetMatriculaOBJ = new ListaDTODetMatriculaOBJ();
                var dtoMatricula = _context.Matriculas
                    .Where(t => t.IDMATRICULA.Equals(idmatri))
                    .Include(t => t.DetMatriculas)
                    .Select(m => new ListaDTODetMatriculaOBJ
                    {
                        DTODetMatriculaCab = new DTOMatricula
                        {
                            IDMATRICULA = m.IDMATRICULA,
                            CODLINEANEGOCIO = m.CODLINEANEGOCIO,
                            CODMODALEST = m.CODMODALEST,
                            CODPERIODO = m.CODPERIODO,
                            CODALUMNO = m.CODALUMNO,
                            USUARIOCREADOR = m.USUARIOCREADOR,
                            FECHACREACION = m.FECHACREACION,
                            USUARIOMODIFICACION = m.USUARIOMODIFICACION,
                            FECHAMODIFICACION = m.FECHAMODIFICACION
                            // Agrega todas las propiedades de Matricula que necesitas aquí
                        },
                        ListaDTODetMatriculaDet = m.DetMatriculas.Select(d => new DTODetMatricula
                        {
                            IDMATRICULA = d.IDMATRICULA,
                            CODPRODUCTO = d.CODPRODUCTO,
                            SECCION = d.SECCION,
                            GRUPO = d.GRUPO,
                            USUARIOCREADOR = d.USUARIOCREADOR,
                            FECHACREACION = d.FECHACREACION,
                            USUARIOMODIFICACION = d.USUARIOMODIFICACION,
                            FECHAMODIFICACION = d.FECHAMODIFICACION,
                            CODLINEANEGOCIO = d.CODLINEANEGOCIO,
                            CODCURSO = d.CODCURSO
                            // Agrega todas las propiedades de DetMatricula que necesitas aquí
                        }).ToList()
                    })
                    .FirstOrDefault();




                //var matricula = _context.Matriculas.Include(t=>t.DetMatriculas).Where(t=> t.IDMATRICULA.Equals(idmatri)).FirstOrDefault();
                //ListaDTODetMatriculaOBJ.DTODetMatriculaCab = matricula;

                oDTOHeader.CodigoRetorno = "Correcto";

                ////var detalles = _context.DetMatriculas.Where(t => t.IDMATRICULA.Equals(idmatri)).ToList();
                ////ListaDTODetMatriculaOBJ.ListaDTODetMatriculaDet = detalles;

                oRespuesta.ListaDTODetMatriculaOBJ = dtoMatricula;
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
