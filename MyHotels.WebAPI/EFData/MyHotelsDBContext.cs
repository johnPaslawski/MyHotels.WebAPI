using Domain;
using Microsoft.EntityFrameworkCore;
using MyHotels.WebAPI.Configurations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHotels.WebAPI.EFData
{
    public class MyHotelsDBContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        public MyHotelsDBContext(DbContextOptions options) : base(options)
        {
        }

        //wypełniam danymi testowymi
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
        }

        

    }
}
