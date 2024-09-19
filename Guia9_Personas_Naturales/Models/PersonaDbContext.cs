using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace Guia9_Personas_Naturales.Models
{
    public class PersonasDbContext : DbContext
    {
        public PersonasDbContext(DbContextOptions<PersonasDbContext> options) : base(options) { }
        public DbSet<PersonaNatural> Persona { get; set; }

    }
}
