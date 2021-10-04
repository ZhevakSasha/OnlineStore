﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.BusinessLogic.DtoModels
{
    public class SaleDto
    {
        public int SaleId { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        /// <summary>
        /// Property  for storing product name.
        /// </summary>
        public string ProductName { get; set; }

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
