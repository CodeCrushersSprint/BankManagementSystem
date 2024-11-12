namespace BMSWebApi.DTO
{
    public class LoanDTO
    {
        public int LoanId { get; set; }
        public int CustomerId { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public string Status { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
