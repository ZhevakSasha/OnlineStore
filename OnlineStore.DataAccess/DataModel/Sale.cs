namespace OnlineStore.DataAccess.DataModel
{
    /// <summary>
    /// Data model for Sales a table. 
    /// </summary>
    public class Sale : ModelBase
    {
        /// <summary>
        /// Property  for storing product id.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Property  for storing customer id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Property  for storing date of sale.
        /// </summary>
        public string DateOfSale { get; set; }

        /// <summary>
        /// Property  for storing amount of sales.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Property  for getting product.
        /// </summary>
        public Product Products { get; set; }

        /// <summary>
        /// Property  for getting customer.
        /// </summary>
        public Customer Customers { get; set; }

    }
}
