using Guia9_Personas_Naturales.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Test
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
