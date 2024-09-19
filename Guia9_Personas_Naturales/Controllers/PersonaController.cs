using Guia9_Personas_Naturales.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Guia9_Personas_Naturales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly PersonasDbContext _context;

        public PersonasController(PersonasDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<PersonaNatural>> PostPersona(PersonaNatural persona)
        {
            //validaciones solicitadas
            if (string.IsNullOrEmpty(persona.PrimerNombre))
            {
                return BadRequest("Ingrese el primer nombre");
            }
            if (string.IsNullOrEmpty(persona.PrimerApellido))
            {
                return BadRequest("Ingrese el primer apellido");
            }
            if (persona.PrimerNombre.Length > 100)
            {
                return BadRequest("El primer nombre no puede exceder los 100 caracteres");
            }

            if (!string.IsNullOrEmpty(persona.SegundoNombre) && persona.SegundoNombre.Length > 100)
            {
                return BadRequest("El segundo nombre no puede exceder los 100 caracteres");
            }

            if (persona.PrimerApellido.Length > 100)
            {
                return BadRequest("El primer apellido no puede exceder los 100 caracteres");
            }

            if (!string.IsNullOrEmpty(persona.SegundoApellido) && persona.SegundoApellido.Length > 100)
            {
                return BadRequest("El segundo apellido no puede exceder los 100 caracteres");
            }

            if (persona.FechaNacimiento == null || !DateTime.TryParse(persona.FechaNacimiento.ToString(), out DateTime fechaNacimiento))
            {
                return BadRequest("La fecha de nacimiento no es válida o está vacía");
            }

            if (fechaNacimiento > DateTime.Now)
            {
                return BadRequest("La fecha de nacimiento no puede ser mayor que la fecha actual");
            }

            // Validar formato del DUI
            var duiRegex = @"^\d{8}-\d{1}$";
            if (!Regex.IsMatch(persona.Dui, duiRegex))
            {
                return BadRequest("El formato del DUI es incorrecto. Debe ser ********-*");
            }
            _context.Persona.Add(persona);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaNatural>> GetPersona(int id)
        {
            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return persona;
        }
    }
}
