using AutoMapper;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.DataAccess.PagedList;
using OnlineStore.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.ServiceApi
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
            

            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName.Substring(0, 1)}. {src.Customer.LastName}"))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.Select(s => new SelectDto { Id = s.Id, Name = s.ProductName }).ToList()));
            CreateMap<SaleDto, Sale>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.Select(s => new Product { Id = s.Id, ProductName = s.Name }).ToList()));
            CreateMap<CustomerSaleReportDto, Customer>()
                .ForMember(dest => dest.Sales, opt => opt.MapFrom(src => src.Sales.Select(s => new Sale {Id = 0, Amount = s.Amount, CustomerId = s.CustomerId, DateOfSale = s.DateOfSale , Products = new List<Product> {  } 
                }).ToList()));
            CreateMap<Customer, CustomerSaleReportDto>()
               .ForMember(dest => dest.Sales, opt => opt.MapFrom(src => src.Sales.Select(s => new SaleWithProductDto
               {
                   Amount = s.Amount,
                   CustomerId = s.CustomerId,
                   DateOfSale = s.DateOfSale,
                   Products = s.Products.Select(p => new ProductDto { Id = p.Id, Price = p.Price, ProductName = p.ProductName, UnitOfMeasurement = p.UnitOfMeasurement}).ToList()
               }).ToList()));
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
