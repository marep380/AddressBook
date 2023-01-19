using System.ComponentModel.DataAnnotations;

namespace AddressBook_API.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }
    }
}
