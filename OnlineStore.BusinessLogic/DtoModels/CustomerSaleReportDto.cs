using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.DtoModels
{
    public class CustomerSaleReportDto
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

        public List<SaleWithProductDto> Sales { get; set; } = new List<SaleWithProductDto>();
    }
}
