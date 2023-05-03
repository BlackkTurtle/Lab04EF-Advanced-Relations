namespace BillsPaymentSystem.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Data.Models;

    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentMethods");

            entity.HasOne(d => d.BankAccount).WithOne(p => p.PaymentMethod)
              .HasForeignKey<PaymentMethod>(d => d.BankAccountId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_PaymentMethod_BankAccounts");
            entity.Property(e => e.BankAccountId).HasAnnotation("Unique", true);
            entity.HasCheckConstraint("CK_PaymentMethod_BankAccountId", "(BankAccountId IS NOT NULL AND CreditCardId IS NULL) OR (BankAccountId IS NULL AND CreditCardId IS NOT NULL)");
            entity.HasOne(d => d.CreditCard).WithOne(p => p.PaymentMethod)
              .HasForeignKey<PaymentMethod>(d => d.CreditCardId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_PaymentMethod_CreditCards");
            entity.Property(e => e.CreditCardId).HasAnnotation("Unique", true);
            entity.HasOne(d => d.User).WithMany(p => p.PaymentMethods)
              .HasForeignKey(d => d.UserId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_PaymentMethod_Users");
        }
    }
}
