using AccountingNotebook.Data;

namespace AccountingNotebook.Services.Models
{
    public class CreateTransaction
    {
        public TransactionType Type { get; set; }
        public double Amount { get; set; }
    }
}
