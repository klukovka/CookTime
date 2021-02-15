namespace CookTime.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CookBookContext : DbContext
    {
        public CookBookContext()
            : base("name=CookBookContext")
        {
        }

        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receipt>()
                .Property(e => e.Proteins)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Receipt>()
                .Property(e => e.Fats)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Receipt>()
                .Property(e => e.Carbohydrates)
                .HasPrecision(18, 0);
        }
    }
}
