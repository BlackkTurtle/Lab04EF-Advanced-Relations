using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BillsPaymentSystem.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data
{
    public class BillsPaymentSystemContext:DbContext
    {
        public BillsPaymentSystemContext(DbContextOptions<BillsPaymentSystemContext> options) : base(options)
        {

        }
        public DbSet<User> users => Set<User>();
        public DbSet<BankAccount> bankAccounts => Set<BankAccount>();
        public DbSet<CreditCard> creditCards => Set<CreditCard>();
        public DbSet<PaymentMethod> paymentMethods => Set<PaymentMethod>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new CreditCardConfiguration());

            modelBuilder.ApplyConfiguration(new BankAccountConfiguration());

            modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
        }
    }
}
