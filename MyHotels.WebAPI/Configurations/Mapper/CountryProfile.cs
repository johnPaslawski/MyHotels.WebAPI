using AutoMapper;
using Domain;
using MyHotels.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHotels.WebAPI.Configurations.Mapper
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            // ReverseMap umożliwia także mapowanie w drugą stronę, bez tego przy próbie mapowania innej niż 
            // zadkeklarowana <T, T2> dostalibyśmy błąd
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
        }
    }
}
