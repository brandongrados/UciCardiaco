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
using UciCardiaco.Web.Models.Almacen.Sintoma;

namespace UciCardiaco.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SintomasController : ControllerBase
    {
        private readonly DbContextUciCardiaco _context;

        public SintomasController(DbContextUciCardiaco context)
        {
            _context = context;
        }

        // GET: api/Sintomas/Listar
        [Authorize(Roles = "Medico, Administrador")]
        [HttpGet("[action]")]
        //agregando tarea asincrona de nombre listar
        public async Task<IEnumerable<SintomaViewModel>> Listar()
        {
            var sintoma = await _context.Sintomas.Include(s => s.enfermedad).ToListAsync();

            return sintoma.Select(s => new SintomaViewModel
            {
                idsintoma = s.idsintoma,
                idenfermedad = s.idenfermedad,
                enfermedad =s.enfermedad.nombre,
                codigo=s.codigo,
                nombre = s.nombre,
                valor=s.valor,
                descripcion = s.descripcion,
                condicion = s.condicion
            });

        }

        // GET: api/Sintomas/ListarHistoria/texto
        [Authorize(Roles = "Medico, Administrador")]
        [HttpGet("[action]/{texto}")]
        //agregando tarea asincrona de nombre listar
        public async Task<IEnumerable<SintomaViewModel>> ListarHistoria([FromRoute] string texto)
        {
            var sintoma = await _context.Sintomas.Include(s => s.enfermedad)
                .Where(s => s.nombre.Contains(texto))
                .Where(s => s.condicion == true)
                .ToListAsync();

            return sintoma.Select(s => new SintomaViewModel
            {
                idsintoma = s.idsintoma,
                idenfermedad = s.idenfermedad,
                enfermedad = s.enfermedad.nombre,
                codigo = s.codigo,
                nombre = s.nombre,
                valor = s.valor,
                descripcion = s.descripcion,
                condicion = s.condicion
            });

        }

        // GET: api/Sintomas/ListarDiagnostico/texto
        [Authorize(Roles = "Medico, Administrador")]
        [HttpGet("[action]/{texto}")]
        //agregando tarea asincrona de nombre listar
        public async Task<IEnumerable<SintomaViewModel>> ListarDiagnostico([FromRoute] string texto)
        {
            var sintoma = await _context.Sintomas.Include(s => s.enfermedad)
                .Where(s => s.nombre.Contains(texto))
                .Where(s => s.condicion == true)
                .ToListAsync();

            return sintoma.Select(s => new SintomaViewModel
            {
                idsintoma = s.idsintoma,
                idenfermedad = s.idenfermedad,
                enfermedad = s.enfermedad.nombre,
                codigo = s.codigo,
                nombre = s.nombre,
                valor = s.valor,
                descripcion = s.descripcion,
                condicion = s.condicion
            });

        }

        // GET: api/Sintomas/Mostrar/1
        [Authorize(Roles = "Medico, Administrador")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var sintoma = await _context.Sintomas.Include(s=>s.enfermedad).
                SingleOrDefaultAsync(s=>s.idsintoma==id);

            if (sintoma == null)
            {
                return NotFound();
            }

            return Ok(new SintomaViewModel
            {
                idsintoma = sintoma.idsintoma,
                idenfermedad = sintoma.idenfermedad,
                enfermedad = sintoma.enfermedad.nombre,
                codigo = sintoma.codigo,
                nombre = sintoma.nombre,
                descripcion = sintoma.descripcion,
                valor = sintoma.valor,
                condicion = sintoma.condicion
            });
        }

        // GET: api/Sintomas/BuscarCodigoHistoria/14356
        [Authorize(Roles = "Medico, Administrador")]
        [HttpGet("[action]/{codigo}")]
        public async Task<IActionResult> BuscarCodigoHistoria([FromRoute] string codigo)
        {

            var sintoma = await _context.Sintomas.Include(s => s.enfermedad).
                Where(s=>s.condicion==true).
                SingleOrDefaultAsync(s => s.codigo== codigo);

            if (sintoma == null)
            {
                return NotFound();
            }

            return Ok(new SintomaViewModel
            {
                idsintoma = sintoma.idsintoma,
                idenfermedad = sintoma.idenfermedad,
                enfermedad = sintoma.enfermedad.nombre,
                codigo = sintoma.codigo,
                nombre = sintoma.nombre,
                descripcion = sintoma.descripcion,
                valor = sintoma.valor,
                condicion = sintoma.condicion
            });
        }

        // GET: api/Sintomas/BuscarCodigoDiagnostico/14356
        [Authorize(Roles = "Medico, Administrador")]
        [HttpGet("[action]/{codigo}")]
        public async Task<IActionResult> BuscarCodigoDiagnostico([FromRoute] string codigo)
        {

            var sintoma = await _context.Sintomas.Include(s => s.enfermedad).
                Where(s => s.condicion == true).
                
                SingleOrDefaultAsync(s => s.codigo == codigo);

            if (sintoma == null)
            {
                return NotFound();
            }

            return Ok(new SintomaViewModel
            {
                idsintoma = sintoma.idsintoma,
                idenfermedad = sintoma.idenfermedad,
                enfermedad = sintoma.enfermedad.nombre,
                codigo = sintoma.codigo,
                nombre = sintoma.nombre,
                descripcion = sintoma.descripcion,
                valor = sintoma.valor,
                condicion = sintoma.condicion
            });
        }


        // PUT: api/Sintomas/Actualizar
        [Authorize(Roles = "Medico, Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idsintoma <= 0)
            {
                return BadRequest();
            }

            var sintoma = await _context.Sintomas.FirstOrDefaultAsync(s => s.idsintoma== model.idsintoma);

            if (sintoma == null)
            {
                return NotFound();
            }
            sintoma.idenfermedad = model.idenfermedad;
            sintoma.codigo = model.codigo;
            sintoma.nombre = model.nombre;
            sintoma.valor = model.valor;
            sintoma.descripcion = model.descripcion;


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

        // POST: api/Sintomas/Crear
        [Authorize(Roles = "Medico, Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idenfermedad <= 0)
            {
                return BadRequest();
            }
            Sintoma sintoma = new Sintoma
            {   
                idenfermedad= model.idenfermedad,
                codigo=model.codigo,
                nombre = model.nombre,
                valor=model.valor,
                descripcion = model.descripcion,
                condicion = true
            };

            _context.Sintomas.Add(sintoma);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Sintomas/Desactivar/1
        [Authorize(Roles = "Medico, Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var sintoma = await _context.Sintomas.FirstOrDefaultAsync(s => s.idsintoma== id);

            if (sintoma == null)
            {
                return NotFound();
            }

            sintoma.condicion = false;

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

        // PUT: api/Sintomas/Activar/1
        [Authorize(Roles = "Medico, Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var sintoma = await _context.Sintomas.FirstOrDefaultAsync(s => s.idsintoma == id);

            if (sintoma == null)
            {
                return NotFound();
            }

            sintoma.condicion = true;

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

        private bool SintomaExists(int id)
        {
            return _context.Sintomas.Any(e => e.idsintoma== id);
        }
    }
}
