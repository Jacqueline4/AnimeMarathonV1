using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;



namespace AnimeMarathon.Data.Data
{
    public class AnimeMarathonContext : DbContext
    {
        public AnimeMarathonContext(DbContextOptions options) : base(options) 
        {
        
        }
        public Microsoft.EntityFrameworkCore.DbSet<Anime> Animes { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UsersAnimes> UsersAnimes { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Comment> Comments { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Genre> Genres { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Rating> AnimeRatings { get; set; }
        //public Microsoft.EntityFrameworkCore.DbSet<UsersRatings> UsersRatings { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<AnimeGenre> AnimeGenre { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<AnimeCategory> AnimeCategories { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Category> Categories { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("desktop-6m2bjpi\\sqlexpress.ANIMEMARATHON_DB.dbo");

        //    optionsBuilder.
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasMany(x => x.UserAnimes)
                .WithOne(x => x.Ususario)
                .HasForeignKey(x => x.UsuarioId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.UserRatings)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Anime>()
                .HasMany(x => x.UsersAnime)
                .WithOne(x => x.Anime)
                .HasForeignKey(x => x.AnimeId);

            modelBuilder.Entity<Anime>()
                .HasMany(x => x.AnimeGenres)
                .WithOne(x => x.Anime)
                .HasForeignKey(x => x.AnimeId);

            modelBuilder.Entity<Anime>()
               .HasMany(x => x.AnimeCategories)
               .WithOne(x => x.Anime)
               .HasForeignKey(x => x.AnimeId);

            modelBuilder.Entity<Anime>()
                .HasMany(x => x.AnimeRatings)
                .WithOne(x => x.Anime)
                .HasForeignKey(x => x.AnimeId);

            modelBuilder.Entity<Genre>()
                .HasMany(x => x.AnimesGenre)
                .WithOne(x => x.Genero)
                .HasForeignKey(x => x.GeneroId);

            modelBuilder.Entity<Category>()
                .HasMany(x => x.AnimesCategory)
                .WithOne(x => x.Categoria)
                .HasForeignKey(x => x.CategoriaId);

            modelBuilder.Entity<Comment>()
           .HasOne(c => c.User)
           .WithMany(u => u.Comments)
           .HasForeignKey(c => c.UsuarioId) 
           .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<BaseEntity>().HasKey(x => x.Id);

            modelBuilder.Entity<User>().HasIndex(x => x.Name);

            modelBuilder.Entity<Anime>().HasIndex(x => x.Title);
            modelBuilder.Entity<Anime>().HasIndex(x => x.Status);
            modelBuilder.Entity<Anime>().HasIndex(x => x.Subtype);

            modelBuilder.Entity<Genre>().HasIndex(x => x.Name);

            modelBuilder.Entity<Category>().HasIndex(x => x.Name);
        }

    }

    
}
