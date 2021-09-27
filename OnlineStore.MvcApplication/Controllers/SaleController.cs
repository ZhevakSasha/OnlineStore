using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;

namespace OnlineStore.MvcApplication.Controllers
{
    public class SaleController : Controller
    {
        private ISaleService _sale;

        public SaleController(ISaleService sale)
        {
            _sale = sale;
        }

        

        public IActionResult SaleTable()
        {
            var results = _sale.GetAllSales();
            return View(results);
        }

        public IActionResult SaleUpdating(int id)
        {
            Sale sale = _sale.FindSaleById(id);
            return View(sale);
        }

        [HttpPost]
        public IActionResult SaleUpdating(Sale sale)
        {
            _sale.UpdateSale(sale);
            return RedirectToAction("SaleTable");
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
                _sale.CreateSale(sale);
                return RedirectToAction("SaleTable");
            }
            else
                return View();
        }

        public IActionResult SaleDeleting(int id)
        {
            Sale sale = _sale.FindSaleById(id);
            _sale.DeleteSale(sale);
            return RedirectToAction("SaleTable");
        }
    }
}
