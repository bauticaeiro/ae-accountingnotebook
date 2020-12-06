using AccountingNotebook.Data;
using System;

namespace AccountingNotebook.Services.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public TransactionType Type { get; set; }
    }
}
