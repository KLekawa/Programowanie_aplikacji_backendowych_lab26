using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Context;

public class ContactsDbContext : IdentityDbContext<CrmUser, CrmRole, string>
{
    public DbSet<Person> People { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlite("Data Source=..\\Infrastructure\\contacts.db");
    // }

    public ContactsDbContext()
    {
    }

    public ContactsDbContext(DbContextOptions<ContactsDbContext> options) :
        base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // wymagane przez Identity
        
        builder.Entity<CrmUser>(entity =>
        {
            entity.Property(u => u.FirstName).HasMaxLength(100);
            entity.Property(u => u.LastName).HasMaxLength(100);
            entity.Property(u => u.Department).HasMaxLength(100);
            entity.HasIndex(u => u.Email).IsUnique();
        });

        var user1 = new CrmUser
        {
            Id = "a72c4b08-d477-4e10-960c-743b8174ba93",
            FirstName = "Adam",
            LastName = "Kowalski",
            FullName = "Adam Michał Kowalski",
            Email = "adam@wsei.edu.pl",
            Department = "Sprzedaż",
            Status = SystemUserStatus.Active,
            CreatedAt = new DateTime(2026, 4, 2, 10, 0 ,0),
            UserName = "Adam",
            NormalizedUserName = "ADAM",
            EmailConfirmed = true,
            NormalizedEmail = "ADAM@WSEI.EDU.PL",
            SecurityStamp = "11111111-aaaa-bbbb-cccc-111111111111"
        };
        
        var user2 = new CrmUser
        {
            Id = "490e4c79-6737-4431-bfaa-26e478489bc3",
            FirstName = "Ewa",
            LastName = "Nowak",
            FullName = "Ewa Julia Nowak",
            Email = "ewa@wsei.edu.pl",
            Department = "Sprawy wewnętrzne",
            Status = SystemUserStatus.Active,
            CreatedAt = new DateTime(2026, 4, 3, 18, 24 ,42),
            UserName = "Ewa",
            NormalizedUserName = "EWA",
            EmailConfirmed = true,
            NormalizedEmail = "EWA@WSEI.EDU.PL",
            SecurityStamp = "22222222-aaaa-bbbb-cccc-222222222222"
        };
        
        
        PasswordHasher<CrmUser> passwordHasher = new PasswordHasher<CrmUser>();
        user1.PasswordHash = passwordHasher.HashPassword(user1, "user1Haslo");
        user2.PasswordHash = passwordHasher.HashPassword(user2, "user2Haslo");
        
        builder.Entity<CrmUser>().HasData(user1, user2);

        builder.Entity<CrmRole>(entity =>
        {
            entity.Property(r => r.Name).HasMaxLength(20);
        });

        builder.Entity<CrmRole>().HasData(
            new CrmRole
            {
                Id = "0f6c6e4a-8a44-4c39-b4dd-111111111111",
                Name = UserRole.Administrator.ToString(),
                NormalizedName = UserRole.Administrator.ToString().ToUpper(),
                Description = "Administrator systemu"
            },
            new CrmRole
            {
                Id = "0f6c6e4a-8a44-4c39-b4dd-222222222222",
                Name = UserRole.SupportAgent.ToString(),
                NormalizedName = UserRole.SupportAgent.ToString().ToUpper(),
                Description = "Support Agent"
                
            }
        );

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = "a72c4b08-d477-4e10-960c-743b8174ba93",
                RoleId = "0f6c6e4a-8a44-4c39-b4dd-111111111111"
            },
            new IdentityUserRole<string>
            {
                UserId = "490e4c79-6737-4431-bfaa-26e478489bc3",
                RoleId = "0f6c6e4a-8a44-4c39-b4dd-222222222222"
            }
        );
        
        // Konfiguracji mapowania dziedziczenia TPH
        // Jedna tabela do przechowywnia wszystkich typów kontaktów
        builder.Entity<Contact>()
            .HasDiscriminator<string>("ContactType")
            .HasValue<Person>("Person")
            .HasValue<Company>("Company")
            .HasValue<Organization>("Organization");

        builder.Entity<Contact>(entity =>
        {
            entity.Property(p => p.Email).HasMaxLength(200);
            entity.Property(p => p.Phone).HasMaxLength(20);
            entity.Property(p => p.Status).HasConversion<string>();
        });
        
        builder.Entity<Person>(entity =>
        {
            entity.Property(p => p.BirthDate).HasColumnType("date");
            entity.Property(p => p.Gender).HasConversion<string>();
            // entity.Property(p => p.Status).HasConversion<string>();
            entity.Property(p => p.FirstName).HasMaxLength(100);
            entity.Property(p => p.LastName).HasMaxLength(200);
            entity.Property(p => p.MiddleName).HasMaxLength(100);
            entity.Property(p => p.Position).HasMaxLength(100);
        });
        
        // definicja związku
        builder.Entity<Person>()
            .HasOne(p => p.Employer)
            .WithMany(e => e.Employees);

        
        // definicja związku        
        builder.Entity<Organization>()
            .HasMany(o => o.Members)
            .WithOne(p => p.Organization);
        
        // przykładowa firma
        builder.Entity<Company>(entity =>
        {
            entity.HasData(
                new Company()
                {
                    Id = Guid.Parse("516A34D7-CCFB-4F20-85F3-62BD0F3AF271"),
                    Name = "WSEI",
                    Industry = "edukacja",
                    Phone = "123567123",
                    Email = "biuro@wsei.edu.pl",
                    Website = "https://wsei.edu.pl",
                }
            );
        });
        
        var address = new
        {
            City = "Kraków",
            Country = "Poland",
            PostalCode = "25-009",
            Street = "ul. Św. Filipa 17",
            Type = AddressType.Correspondence,
            // id osoby, która dodana jest niżej
            ContactId = Guid.Parse("3d54091d-abc8-49ec-9590-93ad3ed5458f")
        };
        
        // przykładowe kontakty typu Person
        builder.Entity<Person>(entity =>
        {
            entity.HasData(
                new
                {
                    Id = Guid.Parse("3d54091d-abc8-49ec-9590-93ad3ed5458f"),
                    FirstName = "Adam",
                    LastName = "Nowak",
                    Gender = Gender.Male,
                    Status = ContactStatus.Active,
                    Email = "adam@wsei.edu.pl",
                    Phone = "123456789",
                    BirthDate = DateTime.Parse("2001-01-11"),
                    Position = "Programista",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new 
                {
                    Id = Guid.Parse("B4DCB17C-F875-43F8-9D66-36597895A466"),
                    FirstName = "Ewa",
                    LastName = "Kowalska",
                    Gender = Gender.Female,
                    Status = ContactStatus.Blocked,
                    Email = "ewa@wsei.edu.pl",
                    Phone = "123123123",
                    BirthDate = DateTime.Parse("2001-01-11"),
                    Position = "Tester",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
        });
        //mapowanie adresu jako typu osadzonej w encji Contact 
        builder.Entity<Contact>()
            .OwnsOne(c => c.Address)
            .HasData(address);
        
        //Konfiguracja Notes i Tags???
        
    }
}