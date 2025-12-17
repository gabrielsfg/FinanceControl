namespace FinanceControl.Shared.Dtos
{
    public class CreateTransactionRequestDto
    {
        public string Type { get; set; }
        public int Value { get; set; }
        public int Category {  get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string PaymentType { get; set; }
        public string Reccurence { get; set; }
        public int? FromWallet { get; set; }
        public int? ToWallet { get; set;}
    }
}
