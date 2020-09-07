using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UciCardiaco.Datos;
using UciCardiaco.Entidad.Almacen;
using UciCardiaco.Web.Models.Almacen.Historia;

namespace UciCardiaco.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriasController : ControllerBase
    {
        private readonly DbContextUciCardiaco _context;

        public HistoriasController(DbContextUciCardiaco context)
        {
            _context = context;
        }

        //GET: api/Historias
        //[HttpGet]
        //public IEnumerable<Historia> GetHistorias()
        //{
        //    return _context.Historias;
        //}

        // GET: api/Historias/Listar
        [Authorize(Roles = "Medico, Administrador, Secretaria")]
        [HttpGet("[action]")]
        //agregando tarea asincrona de nombre listar
        public async Task<IEnumerable<HistoriaViewModel>> Listar()
        {
            var historia = await _context.Historias
                .Include(h => h.usuario)
                .Include(h => h.persona)
                .OrderByDescending(h=>h.idhistoria)
                .Take(100)
                .ToListAsync();
                

            return historia.Select(h => new HistoriaViewModel
            {
                idhistoria = h.idhistoria,
                idenfermera = h.idenfermera,
                enfermera = h.persona.nombre,
                idusuario = h.idusuario,
                usuario = h.usuario.nombre,
                tipo_historia = h.tipo_historia,
                serie_historia = h.serie_historia,
                num_historia = h.num_historia,
                fecha_hora=h.fecha_hora,
                resultado=h.resultado,
                estado = h.estado
            });

        }

        // GET: api/Historias/ListarFiltro
        [Authorize(Roles = "Medico, Administrador, Secretaria")]
        [HttpGet("[action]/{texto}")]
        //agregando tarea asincrona de nombre listar
        public async Task<IEnumerable<HistoriaViewModel>> ListarFiltro([FromRoute] string texto)
        {
            var historia = await _context.Historias
                .Include(h => h.usuario)
                .Include(h => h.persona)
                .Where(h=>h.num_historia.Contains(texto))
                .OrderByDescending(h => h.idhistoria)
                .ToListAsync();


            return historia.Select(h => new HistoriaViewModel
            {
                idhistoria = h.idhistoria,
                idenfermera = h.idenfermera,
                enfermera = h.persona.nombre,
                idusuario = h.idusuario,
                usuario = h.usuario.nombre,
                tipo_historia = h.tipo_historia,
                serie_historia = h.serie_historia,
                num_historia = h.num_historia,
                fecha_hora = h.fecha_hora,
                resultado = h.resultado,
                estado = h.estado
            });

        }

        // GET: api/Diagnosticos/ConsultaFechas/FechaInicio/FechaFin
        [Authorize(Roles = "Medico, Administrador")]
        [HttpGet("[action]/{FechaInicio}/{FechaFin}")]
        public async Task<IEnumerable<HistoriaViewModel>> ConsultaFechas([FromRoute] DateTime FechaInicio, DateTime FechaFin)
        {
            var historia = await _context.Historias
                .Include(d => d.usuario)
                .Include(d => d.persona)
                .Where(i => i.fecha_hora >= FechaInicio)
                .Where(i => i.fecha_hora <= FechaFin)
                .OrderByDescending(h => h.idhistoria)
                .Take(100)
                .ToListAsync();


            return historia.Select(h => new HistoriaViewModel
            {
                idhistoria = h.idhistoria,
                idenfermera = h.idenfermera,
                enfermera = h.persona.nombre,
                idusuario = h.idusuario,
                usuario = h.usuario.nombre,
                tipo_historia = h.tipo_historia,
                serie_historia = h.serie_historia,
                num_historia = h.num_historia,
                fecha_hora = h.fecha_hora,
                resultado = h.resultado,
                estado = h.estado
            });

        }




        // GET: api/Historias/ListarDetalles
        [Authorize(Roles = "Medico, Administrador, Secretaria")]
        [HttpGet("[action]/{idhistoria}")]
        
        public async Task<IEnumerable<DetalleViewModel>> ListarDetalles([FromRoute] int idhistoria)
        {
            var detalle = await _context.DetallesHistorias
                .Include(s => s.sintoma)
                .Where(d => d.idhistoria == idhistoria)
                .ToListAsync();


            return detalle.Select(d => new DetalleViewModel
            {
                idsintoma = d.idsintoma,
                sintoma=d.sintoma.nombre,
                valor=d.valor

            });

        }


        // POST: api/Historias/Crear
        [Authorize(Roles = "Medico, Administrador,Secretaria")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;

            Historia historia = new Historia

            {
                idenfermera = model.idenfermera,
                idusuario = model.idusuario,
                tipo_historia = model.tipo_historia,
                serie_historia = model.serie_historia,
                num_historia = model.num_historia,
                fecha_hora = fechaHora, 
                resultado = model.resultado,
                estado = "Aceptado"
            };

            
            try
            {
                _context.Historias.Add(historia);
                await _context.SaveChangesAsync();

                var id = historia.idhistoria;
                foreach (var det in model.detalles)
                {
                    DetalleHistoria detalle = new DetalleHistoria
                    {
                        idhistoria = id,
                        idsintoma = det.idsintoma,
                        valor = det.valor
                    };
                    _context.DetallesHistorias.Add(detalle);
                }
                await _context.SaveChangesAsync(); 
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }


        // PUT: api/Enfermedades/Anular/1
        [Authorize(Roles = "Medico, Administrador,Secretaria")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var historia = await _context.Historias.FirstOrDefaultAsync(e => e.idhistoria == id);

            if (historia == null)
            {
                return NotFound();
            }

            historia.estado= "Anulado";

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



        private bool HistoriaExists(int id)
        {
            return _context.Historias.Any(e => e.idhistoria == id);
        }
    }
}