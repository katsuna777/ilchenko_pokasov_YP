using Microsoft.EntityFrameworkCore;
using RelationsDemo.Models;

namespace RelationsDemo.Data;

public class TravelContext : DbContext
{
    public DbSet<Tour> Tours => Set<Tour>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<TravelType> Types => Set<TravelType>();
    public DbSet<TourType> TourTypes => Set<TourType>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        const string connection =
            "Server=85.236.189.123;Port=13306;Database=isp-44_ilchenko_dm_relations9;" +
            "User ID=isp-44_ilchenko_dm;Password=isp-44_ilchenko_dm;Character Set=utf8mb4;SslMode=None;AllowPublicKeyRetrieval=True;Connection Timeout=10";
        options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 0)));
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Country>(entity =>
        {
            entity.ToTable("Country");
            entity.HasKey(country => country.Code);
            entity.Property(country => country.Code).HasMaxLength(2);
            entity.Property(country => country.Name).HasMaxLength(100);
        });
        model.Entity<Tour>(entity =>
        {
            entity.ToTable("Tour");
            entity.HasKey(tour => tour.Id);
            entity.Property(tour => tour.CountryCode).HasColumnName("Country_Code").HasMaxLength(2);
            entity.Property(tour => tour.Price).HasPrecision(19, 4);
            entity.Ignore(tour => tour.StatusText);
            entity.Ignore(tour => tour.TypeNames);
            entity.HasOne(tour => tour.Country).WithMany(country => country.Tours)
                .HasForeignKey(tour => tour.CountryCode).OnDelete(DeleteBehavior.Restrict);
        });
        model.Entity<TravelType>(entity =>
        {
            entity.ToTable("Type");
            entity.HasKey(type => type.Id);
        });
        model.Entity<TourType>(entity =>
        {
            entity.ToTable("TypeOfTour");
            entity.HasKey(link => new { link.TourId, link.TypeId });
            entity.HasOne(link => link.Tour).WithMany(tour => tour.TypeOfTours)
                .HasForeignKey(link => link.TourId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(link => link.Type).WithMany(type => type.TypeOfTours)
                .HasForeignKey(link => link.TypeId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
