namespace BillsPaymentSystem.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Data.Models;

    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> entity)
        {
            entity.HasKey(e => e.CreditCardId).HasName("PK__CreditCards");

            entity.Property(e => e.Limit);
            entity.Property(e => e.MoneyOwed);
            entity.Property(e => e.ExpirationDate);
        }
    }
}
