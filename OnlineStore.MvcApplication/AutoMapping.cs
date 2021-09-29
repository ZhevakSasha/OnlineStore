using AutoMapper;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.DataAccess.DataModel;

namespace OnlineStore.MvcApplication
{
    public class AutoMapping : Profile 
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
