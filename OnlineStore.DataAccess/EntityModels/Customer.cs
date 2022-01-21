﻿using System.Collections.Generic;

namespace OnlineStore.DataAccess.DataModel
{
    /// <summary>
    /// Data model for a Customers table.
    /// </summary>
    public class Customer : ModelBase
    {
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
        public string Address { get; set; }

        /// <summary>
        /// Property  for storing customer phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Property for getting List of sales.
        /// </summary>
        public List<Sale> Sales { get; set; } = new List<Sale>();
    }
}