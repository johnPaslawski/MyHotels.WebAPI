using AutoMapper;
using Domain;
using MyHotels.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHotels.WebAPI.Configurations.Mapper
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            // ReverseMap umożliwia także mapowanie w drugą stronę, bez tego przy próbie mapowania innej niż 
            // zadkeklarowana <T, T2> dostalibyśmy błąd
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, UpdateHotelDto>().ReverseMap();
            CreateMap<Hotel, HotelDto>().ReverseMap();
        }
    }
}
