
using Core.Security.Entities;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistance.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Company> Companies { get; set; }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    public BaseDbContext(DbContextOptions<BaseDbContext> dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            base.OnConfiguring(
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("ECommerceConnectionString")));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(p =>
        {
            p.ToTable("Products").HasKey(p => p.Id);
            p.Property(p => p.Id).HasColumnName("Id");
            p.Property(p => p.CategoryId).HasColumnName("CategoryId");
            p.Property(p => p.BrandId).HasColumnName("BrandId");
            p.Property(p => p.ColorId).HasColumnName("ColorId");
            p.Property(p => p.CompanyId).HasColumnName("CompanyId");
            p.Property(p => p.Name).HasColumnName("Name");
            p.Property(p => p.Quantity).HasColumnName("Quantity");
            p.Property(p => p.Cost).HasColumnName("Cost");
            p.Property(p => p.CreatedDate).HasColumnName("CreatedDate");
            p.Property(p => p.Description).HasColumnName("Description");
            p.HasOne(p => p.Company);
            p.HasOne(p => p.Category);
            p.HasOne(p => p.Brand);
            p.HasOne(p => p.Color);
        });

        modelBuilder.Entity<Company>(c =>
        {
            c.ToTable("Companies").HasKey(c => c.Id);
            c.Property(c => c.Id).HasColumnName("Id");
            c.Property(c => c.Name).HasColumnName("Name");
            c.Property(c => c.State).HasColumnName("State");
            c.Property(c => c.City).HasColumnName("City");
            c.Property(c => c.Description).HasColumnName("Description");
            c.Property(c => c.JoinDate).HasColumnName("JoinDate");
            c.HasMany(c => c.Products);
        });

        modelBuilder.Entity<Brand>(b =>
        {
            b.ToTable("Brands").HasKey(b => b.Id);
            b.Property(b => b.Id).HasColumnName("Id");
            b.Property(b => b.Name).HasColumnName("Name");
            b.HasMany(b => b.Products);   
        });

        modelBuilder.Entity<Color>(c => {
            c.ToTable("Colors").HasKey(c => c.Id);
            c.Property(c => c.Id).HasColumnName("Id");
            c.Property(c => c.Name).HasColumnName("Name");
            c.HasMany(c => c.Products);
        });

        modelBuilder.Entity<Category>(c => {
            c.ToTable("Categories").HasKey(c => c.Id);
            c.Property(c => c.Id).HasColumnName("Id");
            c.Property(c => c.Name).HasColumnName("Name");
            c.Property(c => c.Description).HasColumnName("Description");
            c.HasMany(c => c.Brands);
        });

        modelBuilder.Entity<User>(u =>
        {
            u.ToTable("Users").HasKey(u => u.Id);
            u.Property(u => u.Id).HasColumnName("Id");
            u.Property(u => u.FirstName).HasColumnName("FirstName");
            u.Property(u => u.LastName).HasColumnName("LastName");
            u.Property(u => u.Email).HasColumnName("Email");
            u.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
            u.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
            u.Property(u => u.Status).HasColumnName("Status");
            u.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType");
            u.HasMany(u => u.UserOperationClaims);
            u.HasMany(u => u.RefreshTokens);
        });

        modelBuilder.Entity<RefreshToken>(r =>
        {
            r.ToTable("RefreshTokens").HasKey(r => r.Id);
            r.Property(r => r.Id).HasColumnName("Id");
            r.Property(r => r.UserId).HasColumnName("UserId");
            r.Property(r => r.Token).HasColumnName("Token");
            r.Property(r => r.Expires).HasColumnName("Expires");
            r.Property(r => r.Created).HasColumnName("Created");
            r.Property(r => r.CreatedByIp).HasColumnName("CreatedByIp");
            r.Property(r => r.Revoked).HasColumnName("Revoked");
            r.Property(r => r.RevokedByIp).HasColumnName("RevokedByIp");
            r.Property(r => r.ReplacedByToken).HasColumnName("ReplacedByToken");
            r.Property(r => r.ReasonRevoked).HasColumnName("ReasonRevoked");
            r.HasOne(r => r.User);
        });

        modelBuilder.Entity<UserOperationClaim>(u => {
            u.ToTable("UserOperationClaims").HasKey(u => u.Id);
            u.Property(u => u.Id).HasColumnName("Id");
            u.Property(u => u.UserId).HasColumnName("UserId");
            u.Property(u => u.OperationClaimId).HasColumnName("OperationClaimdId");
            u.HasOne(u => u.User);
            u.HasOne(u => u.OperationClaim);
        });

        modelBuilder.Entity<OperationClaim>(o =>
        {
            o.ToTable("OperationClaims").HasKey(o => o.Id);
            o.Property(o => o.Id).HasColumnName("Id");
            o.Property(o => o.Name).HasColumnName("Name");
        });

        Brand[] brandSeeds = { new(1, "Lojitek"), new(2, "AZUZ"), new(3, "GMSI") };
        modelBuilder.Entity<Brand>().HasData(brandSeeds);

        Color[] colorSeeds = { new(1, "White"), new(2, "Black"), new(3, "Red") };
        modelBuilder.Entity<Color>().HasData(colorSeeds);

        Category[] categorySeeds = { new(1, "Technology", "Technology Products"), new(2, "Clothing", "Clothing Products"), new(3, "Food", "Food Products") };
        modelBuilder.Entity<Category>().HasData(categorySeeds);

        Company[] companySeeds = { new(1, "Bursa Comp", "Since 1999", DateTime.Now, City.Bursa),new(2,"Yalova Comp","Since 2005",DateTime.Now,City.Yalova) };
        modelBuilder.Entity<Company>().HasData(companySeeds);

        Product[] productSeeds = { new(1, 1, 1, 1, 1, 10, 100,"Keyboard", "Low-End Keyboard", DateTime.Now) };
        modelBuilder.Entity<Product>().HasData(productSeeds);
    }
    
}
