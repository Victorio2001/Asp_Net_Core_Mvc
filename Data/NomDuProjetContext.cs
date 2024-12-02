using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NomDuProjet.Models;
using NomDuProjet.Models;

namespace NomDuProjet.Data
{
    public class NomDuProjetContext : DbContext
    {
        public NomDuProjetContext (DbContextOptions<NomDuProjetContext> options)
            : base(options)
        {
        }

        //! définition des entity à migrer
        public DbSet<Movie> Movie { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
    }
}
