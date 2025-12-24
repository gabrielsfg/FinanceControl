using FinanceControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Shared.Dtos;
using FinanceControl.Shared.Helpers;

namespace FinanceControl.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public int Value  { get; set; }
        public EnumTransactionType Type { get; set; }
        public int Category {  get; set; }
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public EnumPaymentType PaymentType { get; set; }
        public EnumPaymentRecurrence Reccurence { get; set; }
        
        public Transaction(CreateTransactionRequestDto requestDto)
        {
            Value = requestDto.Value;
            Type = Enum.Parse<EnumTransactionType>(requestDto.Type);
            Category = requestDto.Category;
            Description = requestDto.Description;
            TransactionDate = requestDto.Date;
            PaymentType = Enum.Parse<EnumPaymentType>(requestDto.PaymentType);
            Reccurence = Enum.Parse<EnumPaymentRecurrence>(requestDto.Reccurence);
        }
        
        protected  Transaction()
        {
        }
    }
    
    
}
