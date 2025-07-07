using Microsoft.EntityFrameworkCore;

namespace Data.Entities.DatabaseDB
{
    /// <summary>
    /// ExampleDbContext is a placeholder for a real database context.
    /// It is used to demonstrate how to set up a DbContext with Entity Framework Core.
    /// This context can be generated form a database using tools like EF Core Power Tools or Scaffold-DbContext.
    /// </summary>
    public class ExampleDbContext : DbContext
    {
        /// <summary>
        /// I like to implement a server version for MariaDB. I work on diffrent versions of MariaDB and I want to be sure that the code will work on all of them.
        /// I had problems with 
        /// </summary>
        public MariaDbServerVersion serverVersion = new(new Version(11, 4, 2));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Database=mockdb;User=mockuser;Password=mockpassword;",
                serverVersion
            );
        }

        // Example DbSets
        public DbSet<MockEntity> MockEntities { get; set; }
        public DbSet<RelatedEntity> RelatedEntities { get; set; }
        public DbSet<ParentEntity> ParentEntities { get; set; }
        public DbSet<ChildEntity> ChildEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChildEntity>()
                .HasOne(c => c.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(c => c.ParentId);

            modelBuilder.Entity<MockEntity>()
                .HasOne(m => m.Related)
                .WithOne(r => r.Mock)
                .HasForeignKey<RelatedEntity>(r => r.MockEntityId);
        }
    }
}
