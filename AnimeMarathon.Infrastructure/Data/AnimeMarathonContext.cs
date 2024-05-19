using AnimeMarahon.Core.Entities;
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
        public Microsoft.EntityFrameworkCore.DbSet<Comment> Comments { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Genre> Genres { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Rating> Ratings { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UsersRatings> UsersRatings { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<AnimeGenre> AnimeGenre { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UsersAnimes> UsersAnimes { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<AnimeCategory> AnimeCategories { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Category> Categories { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("desktop-6m2bjpi\\sqlexpress.ANIMEMARATHON_DB.dbo");
        //}
    }

    
}
