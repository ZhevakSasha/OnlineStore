using AutoMapper;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.MvcApplication.Models;
using System.Collections.Generic;

namespace OnlineStore.MvcApplication
{
    public class AutoMapping : Profile 
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<ProductDto, ProductViewModel>().ReverseMap();
            CreateMap<CustomerDto, CustomerViewModel>().ReverseMap();
        }
    }
}
