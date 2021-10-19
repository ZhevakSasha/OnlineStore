using OnlineStore.BusinessLogic.DtoModels;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic.IServices
{
    /// <summary>
    /// SaleService interface.
    /// </summary>
    public interface ISaleService
    {
        /// <summary>
        /// GetAllSales method.
        /// </summary>
        /// <returns>Returns all sales from table</returns>
        IEnumerable<SaleDto> GetAllSales();

        /// <summary>
        /// CreateSale method.
        /// </summary>
        /// <param name="sale">Takes SaleDto object</param>
        void CreateSale(SaleDto sale);

        /// <summary>
        /// Update sale method.
        /// </summary>
        /// <param name="sale">Takes SaleDto object</param>
        void UpdateSale(SaleDto sale);

        /// <summary>
        /// FindSaleById method.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>SaleDto object</returns>
        SaleDto FindSaleById(int id);

        /// <summary>
        /// DeleteSale mrthod deletes sale by id.
        /// </summary>
        /// <param name="id">id</param>
        void DeleteSale(int id);
    }
}
