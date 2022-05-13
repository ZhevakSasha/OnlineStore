using System.Collections.Generic;

namespace OnlineStore.BusinessLogic.DtoModels
{
    /// <summary>
    /// CustomerDto model.
    /// </summary>
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
        /// Property  for storing customer address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Property  for storing customer phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        public List<SaleDto> Sales { get; set; } = new List<SaleDto>();
    }
}
