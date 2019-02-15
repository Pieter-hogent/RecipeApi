using Microsoft.EntityFrameworkCore;
using RecipeApi.Models;
using System;

namespace RecipeApi.Data
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Recipe>()
                .HasMany(p => p.Ingredients)
                .WithOne()
                .IsRequired()
                .HasForeignKey("RecipeId"); //Shadow property
            modelBuilder.Entity<Recipe>().Property(r => r.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Recipe>().Property(r => r.Chef).HasMaxLength(50);
            modelBuilder.Entity<Ingredient>().Property(r => r.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Ingredient>().Property(r => r.Unit).HasMaxLength(10);

            //Another way to seed the database
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe { Id = 1, Name = "Spaghetti", Created = DateTime.Now },
                 new Recipe { Id = 2, Name = "Tomatensoep", Created = DateTime.Now }
  );

            modelBuilder.Entity<Ingredient>().HasData(
                    //Shadow property can be used for the foreign key, in combination with anaonymous objects
                    new { Id = 1, Name = "Tomatoes", Amount = (double?)0.75, Unit = "liter", RecipeId = 1 },
                    new { Id = 2, Name = "Minced Meat", Amount = (double?)500, Unit = "grams", RecipeId = 1 },
                    new { Id = 3, Name = "Onion", Amount = (double?)2, RecipeId = 1 }
                 );
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}