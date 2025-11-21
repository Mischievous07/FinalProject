using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;





namespace FinalProject.Data
{
  
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<BreakfastFood> BreakfastFoods { get; set; }
        public DbSet<FavoriteShow> FavoriteShows { get; set; }
    }
}
