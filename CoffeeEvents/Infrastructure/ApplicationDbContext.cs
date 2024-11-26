using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid, 
    IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
       private readonly string _connectionString;
    
    public DbSet<DynamicFieldType> DynamicFieldTypes { get; init; }
    public DbSet<EntryFieldValue> EntryFieldValues { get; init; }
    public DbSet<EventSignupEntry> EventSignupEntries { get; init; }
    public DbSet<EventSignupForm> EventSignupForms { get; init; }
    public DbSet<FormDynamicField> FormDynamicFields { get; init; }
    public DbSet<OrganizerContacts> OrganizerContacts { get; init; }
    public DbSet<UserEvent> UsersEvents { get; init; }
    
    public ApplicationDbContext(IConfiguration configuration)
    {
        const string connStringName = "DefaultConnection";
        
        var readedConnString = configuration.GetConnectionString(connStringName);
        _connectionString = readedConnString ?? 
                            throw new Exception($"Connection string \"{connStringName}\" wasn't found in configuration.");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DynamicFieldType>().HasData(
            new DynamicFieldType
            {
                Id = Guid.NewGuid(),
                Title = "string"
            },
            new DynamicFieldType
            {
                Id = Guid.NewGuid(),
                Title = "number"
            }
        );
        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .HasKey(entity => entity.UserId);
        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .HasKey(entity => new { entity.RoleId, entity.UserId } );
        modelBuilder.Entity<IdentityUserToken<Guid>>()
            .HasKey(entity => entity.UserId);
        
    }
}