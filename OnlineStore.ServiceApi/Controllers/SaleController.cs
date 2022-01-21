using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.EntityModels;
using OnlineStore.DataAccess.PagedList;
using System.Collections.Generic;

namespace OnlineStore.ServiceApi.Controllers
{
    /// <summary>
    /// ServiceApi SaleController.
    /// </summary>
    [Route("serviceApi/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        /// <summary>
        /// Sale service field.
        /// </summary>
        private ISaleService _saleService;

        /// <summary>
        /// SaleController constructor.
        /// </summary>
        /// <param name="saleService">Sale service</param>
        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// HttpGet endpoint with all sales.
        /// </summary>
        /// <returns>List with all sales</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("getSales")]
        public ActionResult<PagedList<SaleDto>> GetAllSales([FromQuery] PageParameters pageParameters)
        {
            var sales = _saleService.GetAllSales(pageParameters);

            if (sales == null)
            {
                return NotFound();
            }

            var metadata = new
            {
                sales.TotalCount,
                sales.PageSize,
                sales.CurrentPage,
                sales.TotalPages,
                sales.HasNext,
                sales.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(sales);
        }

        /// <summary>
        /// HttpGet endpoint with sale by id.
        /// </summary>
        /// <param name="id">Sale id</param>
        /// <returns>Sale</returns>
        [HttpGet]
        [Route("getSale/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<SaleDto> GetSaleById(int id)
        {
            var sale = _saleService.FindSaleById(id);

            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }

        /// <summary>
        /// HttpPost endpoint. Creates sale.
        /// </summary>
        /// <param name="sale">Sale</param>
        [HttpPost]
        [Route("createSale")]
        [Authorize(Roles = "Admin")]
        public ActionResult<SaleDto> CreateSale(SaleDto sale)
        {
            _saleService.CreateSale(sale);
            return CreatedAtAction(nameof(GetSaleById), new { Id = sale.Id }, sale);
        }

        /// <summary>
        /// HttpPut endpoint. Updates sale.
        /// </summary>
        /// <param name="saleService">sale</param>
        [HttpPut]
        [Route("updateSale")]
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// HttpDelete endpoint. Deletes sale.
        /// </summary>
        /// <param name="id">Sale id</param>
        [HttpDelete]
        [Route("deleteSale/{id}")]
        [Authorize(Roles = "Admin")]
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
