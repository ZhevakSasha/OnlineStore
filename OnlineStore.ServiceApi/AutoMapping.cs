using AutoMapper;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.DataAccess.PagedList;
using OnlineStore.Domain.Models;

namespace OnlineStore.MvcApplication
{
    /// <summary>
    /// AutoMapper profile.
    /// </summary>
    public class AutoMapping : Profile 
    {
        public AutoMapping()
        {
            CreateMap(typeof(PagedList<>), typeof(PagedList<>));

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<PagedList<Customer>, PagedList<CustomerDto>>();

            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName.Substring(0, 1)}. {src.Customer.LastName}"))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));
            CreateMap<SaleDto, Sale>();
        }

        //public class Converter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
        //{
        //    public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        //    {
        //        return new PagedList<TDestination>()
        //        {
        //            PageSize = source.PageSize,
        //            TotalCount = source.TotalCount,
        //            TotalPages = source.TotalPages,
        //            CurrentPage = source.CurrentPage,
        //        };
        //    }

        //}
    }
}
