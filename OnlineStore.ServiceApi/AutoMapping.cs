using AutoMapper;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.DataAccess.PagedList;
using OnlineStore.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.MvcApplication
{
    /// <summary>
    /// AutoMapper profile.
    /// </summary>
    public class AutoMapping : Profile 
    {
        public AutoMapping()
        {
            var prod = new SaleDto();

            CreateMap(typeof(PagedList<>), typeof(PagedList<>));

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<PagedList<Customer>, PagedList<CustomerDto>>();

            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName.Substring(0, 1)}. {src.Customer.LastName}"))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Products.Select(s => new SelectDto { Id = s.Id, Name = s.ProductName }).ToList()));
            CreateMap<SaleDto, Sale>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Product.Select(s => new Product { Id = s.Id, ProductName = s.Name }).ToList()));
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
