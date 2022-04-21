using Microsoft.EntityFrameworkCore;
using Pomodorii.Models;

namespace Pomodorii.Api
{
    /// <summary>
    /// Permet d'intéragir avec la BD via EntityframeWork
    /// l'option de connection à la BD est définie lors de l'instanciation de l'objet dans Pomodorii.Web / program.cs
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="options">options de connexion à la bd</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// permet de manipuler les données de l'entité "Tomate"
        /// </summary>
        public DbSet<Tomate> Tomates { get; set; }

        /// <summary>
        /// permet de manipuler les données de l'entité "Semi"
        /// </summary>
        public DbSet<Semi> Semis { get; set; }


        /// <summary>
        /// initilise la BD avec qq valeurs par défauts
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            int i = 1;
            modelBuilder.Entity<Tomate>().HasData(new Tomate { Id = i++, Nom = "Alambra", Description = "très bon", ImageUrl = "img/tomates/ALAMBRA.gif" });
            modelBuilder.Entity<Tomate>().HasData(new Tomate { Id = i++, Nom = "Andine Cornue", Description = "très bon", ImageUrl = "img/tomates/Andine Cornue.gif" });
            modelBuilder.Entity<Tomate>().HasData(new Tomate { Id = i++, Nom = "Cobra", Description = "très bon", ImageUrl = "img/tomates/COBRA.gif" });
            modelBuilder.Entity<Tomate>().HasData(new Tomate { Id = i++, Nom = "Coeur-de-boeuf", Description = "très bon", ImageUrl = "img/tomates/coeur-de-boeuf.gif" });
            modelBuilder.Entity<Tomate>().HasData(new Tomate { Id = i++, Nom = "noire-de-crimee", Description = "très bon", ImageUrl = "img/tomates/noire-de-crimee.gif" });
            modelBuilder.Entity<Tomate>().HasData(new Tomate { Id = i++, Nom = "russe-rouge", Description = "très bon", ImageUrl = "img/tomates/russe-rouge.gif" });
            modelBuilder.Entity<Tomate>().HasData(new Tomate { Id = i++, Nom = "tomito-f1", Description = "très bon", ImageUrl = "img/tomates/tomito-f1.gif" });
            // 1 semi correspond à une et une seule tomate
            modelBuilder.Entity<Semi>().HasData(new Semi { Id = i++, TomateId = 1, Date = DateTime.Today });
            modelBuilder.Entity<Semi>().HasData(new Semi { Id = i++, TomateId = 2, Date = DateTime.Today });
            modelBuilder.Entity<Semi>().HasData(new Semi { Id = i++, TomateId = 2, Date = DateTime.Today.AddDays(-2) });
        }
    }
}
