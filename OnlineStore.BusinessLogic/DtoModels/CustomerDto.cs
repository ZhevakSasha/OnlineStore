using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.BusinessLogic.DtoModels
{
    public class CustomerDto
    {
        /// <summary>
        /// Property  for storing customer id.
        /// </summary>
        public int Id { get; set; }

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
    }
}
