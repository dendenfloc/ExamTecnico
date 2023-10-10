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
                                    IDMATRICULA = item.Matricula.IDMATRICULA,
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
                // Ahora 'matriculaWithDetails' contiene Matricula y los detalles con Curso asociado.


                // Ahora 'matriculaWithDetails' contiene Matricula y los detalles con Curso asociado.

                ////ListaDTODetMatriculaOBJ ListaDTODetMatriculaOBJ = new ListaDTODetMatriculaOBJ();
                //var dtoMatricula = _context.Matriculas
                //    .Where(t => t.IDMATRICULA.Equals(idmatri))
                //    .Include(t => t.DetMatriculas)
                //    .Include(t => t.Curso)
                //    .Select(m => new ListaDTODetMatriculaOBJ
                //    {
                //        DTODetMatriculaCab = new DTOMatricula
                //        {
                //            IDMATRICULA = m.IDMATRICULA,
                //            CODLINEANEGOCIO = m.CODLINEANEGOCIO,
                //            CODMODALEST = m.CODMODALEST,
                //            CODPERIODO = m.CODPERIODO,
                //            CODALUMNO = m.CODALUMNO,
                //            USUARIOCREADOR = m.USUARIOCREADOR,
                //            FECHACREACION = m.FECHACREACION
                //        },
                //        ListaDTODetMatriculaDet = m.DetMatriculas.Select(d => new DTODetMatricula
                //        {
                //            CODPRODUCTO = d.CODPRODUCTO,
                //            SECCION = d.SECCION,
                //            GRUPO = d.GRUPO
                //        }).ToList()
                //    })
                //    .FirstOrDefault();



                //var matricula = _context.Matriculas.Include(t=>t.DetMatriculas).Where(t=> t.IDMATRICULA.Equals(idmatri)).FirstOrDefault();
                //ListaDTODetMatriculaOBJ.DTODetMatriculaCab = matricula;

                oDTOHeader.CodigoRetorno = "Correcto";

                ////var detalles = _context.DetMatriculas.Where(t => t.IDMATRICULA.Equals(idmatri)).ToList();
                //ListaDTODetMatriculaOBJ.ListaDTODetMatriculaDet = result;

                oRespuesta.ListaDTODetMatriculaOBJ = result.FirstOrDefault();
                oRespuesta.DTOHeader = oDTOHeader;

                return Ok(oRespuesta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpPost("Guardar")]
        //public async Task<ActionResult<dtomatr>> Guardar([FromBody] TBT_SEDES tbt_SEDES)
        //{
        //    try
        //    {
        //        _context.TBT_SEDES.Add(tbt_SEDES);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException != null)
        //        {
        //            if (ex.InnerException.Message.Contains(UtilHelper.GetExceptionTruncate()))
        //            {
        //                tbt_SEDES.Error = UtilHelper.GetMessageErrorTruncate();
        //            }
        //        }
        //        else
        //        {
        //            tbt_SEDES.Error = UtilHelper.GetExceptionDeleteOtros();
        //        }
        //        return tbt_SEDES;
        //    }

        //    return Ok(tbt_SEDES);
        //}
    }
}
