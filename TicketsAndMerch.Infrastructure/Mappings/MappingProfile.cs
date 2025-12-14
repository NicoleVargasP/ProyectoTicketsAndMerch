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

            CreateMap<Order, UserOrderDto>()
    .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src =>
       src.Merch != null ? src.Merch.MerchName : src.Ticket.Concert.Title))
    .ForMember(dest => dest.OrderType, opt => opt.MapFrom(src => src.MerchId != null ? "Merch" : "Ticket"))
    .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.DateOrder ?? DateTime.Now))
    .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.OrderAmount ?? 0))
    .ForMember(dest => dest.PaymentState, opt => opt.MapFrom(src => src.State ?? "Pendiente"));



        }
    }
}
