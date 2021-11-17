using AutoMapper;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.DataAccess.DataModel;

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

            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName.Substring(0, 1)}. {src.Customer.LastName}"))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));
            CreateMap<SaleDto, Sale>();
        }
    }
}
