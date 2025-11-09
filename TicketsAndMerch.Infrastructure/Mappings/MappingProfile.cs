using AutoMapper;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Infrastructure.DTOs;

namespace TicketsAndMerch.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Concert, ConcertDto>();
            CreateMap<ConcertDto, Concert>();

            CreateMap<Merch, MerchDto>();
            CreateMap<MerchDto, Merch>();

            
            CreateMap<Ticket, TicketDto>();
            CreateMap<TicketDto, Ticket>();

            
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentDto, Payment>();

            
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
