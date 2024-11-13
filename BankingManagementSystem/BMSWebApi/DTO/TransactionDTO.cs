using System.ComponentModel.DataAnnotations;

namespace BMSWebApi.DTO
{
    public class TransactionDTO
    {
        [Required]
        public int TransactionId { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string TransactionType { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }
    }
}
