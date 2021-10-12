namespace OnlineStore.BusinessLogic.DtoModels
{
    /// <summary>
    /// ProductDto model.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Property  for storing product id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Property  for storing product name.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Property for storing price of product.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Property for storing product unit of measurement.
        /// </summary>
        public string UnitOfMeasurement { get; set; }
    }
}
