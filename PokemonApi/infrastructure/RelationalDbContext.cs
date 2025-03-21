using Microsoft.EntityFrameworkCore;
using PokemonApi.infrastructure.Entities;
namespace PokemonApi.Infrastructure
{
    public class RelationalDbContext : DbContext
    {
        public DbSet<PokemonEntity> Pokemons { get; set; }

        public DbSet<HobbyEntity> Hobbies { get; set; }

        public DbSet<BookEntity> Books { get; set; }

        public RelationalDbContext(DbContextOptions<RelationalDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PokemonEntity>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Type).IsRequired().HasMaxLength(50);
                entity.Property(s => s.Level).IsRequired();
                entity.Property(s => s.Attack).IsRequired();
                entity.Property(s => s.Defense).IsRequired();
                entity.Property(s => s.Speed).IsRequired();
                entity.Property(s => s.Height).IsRequired();
            });

            modelBuilder.Entity<HobbyEntity>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Top).IsRequired();
            });

            modelBuilder.Entity<BookEntity>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Title).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Author).IsRequired().HasMaxLength(100);
                entity.Property(s => s.PublishedDate).IsRequired();
            });
        }
    }
}