namespace API.DTOs.PaymentDetails
{
    public class PaymentDetailEmployeeDto
    {
        public Guid Guid { get; set; }
        public DateTime DateRequest { get; set; }
        public int Duration { get; set; }
        public int TotalPay { get; set; }
    }
}
