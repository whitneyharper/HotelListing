using HotelListing.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData
                (
                    new Country
                    {
                        Id = 1,
                        Name = "Jamaica",
                        Abbreviation = "JM"
                    },

                    new Country
                    {
                        Id = 2,
                        Name = "United States of America",
                        Abbreviation = "USA"
                    },

                    new Country
                    {
                        Id = 3,
                        Name = "New Zealand",
                        Abbreviation = "NZ"
                    }
                );

            builder.Entity<Hotel>().HasData
                (
                   new Hotel
                   {
                       Id = 1,
                       Name = "Sandals Resort and Spa",
                       Address = "Norman Manley Blvd, Negril, Jamaica",
                       CountryId = 1,
                       Rating = 4.5
                   },

                   new Hotel
                   {
                       Id = 2,
                       Name = "Four Seasons Resort and Residences Jackson Hole",
                       Address = "7680 Granite Loop Rd, Teton Village, WY 83025",
                       CountryId = 2,
                       Rating = 3.0
                   },

                   new Hotel
                   {
                       Id = 3,
                       Name = "Paihia Beach Resort & Spa",
                       Address = "130 Marsden Road, Bay Of Islands, Paihia 0200, New Zealand",
                       CountryId = 3,
                       Rating = 5.0
                   }
                );
        }
    }
}
