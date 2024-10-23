using LibraryWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Infrastructure.Configurations
{
    public  class BookConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BookEntity> builder)
        {
            builder
                .HasKey(book => book.ISBN);

            builder
                .Property(book => book.Title)
                .IsRequired();

            builder
                .Property(book => book.Genre)
                .IsRequired();

            builder
                .Property(book => book.Description);

            
            builder
                .HasOne(book => book.Author)
                .WithMany(author => author.Books);

            builder
                .HasOne<UserBookEntity>()
                .WithOne();
        }
    }
}
