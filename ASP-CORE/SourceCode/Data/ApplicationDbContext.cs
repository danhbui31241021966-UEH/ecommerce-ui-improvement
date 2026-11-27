using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using _31241021966_BuiCongDanh.Models;
namespace _31241021966_BuiCongDanh.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options) {
    public DbSet<Movie> Movies {get;set;}
    public DbSet<Genre> Genres {get;set;}
    public DbSet<HoaDon> HoaDons {get;set;}
    public DbSet<ChiTietHoaDon> ChiTietHoaDons {get;set;}
    public DbSet<ShippingDetail> ShippingDetails {get;set;}
    protected override void OnModelCreating(ModelBuilder b) {
        base.OnModelCreating(b);
        b.Entity<Movie>().HasOne(x=>x.GenreObj).WithMany(x=>x.Movies).HasForeignKey(x=>x.GenreID).OnDelete(DeleteBehavior.Restrict);
        b.Entity<ChiTietHoaDon>().HasOne(x=>x.MovieObj).WithMany().HasForeignKey(x=>x.MaMovie).OnDelete(DeleteBehavior.Restrict);
        b.Entity<HoaDon>().HasOne(x=>x.ShippingDetail).WithOne(x=>x.HoaDonObj).HasForeignKey<ShippingDetail>(x=>x.MaHoaDon);
    }
}
