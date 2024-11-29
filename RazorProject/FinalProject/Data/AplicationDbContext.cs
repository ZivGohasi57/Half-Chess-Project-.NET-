using Microsoft.EntityFrameworkCore;
using FinalProject.Model;



namespace FinalProject.Data
{
    public class ApplicationDbContext : DbContext
    {



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TblUsers> Users { get; set; }
        public DbSet<TblGames> Games { get; set; }
        public DbSet<TblMoves> Moves { get; set; }
        public DbSet<TblQueryResults> QueryResults { get; set; }



        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblUsers>()
                .HasMany(u => u.QueryResults)
                .WithOne(q => q.User)
                .HasForeignKey(q => q.UserId);


        }
    }
}