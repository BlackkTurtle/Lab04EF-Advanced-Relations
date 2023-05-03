namespace BillsPaymentSystem.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Data.Models;

    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> entity)
        {
            entity.HasKey(e => e.BankAccountId).HasName("PK__BankAccounts");

            entity.Property(e => e.Balance);
            entity.Property(e => e.BankName).HasMaxLength(50);
            entity.Property(e => e.SWIFTCode).HasMaxLength(20);
        }
    }
}
