﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyHotels.WebAPI.EFData;

namespace MyHotels.WebAPI.Migrations
{
    [DbContext(typeof(MyHotelsDBContext))]
    partial class MyHotelsDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "PL",
                            Name = "Poland"
                        },
                        new
                        {
                            Id = 2,
                            Code = "DE",
                            Name = "Germany"
                        },
                        new
                        {
                            Id = 3,
                            Code = "US",
                            Name = "United States"
                        });
                });

            modelBuilder.Entity("Domain.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Adress = "Sowa 2, 30-324 Wrocław",
                            CountryId = 1,
                            Name = "Hotel Piast",
                            Rating = 4.0
                        },
                        new
                        {
                            Id = 2,
                            Adress = "Kury 4, 19-969 Kraków",
                            CountryId = 1,
                            Name = "Hotel Kołodziej",
                            Rating = 4.5
                        },
                        new
                        {
                            Id = 3,
                            Adress = "Walsfdorf Strasse 4, 10-322 Berlin",
                            CountryId = 2,
                            Name = "Hotel Eindhoven",
                            Rating = 4.7000000000000002
                        },
                        new
                        {
                            Id = 4,
                            Adress = "Einsatz Platz 32, 28-001 Lipsk",
                            CountryId = 2,
                            Name = "Hotel SturmUndDrang",
                            Rating = 4.9000000000000004
                        },
                        new
                        {
                            Id = 5,
                            Adress = "South 667 Avenue 6, 82-937 Cincinnatti",
                            CountryId = 3,
                            Name = "Hotel Welcome Folks",
                            Rating = 5.0
                        });
                });

            modelBuilder.Entity("Domain.Hotel", b =>
                {
                    b.HasOne("Domain.Country", "Country")
                        .WithMany("Hotels")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Domain.Country", b =>
                {
                    b.Navigation("Hotels");
                });
#pragma warning restore 612, 618
        }
    }
}
