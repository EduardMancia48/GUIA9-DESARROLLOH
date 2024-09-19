using System;
using Microsoft.EntityFrameworkCore; // Necesario para DbContext y opciones
using Guia9_Personas_Naturales.Models; 

namespace PersonaNatural.Test
{
    public static class Setup
    {
        public static PersonasDbContext GetInMemoryDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<PersonasDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new PersonasDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
