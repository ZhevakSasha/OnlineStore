using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.MvcApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.MvcApplication.Controllers
{
    /// <summary>
    /// Sale controller.
    /// </summary>
    public class SaleController : Controller
    {

        /// <summary>
        /// Sale service.
        /// </summary>
        private ISaleService _sale;

        /// <summary>
        /// Product service.
        /// </summary>
        private IProductService _product;

        /// <summary>
        /// Customer service.
        /// </summary>
        private ICustomerService _customer;

        /// <summary>
        /// Mapper.
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// SaleController constructor.
        /// </summary>
        /// <param name="sale">Sale service</param>
        /// <param name="product">Product service</param>
        /// <param name="customer">Customer service</param>
        /// <param name="mapper">Mapper</param>
        public SaleController(ISaleService sale, IProductService product, ICustomerService customer, IMapper mapper)
        {
            _mapper = mapper;
            _sale = sale;
            _customer = customer;
            _product = product;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        /// <summary>
        /// Takes a list of all sales from the table and passes them into view.
        /// </summary>
        /// <returns>View with sales</returns>
        public IActionResult SaleTable()
        {
            var results = _sale.GetAllSales();
            var sales = _mapper.Map<IEnumerable<SaleViewModel>>(results);
            return View(sales);
        }

        /// <summary>
        /// Takes a list of all sales from the table and passes them into view.
        /// </summary>
        /// <returns>View with sales</returns>
        [HttpGet]
        public IActionResult SaleTable(string searchString)
        {
            ViewData["GetDetails"] = searchString;

            var results = _sale.GetAllSales();
            var sales = _mapper.Map<IEnumerable<SaleViewModel>>(results);
            if (!String.IsNullOrEmpty(searchString))
            {
                sales = sales.Where(s => s.ProductName.Contains(searchString)
                                       || s.CustomerName.Contains(searchString));
            }
            return View(sales);
        }

        /// <summary>
        /// Takes sale data by id from the table and passes them into view.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>View with sale</returns>
        public IActionResult SaleUpdating(int id)
        {

            var sale = _mapper.Map<SaleViewModel>(_sale.FindSaleById(id));
            var productNames = _product.GetAllProductNames();
            var customerNames = _customer.GetAllCustomerNames();
            ViewBag.ProductNames = new SelectList(productNames,"Id","Name");
            ViewBag.CustomerNames = new SelectList(customerNames,"Id","Name");
            return View(sale);
        }

        /// <summary>
        ///  Updates sale data.
        /// </summary>
        /// <param name="sale">Takes saleViewModel object</param>
        /// <returns>SaleTable View</returns>
        [HttpPost]
        public IActionResult SaleUpdating(SaleViewModel sale)
        {
            if (ModelState.IsValid)
            {
                _sale.UpdateSale(_mapper.Map<SaleDto>(sale));
                return RedirectToAction("SaleTable");
            }
            else
            {
                var productNames = _product.GetAllProductNames();
                var customerNames = _customer.GetAllCustomerNames();
                ViewBag.ProductNames = new SelectList(productNames,"Id","Name");
                ViewBag.CustomerNames = new SelectList(customerNames,"Id","Name");
                return View(sale);
            }
        }

        /// <summary>
        /// SaleCreating.
        /// </summary>
        /// <returns>SaleCreating view</returns>
        public IActionResult SaleCreating()
        {
            var productNames = _product.GetAllProductNames();
            var customerNames = _customer.GetAllCustomerNames();
            ViewBag.ProductNames = new SelectList(productNames,"Id","Name");
            ViewBag.CustomerNames = new SelectList(customerNames,"Id","Name");
            return View();
        }

        /// <summary>
        /// Saves sale data.
        /// </summary>
        /// <param name="sale">Takes saleViewModel object</param>
        /// <returns>SaleTable view</returns>
        [HttpPost]
        public IActionResult SaleCreating(SaleViewModel sale)
        {
            if (ModelState.IsValid)
            {
                _sale.CreateSale(_mapper.Map<SaleDto>(sale));
                return RedirectToAction("SaleTable");
            }
            else
            {
                var productNames = _product.GetAllProductNames();
                var customerNames = _customer.GetAllCustomerNames();
                ViewBag.ProductNames = new SelectList(productNames, "Id", "Name");
                ViewBag.CustomerNames = new SelectList(customerNames, "Id", "Name");
                return View(sale);
            }     
        }

        /// <summary>
        /// Removes sale.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>SaleTable view</returns>
        public IActionResult SaleDeleting(int id)
        {
            var sale = _mapper.Map<SaleViewModel>(_sale.FindSaleById(id));
            return View(sale);
        }

        /// <summary>
        /// Removes sale.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>SaleTable view</returns>
        [HttpPost]
        public IActionResult SaleDeleting(SaleViewModel sale)
        {
            _sale.DeleteSale(sale.Id);
            return RedirectToAction("SaleTable");
        }
    }
}
