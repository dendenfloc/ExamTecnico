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
                var matriculaWithDetails = _context.Matriculas
                       .Where(m => m.IDMATRICULA == idmatri) // Filtra por ID de Matricula deseado
                       .Select(m => new
                       {
                           Matricula = m,
                           Detalles = _context.DetMatriculas
                               .Where(d => d.IDMATRICULA == m.IDMATRICULA)
                               .Join(_context.Cursos,
                                   d => new { d.CODLINEANEGOCIO, d.CODCURSO },
                                   curso => new { curso.CODLINEANEGOCIO, curso.CODCURSO },
                                   (d, curso) => new { Detalle = d, Curso = curso }
                               )
                               .ToList() // Convierte el resultado intermedio en una lista
                       })
                       .ToList();

                var result = matriculaWithDetails
                            .Select(item => new ListaDTODetMatriculaOBJ
                            {
                                DTODetMatriculaCab = new DTOMatricula
                                {
                                    IDMATRICULA = item.Matricula.IDMATRICULA.Value,
                                    CODLINEANEGOCIO = item.Matricula.CODLINEANEGOCIO,
                                    CODMODALEST = item.Matricula.CODMODALEST,
                                    CODPERIODO = item.Matricula.CODPERIODO,
                                    CODALUMNO = item.Matricula.CODALUMNO,
                                    USUARIOCREADOR = item.Matricula.USUARIOCREADOR,
                                    FECHACREACION = item.Matricula.FECHACREACION
                                },
                                ListaDTODetMatriculaDet = item.Detalles
                                    .Select(d => new DTODetMatricula
                                    {
                                        CODPRODUCTO = d.Detalle.CODPRODUCTO,
                                        SECCION = d.Detalle.SECCION,
                                        GRUPO = d.Detalle.GRUPO,
                                        CODCURSO = d.Detalle.Curso.CODCURSO,
                                        DESCCURSO = d.Detalle.Curso.DESCCURSO
                                    })
                                    .ToList()
                            })
                            .ToList();
                oDTOHeader.CodigoRetorno = "Correcto";
                oRespuesta.ListaDTODetMatriculaOBJ = result.FirstOrDefault();
                oRespuesta.DTOHeader = oDTOHeader;

                return Ok(oRespuesta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<DTOMatricula>> Guardar([FromBody] DTOMatricula dTOMatricula)
        {
            try
            {
                var model = new Matricula();
                model.CODLINEANEGOCIO = dTOMatricula.CODLINEANEGOCIO;
                model.CODMODALEST = dTOMatricula.CODMODALEST;
                model.CODPERIODO = dTOMatricula.CODPERIODO;
                model.CODALUMNO = dTOMatricula.CODALUMNO;
                model.USUARIOCREADOR = dTOMatricula.USUARIOCREADOR;
                model.FECHACREACION = dTOMatricula.FECHACREACION;
                _context.Matriculas.Add(model);
                await _context.SaveChangesAsync();

                dTOMatricula.IDMATRICULA = model.IDMATRICULA.Value;
            }
            catch (Exception ex)
            {
                return dTOMatricula;
            }

            return Ok(dTOMatricula);
        }
    }
}
