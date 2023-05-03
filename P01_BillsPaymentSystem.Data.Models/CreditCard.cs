using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class CreditCard
    {
        public CreditCard()
        {
            this.LimitLeft = CalculateLimit();
        }
        public int CreditCardId { get; set; }
        public double Limit { get; set; }
        public double MoneyOwed { get; set; }
        public double LimitLeft;
        public DateTime ExpirationDate { get; set; }
        private double CalculateLimit()
        {
            return MoneyOwed/Limit;
        }
        public virtual PaymentMethod? PaymentMethod { get; set; }
    }
}
