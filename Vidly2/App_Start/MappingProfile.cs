using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly2.Dtos;
using Vidly2.Models;

namespace Vidly2.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerDto>());
            config.CreateMapper();
            config = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDto, Customer>());
            config.CreateMapper();
        }
    }
}