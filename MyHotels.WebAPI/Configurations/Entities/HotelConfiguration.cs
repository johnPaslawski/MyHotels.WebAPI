using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyHotels.WebAPI.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel { Id = 1, CountryId = 1, Name = "Hotel Piast", Adress = "Sowa 2, 30-324 Wrocław", Rating = 4.0},
                new Hotel { Id = 2, CountryId = 1, Name = "Hotel Kołodziej", Adress = "Kury 4, 19-969 Kraków", Rating = 4.5},
                new Hotel { Id = 3, CountryId = 2, Name = "Hotel Eindhoven", Adress = "Walsfdorf Strasse 4, 10-322 Berlin", Rating = 4.7},
                new Hotel { Id = 4, CountryId = 2, Name = "Hotel SturmUndDrang", Adress = "Einsatz Platz 32, 28-001 Lipsk", Rating = 4.9},
                new Hotel { Id = 5, CountryId = 3, Name = "Hotel Welcome Folks", Adress = "South 667 Avenue 6, 82-937 Cincinnatti", Rating = 5.0}
                );
        }
    }
}
