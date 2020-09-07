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
using UciCardiaco.Web.Models.Diagnosticos.Persona;

namespace UciCardiaco.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly DbContextUciCardiaco _context;

        public PersonasController(DbContextUciCardiaco context)
        {
            _context = context;
        }

        // GET: api/Personas
        [HttpGet]
        public IEnumerable<Persona> GetPersonas()
        {
            return _context.Personas;
        }

        // GET: api/Personas/ListarPacientes
        [Authorize(Roles = "Medico, Administrador,Secretaria")]
        [HttpGet("[action]")]
        //agregando tarea asincrona de nombre listar
        public async Task<IEnumerable<PersonaViewModel>> ListarPacientes()
        {
            var persona = await _context.Personas.Where(p => p.tipo_persona=="Paciente").ToListAsync();

            return persona.Select(p => new PersonaViewModel
            {
                idpersona = p.idpersona,
                tipo_persona = p.tipo_persona,
                nombre = p.nombre,
                tipo_documento = p.tipo_documento,
                num_documento = p.num_documento,
                direccion= p.direccion,
                telefono = p.telefono,
                email = p.email,
                
            });

        }
        
        // GET: api/Personas/ListarEnfermeras
        [Authorize(Roles = "Medico, Administrador,Secretaria")]
        [HttpGet("[action]")]
        //agregando tarea asincrona de nombre listar
        public async Task<IEnumerable<PersonaViewModel>> ListarEnfermeras()
        {
            var persona = await _context.Personas.Where(p => p.tipo_persona == "Enfermera").ToListAsync();

            return persona.Select(p => new PersonaViewModel
            {
                idpersona = p.idpersona,
                tipo_persona = p.tipo_persona,
                nombre = p.nombre,
                tipo_documento = p.tipo_documento,
                num_documento = p.num_documento,
                direccion = p.direccion,
                telefono = p.telefono,
                email = p.email,

            });

        }

        // GET: api/Personas/SelectEnfermeras
        [Authorize(Roles ="Medico, Administrador , Secretaria")]
        [HttpGet("[action]")]
        //agregando tarea asincrona de nombre select
        public async Task<IEnumerable<SelectViewModel>> SelectEnfermeras()
        {
            var persona = await _context.Personas.Where(p => p.tipo_persona== "Enfermera").ToListAsync();

            return persona.Select(p => new SelectViewModel
            {
                idpersona = p.idpersona,
                nombre = p.nombre
            });

        }

        // GET: api/Personas/SelectPacientes
        [Authorize(Roles = "Medico, Administrador , Secretaria")]
        [HttpGet("[action]")]
        //agregando tarea asincrona de nombre select
        public async Task<IEnumerable<SelectViewModel>> SelectPacientes()
        {
            var persona = await _context.Personas.Where(p => p.tipo_persona == "Paciente").ToListAsync();

            return persona.Select(p => new SelectViewModel
            {
                idpersona = p.idpersona,
                nombre  = p.nombre
            });

        }

        // POST: api/Personas/Crear
        [Authorize(Roles = "Medico, Administrador,Secretaria")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = model.email.ToLower();

            if (await _context.Personas.AnyAsync(p => p.email == email))
            {
                return BadRequest("El email ya existe");
            }

            Persona persona = new Persona
            {
                tipo_persona = model.tipo_persona,
                nombre = model.nombre,
                tipo_documento = model.tipo_documento,
                num_documento = model.num_documento,
                direccion = model.direccion,
                telefono = model.telefono,
                email = model.email.ToLower(),
                


            };

            _context.Personas.Add(persona);
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

        // PUT: api/Personas/Actualizar
        [Authorize(Roles = "Medico, Administrador,Secretaria")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idpersona <= 0)
            {
                return BadRequest();
            }

            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.idpersona== model.idpersona);

            if (persona == null)
            {
                return NotFound();
            }
            persona.tipo_persona= model.tipo_persona;
            persona.nombre = model.nombre;
            persona.tipo_documento = model.tipo_documento;
            persona.num_documento = model.num_documento;
            persona.direccion = model.direccion;
            persona.telefono = model.telefono;
            persona.email = model.email.ToLower();

            


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



        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.idpersona == id);
        }
    }
}