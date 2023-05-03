
using System.Security.AccessControl;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_StudentSystem.Seeding
{
    public class BillsPaymentContextSeeding
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var servicescope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = servicescope.ServiceProvider.GetService<BillsPaymentSystemContext>();
                if (!context.bankAccounts.Any())
                {
                    context.bankAccounts.AddRange(new BankAccount()
                    {
                        Balance = 0,
                        BankName="Privat",
                        SWIFTCode="5f6543636v"
                    });
                }
                context.SaveChanges();
                if (!context.creditCards.Any())
                {
                    context.creditCards.AddRange(new CreditCard()
                    {
                        Limit = 2,
                        MoneyOwed=22,
                        ExpirationDate=DateTime.Now,
                    });
                }
                if (!context.users.Any())
                {
                    context.users.AddRange(new User()
                    {
                        FirstName="firstname",
                        LastName="lastname",
                        Email="email",
                        Password="password",
                    });
                }
                context.SaveChanges();
                context.SaveChanges();
                if (!context.paymentMethods.Any())
                {
                    context.paymentMethods.AddRange(new PaymentMethod()
                    {
                        UserId=1,
                        BankAccountId=1
                    },new PaymentMethod()
                    {
                        UserId=1,
                        CreditCardId=1
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
