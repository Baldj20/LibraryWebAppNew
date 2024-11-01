using LibraryWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserEntity> builder)
        {
            builder
                .HasKey(user => user.Login);

            builder
                .Property(user => user.Password)
                .IsRequired();

            builder
                .HasMany(user => user.TakenBooks)
                .WithOne(userBook => userBook.User);

            builder
                .HasMany(user => user.RefreshTokens)
                .WithOne(token => token.User);
        }
    }
}

