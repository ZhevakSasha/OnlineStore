using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.MvcApplication.Models;
using System.Collections.Generic;

namespace OnlineStore.MvcApplication.Controllers
{
    public class SaleController : Controller
    {
        /// <summary>
        /// Sale service.
        /// </summary>
        private ISaleService _sale;

        /// <summary>
        /// Mapper.
        /// </summary>
        private IMapper _mapper;


        /// <summary>
        /// SaleController constructor.
        /// </summary>
        /// <param name="sale">Sales service</param>
        public SaleController(ISaleService sale, IMapper mapper)
        {
            _mapper = mapper;
            _sale = sale;
        }

        public IActionResult SaleTable()
        {
            var results = _sale.GetAllSales();
            var sales = _mapper.Map<IEnumerable<SaleViewModel>>(results);
            return View(sales);
        }

        public IActionResult SaleUpdating(int id)
        {  
            var sale = _sale.FindSaleById(id);
            return View(_mapper.Map<SaleViewModel>(sale));
        }

        [HttpPost]
        public IActionResult SaleUpdating(Sale sale)
        {
            if (ModelState.IsValid)
            {
                _sale.UpdateSale(_mapper.Map<SaleDto>(sale));
                return RedirectToAction("SaleTable");
            }
            else
                return View();
        }

        public IActionResult SaleCreating()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaleCreating(Sale sale)
        {
            if (ModelState.IsValid)
            {
                _sale.CreateSale(_mapper.Map<SaleDto>(sale));
                return RedirectToAction("SaleTable");
            }
            else
                return View();
        }

        public IActionResult SaleDeleting(int id)
        {
            var sale = _sale.FindSaleById(id);
            _sale.DeleteSale(id);
            return RedirectToAction("SaleTable");
        }
    }
}
