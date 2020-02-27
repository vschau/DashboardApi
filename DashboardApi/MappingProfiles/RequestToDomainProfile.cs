using AutoMapper;
using DashboardApi.Contracts.Requests;
using DashboardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardApi.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<OrderCreateRequest, Order>();
        }
    }
}
