using AutoMapper;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.MvcApplication.Models;
using System.Collections.Generic;

namespace OnlineStore.MvcApplication
{
    /// <summary>
    /// AutoMapper profile.
    /// </summary>
    public class AutoMapping : Profile 
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductDto, ProductViewModel>().ReverseMap();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerDto, CustomerViewModel>().ReverseMap();

            CreateMap<Sale, SaleDto>().ReverseMap();
            CreateMap<SaleDto, SaleViewModel>().ReverseMap();
        }
    }
}
