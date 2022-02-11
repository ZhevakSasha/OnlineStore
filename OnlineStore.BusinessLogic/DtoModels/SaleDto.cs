using System.Collections.Generic;

namespace OnlineStore.BusinessLogic.DtoModels
{
    /// <summary>
    /// SaleDto model.
    /// </summary>
    public class SaleDto
    {
        /// <summary>
        /// Property for storing sale id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Property for storing customer id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Property  for storing product name.
        /// </summary>
        public IList<SelectDto> Product { get; set; }

        /// <summary>
        /// Property for storing customer name.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Property  for storing date of sale.
        /// </summary>
        public string DateOfSale { get; set; }

        /// <summary>
        /// Property  for storing amount of sales.
        /// </summary>
        public int Amount { get; set; }

    }
}
