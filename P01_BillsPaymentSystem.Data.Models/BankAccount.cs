using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }
        public double Balance { get; set; }
        public string BankName { get; set; }
        public string SWIFTCode { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; } = null!;
    }
}
