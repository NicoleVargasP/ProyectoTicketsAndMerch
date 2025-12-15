using AutoMapper;
using TicketsAndMerch.Core.CustomEntities;
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

            CreateMap<BuyTicketDto, BuyTicket>()
             .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<BuyTicket, BuyTicketDto>();

            CreateMap<BuyMerchDto, BuyMerch>()
            .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<BuyMerch, BuyMerchDto>();


            CreateMap<Security, SecurityDto>();
            CreateMap<SecurityDto, Security>();

            CreateMap<UserOrder, UserOrderDto>()
            .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.ItemName))
            .ForMember(dest => dest.OrderType, opt => opt.MapFrom(src => src.OrderType))
            .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.PaymentState, opt => opt.MapFrom(src => src.PaymentState))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));



        }
    }
}
