using Microsoft.EntityFrameworkCore;
using La_mia_pizzeria_1_n.Models;

namespace La_mia_pizzeria_1_n.Database {
    public class PizzaContext : DbContext {

        public DbSet<Pizza> Pizze { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Data Source=localhost;Database=MiaPizzeriaEF/1-n;" +
            "Integrated Security=True;TrustServerCertificate=True");
        }

    }
}
