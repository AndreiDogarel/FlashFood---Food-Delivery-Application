using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FlashFood.Models;

namespace User.Management.Data.Data.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.OrderDetails.Select(od => od.Dish)));
            CreateMap<Dish, DishDto>();
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<Menu, MenuDto>();
            CreateMap<Feedback, FeedbackDto>();
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
