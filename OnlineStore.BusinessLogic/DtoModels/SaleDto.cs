using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.BusinessLogic.DtoModels
{
    class SaleDto
    {
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

        /// <summary>
        /// Property  for storing customer firstname.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Property  for storing customer lastname.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Property  for storing customer addres.
        /// </summary>
        public string Addres { get; set; }

        /// <summary>
        /// Property  for storing customer phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

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
