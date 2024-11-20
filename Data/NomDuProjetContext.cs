using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace NomDuProjet.Data
{
    public class NomDuProjetContext : DbContext
    {
        public NomDuProjetContext (DbContextOptions<NomDuProjetContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Movie> Movie { get; set; } = default!;
    }
}
