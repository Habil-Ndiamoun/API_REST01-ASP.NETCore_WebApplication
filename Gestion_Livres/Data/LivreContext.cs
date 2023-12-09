using Gestion_Livres.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Livres.Data
{
    public class LivreContext : DbContext
    {
        public LivreContext(DbContextOptions<LivreContext> options) : base(options)
        {

        }
        public DbSet<Livre> Livres { get; set; }
        public DbSet<Exemplaire> Exemplaires { get; set; }
    }
}
