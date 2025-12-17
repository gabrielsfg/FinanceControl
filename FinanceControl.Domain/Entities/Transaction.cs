using FinanceControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int Value  { get; set; }
        public EnumTransactionType Type { get; set; }
        public int Category {  get; set; }
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public EnumPaymentType PaymentType { get; set; }
        public EnumPaymentRecurrence Reccurence { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
