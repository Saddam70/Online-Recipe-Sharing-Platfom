using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public  class RecipeDbContext:DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeStep> RecipeSteps { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //It contains details about connection string
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DatabaseHelper.GetConnectionString());
            }


            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			// Fluent API configurations can be added here if needed

			//All Forign Key
			//foreign key for Recipe table
			modelBuilder.Entity<Recipe>().HasOne<User>().WithMany().HasForeignKey(r => r.UserId);

            //forign key for Recipe Step
			modelBuilder.Entity<RecipeStep>().HasOne<Recipe>().WithMany().HasForeignKey(c => c.RecipeId);

			//forign key for Ingredient
			modelBuilder.Entity<Ingredient>().HasOne<Recipe>().WithMany().HasForeignKey(c => c.RecipeId);

            //forign key for Comment
            modelBuilder.Entity<Comment>().HasOne<User>().WithMany().HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Comment>().HasOne<Recipe>().WithMany().HasForeignKey(c => c.RecipeId);
            //		modelBuilder.Entity<Comment>()
            //.HasOne(c => c.User)
            //.WithMany(u => u.Comments)
            //.HasForeignKey(c => c.UserId)
            //.OnDelete(DeleteBehavior.Cascade);

            //		modelBuilder.Entity<Comment>()
            //			.HasOne(c => c.Recipe)
            //			.WithMany(r => r.Comments)
            //			.HasForeignKey(c => c.RecipeId)
            //			.OnDelete(DeleteBehavior.Cascade);
            //forign key for Rating
            modelBuilder.Entity<Rating>().HasOne<User>().WithMany().HasForeignKey(c => c.UserId);

			modelBuilder.Entity<Rating>().HasOne<Recipe>().WithMany().HasForeignKey(c => c.RecipeId);

//			modelBuilder.Entity<Recipe>()
//		.Property(r => r.RecipeId)
//		.ValueGeneratedOnAdd();
//			modelBuilder.Entity<RecipeStep>()
//.Property(r => r.RecipeStepId)
//.ValueGeneratedOnAdd();
//			modelBuilder.Entity<Ingredient>()
//.Property(r => r.IngredientId)
//.ValueGeneratedOnAdd();
//			modelBuilder.Entity<Comment>()
//.Property(r => r.CommentId)
//.ValueGeneratedOnAdd();
//			modelBuilder.Entity<Rating>()
//.Property(r => r.RatingId)
//.ValueGeneratedOnAdd();

		//	modelBuilder.Entity<Comment>()
		//.HasOne(c => c.Recipe)
		//.WithMany(r => r.Comments)
		//.HasForeignKey(c => c.RecipeId);

		//	modelBuilder.Entity<Comment>()
		//		.HasOne(c => c.User)
		//		.WithMany(u => u.Comments)
		//		.HasForeignKey(c => c.UserId);
			// Ensure ID is generated on add
			// Configure User Entity
			//modelBuilder.Entity<User>()
			//             .HasKey(u => u.UserId);

			//         modelBuilder.Entity<User>()
			//             .HasIndex(u => u.Username)
			//             .IsUnique();

			//         modelBuilder.Entity<User>()
			//             .Property(u => u.EmailId)
			//             .IsRequired();

			// Configure Recipe Entity
			//modelBuilder.Entity<Recipe>()
			//    .HasKey(r => r.RecipeId);

			//modelBuilder.Entity<Recipe>()
			//    .Property(r => r.Title)
			//    .IsRequired();


			//modelBuilder.Entity<Recipe>()
			//    .HasMany(r => r.Ingredients)
			//    .WithOne(i => i.Recipe)
			//    .HasForeignKey(i => i.RecipeId);

			//modelBuilder.Entity<Recipe>()
			//    .HasMany(r => r.RecipeSteps)
			//    .WithOne(rs => rs.Recipe)
			//    .HasForeignKey(rs => rs.RecipeId);

			//modelBuilder.Entity<Recipe>()
			//    .HasMany(r => r.Comments)
			//    .WithOne(c => c.Recipe)
			//    .HasForeignKey(c => c.RecipeId);

			//modelBuilder.Entity<Recipe>()
			//    .HasMany(r => r.Ratings)
			//    .WithOne(rt => rt.Recipe)
			//    .HasForeignKey(rt => rt.RecipeId);

			// Configure Ingredient Entity
			//modelBuilder.Entity<Ingredient>()
			//    .HasKey(i => i.IngredientId);

			//modelBuilder.Entity<Ingredient>()
			//    .Property(i => i.Name)
			//    .IsRequired();

			//// Configure RecipeStep Entity
			//modelBuilder.Entity<RecipeStep>()
			//    .HasKey(rs => rs.RecipeStepId);

			//modelBuilder.Entity<RecipeStep>()
			//    .Property(rs => rs.StepNumber)
			//    .IsRequired();
			//modelBuilder.Entity<RecipeStep>()
			//    .HasOne<Recipe>
			//     ().WithMany()
			//    .HasForeignKey(c => c.RecipeId);
			//// Configure Comment Entity
			//modelBuilder.Entity<Comment>()
			//    .HasKey(c => c.CommentId);

			//modelBuilder.Entity<Comment>()
			//.Property(c => c.Content)
			//.IsRequired();

			//modelBuilder.Entity<Comment>()
			//    .HasOne(c => c.User)
			//    .WithMany(u => u.Comments)
			//    .HasForeignKey(c => c.UserId);

			//modelBuilder.Entity<Comment>()
			//    .HasOne(c => c.Recipe)
			//    .WithMany(r => r.Comments)
			//    .HasForeignKey(c => c.RecipeId);

			// Configure Rating Entity
			//modelBuilder.Entity<Rating>()
			//    .HasKey(rt => rt.RatingId);

			//modelBuilder.Entity<Rating>()
			//    .Property(rt => rt.Star)
			//    .IsRequired();

			//modelBuilder.Entity<Rating>()
			//    .HasOne(rt => rt.User)
			//    .WithMany(u => u.Ratings)
			//    .HasForeignKey(rt => rt.UserId);

			//modelBuilder.Entity<Rating>()
			//    .HasOne(rt => rt.Recipe)
			//    .WithMany(r => r.Ratings)
			//    .HasForeignKey(rt => rt.RecipeId);

			//Seed Data for default admin
			modelBuilder.Entity<Admin>().HasData(
                new Admin { EmailId = "admin@gmail.com", Password = "admin123", Role = "Admin" },
                new Admin { EmailId="scott@gmail.com",Password="scott123",Role="Admin"}
                );

            base.OnModelCreating(modelBuilder);
        }
      
    }
}
