using OnlineStore.DataAccess.DataModel;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic.IServices
{

    /// <summary>
    /// SaleService interface.
    /// </summary>
    public interface ISaleService
    {
        IEnumerable<Sale> GetAllSales();
        void CreateSale(Sale sale);
        void UpdateSale(Sale sale);
        Sale FindSaleById(int id);
        void DeleteSale(Sale sale);
    }
}
