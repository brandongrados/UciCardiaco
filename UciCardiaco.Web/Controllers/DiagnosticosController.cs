using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UciCardiaco.Datos;
using UciCardiaco.Entidad.Diagnosticos;
using UciCardiaco.Web.Models.Diagnosticos.Diagnostico;

namespace UciCardiaco.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticosController : ControllerBase
    {
        private readonly DbContextUciCardiaco _context;

        public DiagnosticosController(DbContextUciCardiaco context)
        {
            _context = context;
        }

        // GET: api/Diagnosticos/Listar
        [Authorize(Roles = "Medico, Administrador")]
        [HttpGet("[action]")]
        //agregando tarea asincrona de nombre listar
        public async Task<IEnumerable<DiagnosticoViewModel>> Listar()
        {
            var diagnostico = await _context.Diagnosticos
                .Include(d => d.usuario)
                .Include(d => d.persona)
                .OrderByDescending(d => d.id_diagnostico)
                .Take(100)
                .ToListAsync();


            return diagnostico.Select(d => new DiagnosticoViewModel
            {
                id_diagnostico = d.id_diagnostico,
                idpaciente = d.idpaciente,
                paciente = d.persona.nombre,
                num_documento=d.persona.num_documento,
                direccion=d.persona.direccion,
                telefono=d.persona.telefono,
                email=d.persona.email,
                idusuario = d.idusuario,
                usuario = d.usuario.nombre,
                tipo_diagnostico = d.tipo_diagnostico,
                serie_diagnostico = d.serie_diagnostico,
                num_diagnostico = d.num_diagnostico,
                fecha_hora = d.fecha_hora,
                resultado = d.resultado,
                estado = d.estado
            });

        }

        // GET: api/Diagnosticos/DiagnosticosMes12
        [Authorize(Roles = "Medico, Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConsultaViewModel>> DiagnosticosMes12()
        {
            var consulta = await _context.Diagnosticos
                .GroupBy(d=>d.fecha_hora.Month)
                .Select(x=> new { etiqueta=x.Key, valor =x.Sum(d=>d.resultado)})
               
                .OrderByDescending(x => x.etiqueta)
                .Take(12)
                .ToListAsync();


            return consulta.Select(d => new ConsultaViewModel
            {
                etiqueta=d.etiqueta.ToString(),
                valor=d.valor
            });

        }

        // GET: api/Diagnosticos/ListarFiltro
        [Authorize(Roles = "Medico, Administrador")]
        [HttpGet("[action]/{texto}")]
        //agregando tarea asincrona de nombre listar
        public async Task<IEnumerable<DiagnosticoViewModel>> ListarFiltro([FromRoute] string texto)
        {
            var diagnostico = await _context.Diagnosticos
                .Include(d => d.usuario)
                .Include(d => d.persona)
                .Where(d => d.num_diagnostico.Contains(texto))
                .OrderByDescending(d => d.id_diagnostico)
                .ToListAsync();


            return diagnostico.Select(d => new DiagnosticoViewModel
            {
                id_diagnostico = d.id_diagnostico,
                idpaciente = d.idpaciente,
                paciente = d.persona.nombre,
                num_documento = d.persona.num_documento,
                direccion = d.persona.direccion,
                telefono = d.persona.telefono,
                email = d.persona.email,
                idusuario = d.idusuario,
                usuario = d.usuario.nombre,
                tipo_diagnostico = d.tipo_diagnostico,
                serie_diagnostico = d.serie_diagnostico,
                num_diagnostico = d.num_diagnostico,
                fecha_hora = d.fecha_hora,
                resultado = d.resultado,
                estado = d.estado
            });

        }

        // GET: api/Diagnosticos/ConsultaFechas/FechaInicio/FechaFin
        [Authorize(Roles = "Medico, Administrador")]
        [HttpGet("[action]/{FechaInicio}/{FechaFin}")]
        public async Task<IEnumerable<DiagnosticoViewModel>> ConsultaFechas([FromRoute]DateTime FechaInicio, DateTime FechaFin)
        {
            var diagnostico = await _context.Diagnosticos
                .Include(d => d.usuario)
                .Include(d => d.persona)
                .Where(h=>h.fecha_hora>=FechaInicio)
                .Where(h => h.fecha_hora <= FechaFin)
                .OrderByDescending(d => d.id_diagnostico)
                .Take(100)
                .ToListAsync();


            return diagnostico.Select(d => new DiagnosticoViewModel
            {
                id_diagnostico = d.id_diagnostico,
                idpaciente = d.idpaciente,
                paciente = d.persona.nombre,
                num_documento = d.persona.num_documento,
                direccion = d.persona.direccion,
                telefono = d.persona.telefono,
                email = d.persona.email,
                idusuario = d.idusuario,
                usuario = d.usuario.nombre,
                tipo_diagnostico = d.tipo_diagnostico,
                serie_diagnostico = d.serie_diagnostico,
                num_diagnostico = d.num_diagnostico,
                fecha_hora = d.fecha_hora,
                resultado = d.resultado,
                estado = d.estado
            });

        }



        // GET: api/Diagnosticos/ListarDetalles
        [Authorize(Roles = "Medico, Administrador, Secretaria")]
        [HttpGet("[action]/{id_diagnostico}")]
        public async Task<IEnumerable<DetalleViewModel>> ListarDetalles([FromRoute] int id_diagnostico)
        {
            var detalle = await _context.DetallesDiagnosticos
                .Include(s => s.sintoma)
                .Where(dd => dd.id_diagnostico== id_diagnostico)
                .ToListAsync();


            return detalle.Select(dd => new DetalleViewModel
            {
                idsintoma = dd.idsintoma,
                sintoma = dd.sintoma.nombre,
                valor = dd.valor

            });

        }

        // POST: api/Diagnosticos/Crear
        [Authorize(Roles = "Medico, Administrador, Secretaria")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;

            Diagnostico diagnostico = new Diagnostico

            {
                idpaciente = model.idpaciente,
                idusuario = model.idusuario,
                tipo_diagnostico= model.tipo_diagnostico,
                serie_diagnostico= model.serie_diagnostico,
                num_diagnostico = model.num_diagnostico,
                fecha_hora = fechaHora,
                resultado = model.resultado,
                estado = "Aceptado"
            };


            try
            {
                _context.Diagnosticos.Add(diagnostico);
                await _context.SaveChangesAsync();

                var id = diagnostico.id_diagnostico;
                foreach (var det in model.detalles)
                {
                    DetalleDiagnostico detalle = new DetalleDiagnostico
                    {
                        id_diagnostico = id,
                        idsintoma = det.idsintoma,
                        valor = det.valor
                    };
                    _context.DetallesDiagnosticos.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }


        // PUT: api/Diagnosticos/Anular/1
        [Authorize(Roles = "Medico, Administrador,Secretaria")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var diagnostico = await _context.Diagnosticos.FirstOrDefaultAsync(d => d.id_diagnostico== id);

            if (diagnostico == null)
            {
                return NotFound();
            }

            diagnostico.estado = "Anulado";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }



        private bool DiagnosticoExists(int id)
        {
            return _context.Diagnosticos.Any(e => e.id_diagnostico == id);
        }
    }
}