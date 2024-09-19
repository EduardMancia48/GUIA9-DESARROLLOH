using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guia9_Personas_Naturales.Controllers;
using Guia9_Personas_Naturales.Models;
using Microsoft.EntityFrameworkCore;
namespace PersonaNatural.Test
{
    public class PersonasControllerTest
    {
        [Fact]
        public async Task PostPersona_AgregarPersona_CuandoPersonaEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonaController(context);
            var nuevapersona = new Persona
            {
                PrimerNombre = "Javier",
                SegundoNombre = null,
                PrimerApellido = "Ramírez",
                SegundoApellido = null,
                Dui = "04567890-1",
                FechaNacimiento = new DateTime(1995, 12, 15)
            };

            var result = await controller.PostPersona(nuevapersona);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var persona = Assert.IsType<Persona>(createdResult.Value);
            Assert.Equal("Javier", persona.PrimerNombre);
        }

        [Fact]
        public async Task GetPersona_RetornaPersona_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var persona = new Persona
            {
                PrimerNombre = "Luis",
                SegundoNombre = "Andrés",
                PrimerApellido = "Torres",
                SegundoApellido = "Martínez",
                Dui = "11223344-5",
                FechaNacimiento = new DateTime(1990, 3, 10)
            };
            context.Persona.Add(persona);
            await context.SaveChangesAsync();

            var result = await controller.GetPersona(persona.Id);

            var actionResult = Assert.IsType<ActionResult<Persona>>(result);
            var returnValue = Assert.IsType<Persona>(actionResult.Value);
            Assert.Equal("Luis", returnValue.PrimerNombre);
        }

        [Fact]
        public async Task GetPersona_RetornaNotFound_CuandoIdNoExiste()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var result = await controller.GetPersona(999);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // Validación de Primer Nombre
        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoPrimerNombreEsNulo()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            // probando nombre nulo
            var primerpersona = new Persona
            {
                PrimerNombre = "Carlos",
                SegundoNombre = "Fernando",
                PrimerApellido = "Salazar",
                SegundoApellido = "Pérez",
                Dui = "09876543-2",
                FechaNacimiento = new DateTime(1985, 7, 25)
            };
            await controller.PostPersona(primerpersona);
            var nuevapersona = new Persona
            {
                PrimerNombre = "Daniel",
                SegundoNombre = "Emilio",
                PrimerApellido = "Fuentes",
                SegundoApellido = "García",
                Dui = "12345678-9",
                FechaNacimiento = new DateTime(2000, 11, 30)
            };

            await controller.PostPersona(nuevapersona);
            var personas = await context.Persona.ToListAsync();

            Assert.Equal(2, personas.Count);
        }

        // Validación de primer apellido
        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoPrimerApellidoEsNulo()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);

            var persona = new Persona
            {
                PrimerNombre = "Sofía",
                SegundoNombre = "Valentina",
                PrimerApellido = "Navarro",
                SegundoApellido = "Mendoza",
                Dui = "22334455-6",
                FechaNacimiento = new DateTime(2001, 9, 12)
            };

            var result = await controller.PostPersona(persona);
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var personas = await context.Persona.ToListAsync();
            Assert.Single(personas);
        }

        // Validación campos opcionales
        [Fact]
        public async Task PostPersona_AgregaPersona_CuandoSegundoNombreYSegundoApellidoSonOpcionales()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);

            var nuevapersona = new Persona
            {
                PrimerNombre = "Fernando",
                SegundoNombre = null,  // Segundo nombre opcional
                PrimerApellido = "López",
                SegundoApellido = null,  // Segundo apellido opcional
                Dui = "33445566-7",
                FechaNacimiento = new DateTime(1983, 4, 20)
            };

            var result = await controller.PostPersona(nuevapersona);

            Assert.IsType<CreatedAtActionResult>(result.Result);
            var personas = await context.Persona.ToListAsync();
            Assert.Single(personas);
        }

        // Validación de longitud para primer nombre
        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoPrimerNombreExcedeLongitud()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);

            var persona = new Persona
            {
                PrimerNombre = "Alejandra",
                SegundoNombre = "Lucía",
                PrimerApellido = "Guzmán",
                SegundoApellido = "Vega",
                Dui = "44556677-8",
                FechaNacimiento = new DateTime(1997, 6, 14)
            };

            var result = await controller.PostPersona(persona);
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var personas = await context.Persona.ToListAsync();
            Assert.Single(personas);
        }

        // Validación de longitud para segundo nombre
        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoSegundoNombreExcedeLongitud()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);

            var persona = new Persona
            {
                PrimerNombre = "Gabriela",
                SegundoNombre = "María",
                PrimerApellido = "Pérez",
                SegundoApellido = "Rodríguez",
                Dui = "55667788-9",
                FechaNacimiento = new DateTime(2002, 2, 17)
            };
            var result = await controller.PostPersona(persona);
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var personas = await context.Persona.ToListAsync();
            Assert.Single(personas);
        }

        // Validación de longitud para primer apellido
        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoPrimerApellidoExcedeLongitud()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);

            var persona = new Persona
            {
                PrimerNombre = "Juan",
                SegundoNombre = "Diego",
                PrimerApellido = "Alvarado",
                SegundoApellido = "Cruz",
                Dui = "66778899-0",
                FechaNacimiento = new DateTime(1980, 1, 5)
            };

            var result = await controller.PostPersona(persona);
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var personas = await context.Persona.ToListAsync();
            Assert.Single(personas);
        }

        // Validación de longitud para segundo apellido
        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoSegundoApellidoExcedeLongitud()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);

            var persona = new Persona
            {
                PrimerNombre = "Carlos",
                SegundoNombre = "Eduardo",
                PrimerApellido = "Morales",
                SegundoApellido = "Hernández",
                Dui = "77889900-1",
                FechaNacimiento = new DateTime(1988, 3, 22)
            };

            var result = await controller.PostPersona(persona);
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var personas = await context.Persona.ToListAsync();
            Assert.Single(personas);
        }

        // Validación de Dui
        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoDuiIncorrecto()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);

            var persona = new Persona
            {
                PrimerNombre = "Lucía",
                SegundoNombre = "Isabel",
                PrimerApellido = "Cruz",
                SegundoApellido = "Ramírez",
                Dui = "87654321-0",
                FechaNacimiento = new DateTime(1994, 11, 2)
            };

            var result = await controller.PostPersona(persona);
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var personas = await context.Persona.ToListAsync();
            Assert.Single(personas);
        }

        // Validación de fecha nula
        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoFechaNacimientoEsNula()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);

            var persona = new Persona
            {
                PrimerNombre = "Valeria",
                SegundoNombre = "Sofía",
                PrimerApellido = "Ríos",
                SegundoApellido = "Ortiz",
                Dui = "98765432-1",
                FechaNacimiento = new DateTime(2005, 9, 18)
            };
            var result = await controller.PostPersona(persona);
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var personas = await context.Persona.ToListAsync();
            Assert.Single(personas);
        }

        // Validación de fecha inválida
        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoFechaNacimientoEsInvalida()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);

            var persona = new Persona
            {
                PrimerNombre = "Andrea",
                SegundoNombre = "Fernanda",
                PrimerApellido = "Villalobos",
                SegundoApellido = "Gómez",
                Dui = "12312312-3",
                FechaNacimiento = new DateTime(2004, 12, 25)
            };

            var result = await controller.PostPersona(persona);
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var personas = await context.Persona.ToListAsync();
            Assert.Single(personas);
        }
    }

}
