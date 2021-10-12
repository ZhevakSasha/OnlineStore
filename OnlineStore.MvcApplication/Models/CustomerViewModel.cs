using System.ComponentModel.DataAnnotations;

namespace OnlineStore.MvcApplication.Models
{
    public class CustomerViewModel
    {

        /// <summary>
        /// Property  for storing customer id.
        /// </summary>
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        /// <summary>
        /// Property  for storing customer firstname.
        /// </summary>
        [Display(Name ="First Name")]
        [Required(ErrorMessage = "First name required")]
        [StringLength(30)]
        public string FirstName { get; set; }

        /// <summary>
        /// Property  for storing customer lastname.
        /// </summary>
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name required")]
        [StringLength(30)]
        public string LastName { get; set; }

        /// <summary>
        /// Property  for storing customer addres.
        /// </summary>
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address required")]
        [StringLength(30)]
        public string Address { get; set; }

        /// <summary>
        /// Property  for storing customer phone number.
        /// </summary>
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone number required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong number")]
        [StringLength(10)]
        public string PhoneNumber { get; set; }
    }
}
