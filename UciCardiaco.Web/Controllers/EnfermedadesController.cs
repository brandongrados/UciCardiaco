using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UciCardiaco.Datos;
using UciCardiaco.Entidad.Almacen;
using UciCardiaco.Web.Models.Almacen.Enfermedad;


namespace UciCardiaco.Web.Controllers
{
    // Ruta para el front de Enfermedad

    [Authorize(Roles ="Medico, Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class EnfermedadesController : ControllerBase
    {
        private readonly DbContextUciCardiaco _context;

        public EnfermedadesController(DbContextUciCardiaco context)
        {
            _context = context;
        }

        // GET: api/Enfermedad/Listar
        [HttpGet("[action]")]
        //agregando tarea asincrona de nombre listar
        public async Task <IEnumerable<EnfermedadViewModel>> Listar()
        {
            var enfermedad = await _context.Enfermedades.ToListAsync();

            return enfermedad.Select(e => new EnfermedadViewModel
            {
                idenfermedad = e.idenfermedad,
                nombre = e.nombre,
                descripcion = e.descripcion,
                condicion = e.condicion
            });

        }

        // GET: api/Enfermedad/Select
        [HttpGet("[action]")]
        //agregando tarea asincrona de nombre select
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var enfermedad = await _context.Enfermedades.Where(e=>e.condicion==true).ToListAsync();

            return enfermedad.Select(e => new SelectViewModel
            {
                idenfermedad = e.idenfermedad,
                nombre = e.nombre
            });

        }

        // GET: api/Enfermedades/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var enfermedad = await _context.Enfermedades.FindAsync(id);

            if (enfermedad == null)
            {
                return NotFound();
            }

            return Ok(new EnfermedadViewModel
            {
                idenfermedad = enfermedad.idenfermedad,
                nombre = enfermedad.nombre,
                descripcion = enfermedad.descripcion,
                condicion = enfermedad.condicion
            });
        }

        // PUT: api/Enfermedades/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idenfermedad <= 0)
            {
                return BadRequest();
            }

            var enfermedad = await _context.Enfermedades.FirstOrDefaultAsync(e => e.idenfermedad == model.idenfermedad);

            if (enfermedad == null)
            {
                return NotFound();
            }
            enfermedad.idenfermedad = model.idenfermedad;
            enfermedad.nombre = model.nombre;
            enfermedad.descripcion = model.descripcion;

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

        // POST: api/Enfermedades/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Enfermedad enfermedad = new Enfermedad
            {
                nombre = model.nombre,
                descripcion = model.descripcion,
                condicion = true
            };

            _context.Enfermedades.Add(enfermedad);
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

        // DELETE: api/Enfermedades/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enfermedad = await _context.Enfermedades.FindAsync(id);
            if (enfermedad == null)
            {
                return NotFound();
            }

            _context.Enfermedades.Remove(enfermedad);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(enfermedad);
        }

        // PUT: api/Enfermedades/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var enfermedad = await _context.Enfermedades.FirstOrDefaultAsync(e => e.idenfermedad== id);

            if (enfermedad == null)
            {
                return NotFound();
            }

            enfermedad.condicion = false;

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

        // PUT: api/Enfermedades/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var enfermedad = await _context.Enfermedades.FirstOrDefaultAsync(e => e.idenfermedad== id);

            if (enfermedad== null)
            {
                return NotFound();
            }

            enfermedad.condicion = true;

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

        private bool EnfermedadExists(int id)
        {
            return _context.Enfermedades.Any(e => e.idenfermedad== id);
        }
    }
}