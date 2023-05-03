using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {
            paymentType = MakepaymentType();
        }
        public int Id { get; set; }
        public PaymentType paymentType;
        public int UserId { get; set; }
        public int? BankAccountId { get; set; }
        public int? CreditCardId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual CreditCard? CreditCard { get; set; }
        public virtual BankAccount? BankAccount { get; set; }
        private PaymentType MakepaymentType()
        {
            if(BankAccount == null)
            {
                return PaymentType.CreditCard;
            }
            else
            {
                return PaymentType.BankAccount;
            }
        }
    }
}
