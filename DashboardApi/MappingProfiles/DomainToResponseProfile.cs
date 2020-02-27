using AutoMapper;
using DashboardApi.Contracts.Responses;
using DashboardApi.Models;
using System.Linq;

namespace DashboardApi.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            //        CreateMap<Post, PostResponse>()
            //.ForMember(dest => dest.Tags, opt =>
            //    opt.MapFrom(src => src.Tags.Select(x => new TagResponse { Name = x.TagName })));

            //        CreateMap<Tag, TagResponse>();
            CreateMap<Customer, CustomerResponse>()
                .ForMember(dest => dest.Orders, opt =>
                    opt.MapFrom(src => src.Orders.Select(t => new OrderResponse
                    {
                        Id = t.Id,
                        Total = t.Total,
                        Placed = t.Placed,
                        Completed = t.Completed,
                    })));

            CreateMap<Order, CustomerOrderResponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Cutomer.Name));
            CreateMap<OrderGrpByState, OrderGrpByStateResponse>();
            CreateMap<OrderGrpByCustomer, OrderGrpByCustomerResponse>();
            CreateMap<Order, OrderResponse>();

            CreateMap<Server, ServerResponse>();
        }
    }
}
