using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;
using Models.Database_Models;
using Property = Models.Database_Models.Property;

namespace DataAccess.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Property> properties { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Features> Features { get; set; }
        public DbSet<Newsletter> Newsletter { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<AdditionalDetails> AdditionalDetails { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Logo> Logos { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<FaqQue> FaqQues { get; set; }
        public DbSet<FaqAns> FaqAns { get; set; }
        public DbSet<Blog> Blogs { get; set; }


    }
}