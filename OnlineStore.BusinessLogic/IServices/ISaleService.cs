using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.DataAccess.DataModel;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic.IServices
{

    /// <summary>
    /// SaleService interface.
    /// </summary>
    public interface ISaleService
    {
        IEnumerable<SaleDto> GetAllSales();
        void CreateSale(SaleDto sale);
        void UpdateSale(SaleDto sale);
        SaleDto FindSaleById(int id);
        void DeleteSale(int id);
    }
}
