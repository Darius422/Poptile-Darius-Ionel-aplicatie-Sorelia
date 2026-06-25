using System.ComponentModel.DataAnnotations;

namespace BeautyHealthStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Numele este obligatoriu.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Numele trebuie să aibă între 3 și 100 de caractere.")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adresa de e-mail este obligatorie.")]
        [EmailAddress(ErrorMessage = "Adresa de e-mail nu este validă.")]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adresa de livrare este obligatorie.")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Adresa trebuie să aibă între 10 și 200 de caractere.")]
        public string Address { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "Plasată";

        public string UserId { get; set; } = string.Empty;

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}