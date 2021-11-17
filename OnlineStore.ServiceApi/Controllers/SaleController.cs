using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ServiceApi.Controllers
{
    [Route("serviceApi/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getSales")]
        public ActionResult<IEnumerable<SaleDto>> GetAllSales()
        {
            var sales = _saleService.GetAllSales();

            if (sales == null)
            {
                return NotFound();
            }

            return Ok(sales);
        }


        [HttpGet]
        [Route("getSale/{id}")]
        public ActionResult<SaleDto> GetSaleById(int id)
        {
            var sale = _saleService.FindSaleById(id);

            if (sale == null)
            {
                return NotFound();
            }


            return Ok(sale);
        }

        [HttpPost]
        [Route("createSale")]
        public ActionResult<SaleDto> CreateSale(SaleDto sale)
        {
            _saleService.CreateSale(sale);
            return CreatedAtAction(nameof(GetSaleById), new { Id = sale.Id }, sale);
        }

        [HttpPut]
        [Route("updateSale")]
        public ActionResult UpdateSale(SaleDto saleService)
        {
            var sale = _saleService.FindSaleById(saleService.Id);

            if (sale == null)
            {
                return NotFound();
            }

            sale.ProductId = saleService.ProductId;
            sale.CustomerId = saleService.CustomerId;
            sale.Amount = saleService.Amount;
            sale.DateOfSale = saleService.DateOfSale;

            _saleService.UpdateSale(sale);

            return NoContent();
        }

        [HttpDelete]
        [Route("deleteSale/{id}")]
        public ActionResult DeleteSale(int id)
        {
            var sale = _saleService.FindSaleById(id);

            if (sale == null)
            {
                return NotFound();
            }

            _saleService.DeleteSale(id);

            return NoContent();
        }
    }
}
