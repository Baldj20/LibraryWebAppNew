using LibraryWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Infrastructure.Configurations
{
    public class UserBookConfiguration : IEntityTypeConfiguration<UserBookEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserBookEntity> builder)
        {
            builder
                .HasKey(userBook => userBook.ISBN);

            builder
                .Property(userBook => userBook.ReceiptDate)
                .IsRequired();

            builder
                .Property(userBook => userBook.ReturnDate)
                .IsRequired();

            builder
                .HasOne(userBook => userBook.User)
                .WithMany(user => user.TakenBooks);

            builder
                .HasOne<BookEntity>()
                .WithOne().HasForeignKey<UserBookEntity>(userBook => userBook.ISBN);
        }
    }
}

