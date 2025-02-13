using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .Property(c => c.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        modelBuilder.Entity<Note>()
            .Property(n => n.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Notes)
            .WithOne(n => n.Category)
            .HasForeignKey(n => n.CategoryId);
    }
}