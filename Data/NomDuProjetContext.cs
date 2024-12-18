using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NomDuProjet.Models;
using NomDuProjet.Models;

namespace NomDuProjet.Data
{
    public class NomDuProjetContext : IdentityDbContext
    {
        public NomDuProjetContext(DbContextOptions<NomDuProjetContext> options)
            : base(options)
        {
        }

        //! définition des entity à migrer + lien entre mes objets et tables
        //? DbSet fournis les outils crud comme doctrinne en php
        public DbSet<Movie> Movie { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
    }
}
