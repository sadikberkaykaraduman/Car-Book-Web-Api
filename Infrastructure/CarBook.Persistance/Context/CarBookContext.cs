using CarBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistance.Context
{
    public class CarBookContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=berkay; initial Catalog=CarBook; integrated Security=True; TrustServerCertificate=True;");
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=tcp:carbookdb-1.database.windows.net,1433;" +
                "Initial Catalog=carbookdb;" +
                "Persist Security Info=False;" +
                "User ID=carbookdb;" +
                "Password=Carbook1500?;" +
                "MultipleActiveResultSets=False;" +
                "Encrypt=True;" +
                "TrustServerCertificate=False;" +
                "Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Car relations
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Brand)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.CarBrandId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Car>()
                .HasMany(c => c.CarReviews)
                .WithOne(cr => cr.Car)
                .HasForeignKey(cr => cr.CarReviewCarId)
                .OnDelete(DeleteBehavior.Cascade);

            // CarFeature relations
            modelBuilder.Entity<CarFeature>()
                .HasOne(cf => cf.Car)
                .WithMany(c => c.CarFeatures)
                .HasForeignKey(cf => cf.CarFeatureCarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarFeature>()
                .HasOne(cf => cf.Feature)
                .WithMany(f => f.CarFeatures)
                .HasForeignKey(cf => cf.CarFeatureFeatureId)
                .OnDelete(DeleteBehavior.Cascade);

            // CarDescription relation
            modelBuilder.Entity<CarDescription>()
                .HasOne(cd => cd.Car)
                .WithMany(c => c.CarDescriptions)
                .HasForeignKey(cd => cd.CarDescriptionCarId)
                .OnDelete(DeleteBehavior.Cascade);

            // CarPricing relations
            modelBuilder.Entity<CarPricing>()
                .HasOne(cp => cp.Car)
                .WithMany(c => c.CarPricings)
                .HasForeignKey(cp => cp.CarPricingCarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarPricing>()
                .HasOne(cp => cp.Pricing)
                .WithMany(p => p.CarPricings)
                .HasForeignKey(cp => cp.CarPricingPricingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Blog relation
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Blogs)
                .HasForeignKey(b => b.BlogAuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Blogs)
                .HasForeignKey(b => b.BlogCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // TagCloud relation
            modelBuilder.Entity<TagCloud>()
                .HasOne(tc => tc.Blog)
                .WithMany(b => b.TagClouds)
                .HasForeignKey(tc => tc.TagCloudBlogId)
                .OnDelete(DeleteBehavior.Cascade);

            // Comment relation
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Blog)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.CommentBlogId)
                .OnDelete(DeleteBehavior.Cascade);

            // RentACar relation
            modelBuilder.Entity<RentACar>()
                .HasOne(r => r.Location)
                .WithMany(l => l.RentACars)
                .HasForeignKey(r => r.PickUpLocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RentACar>()
                .HasOne(r => r.Car)
                .WithMany(c => c.RentACars)
                .HasForeignKey(r => r.RentACarCarId)
                .OnDelete(DeleteBehavior.Cascade);

            // RentACarProcess relation
            modelBuilder.Entity<RentACarProcess>()
                .HasOne(rp => rp.Car)
                .WithMany(r => r.RentACarProcesses)
                .HasForeignKey(rp => rp.RentACarProcessCarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RentACarProcess>()
                .HasOne(rp => rp.Customer)
                .WithMany(r => r.RentACarProcesses)
                .HasForeignKey(rp => rp.RentACarProcessCustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Booking relation
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.PickUpLocation)
                .WithMany(r => r.PickUpBooking)
                .HasForeignKey(b => b.PickUpLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.DropOffLocation)
                .WithMany(r => r.DropoffBooking)
                .HasForeignKey(b => b.DropOffLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.BookingCar)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.BookingCarId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // AppUsers relation
            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.AppRole)
                .WithMany(r => r.AppUsers)
                .HasForeignKey(u => u.AppUserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarDescription> CarDescriptions { get; set; }
        public DbSet<CarFeature> CarFeatures { get; set; }
        public DbSet<CarPricing> CarPricings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<FooterAddress> FooterAddresses { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<TagCloud> TagClouds { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<RentACar> RentACars { get; set; }
        public DbSet<RentACarProcess> RentACarProcesses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CarReview> CarReviews { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
    }
}
