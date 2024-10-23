using LibraryWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Infrastructure.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AuthorEntity> builder)
        {
            builder
                .HasKey(author => author.Id);

            builder
                .Property(author => author.Name)
                .IsRequired();

            builder
                .Property(author => author.Surname)
                .IsRequired();

            builder
                .Property(author => author.BirthDate)
                .IsRequired();

            builder
                .Property(author => author.Country)
                .IsRequired();

            builder
                .HasMany(author => author.Books)
                .WithOne(book => book.Author);
        }
    }
}
