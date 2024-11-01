using LibraryWebApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryWebApp.Infrastructure.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            builder
                .HasKey(token => token.Id);

            builder
                .Property(token => token.Token)
                .IsRequired();

            builder
                .Property(token => token.Expires)
                .IsRequired();

            builder
                .Property(token => token.IsRevoked)
                .HasDefaultValue(false);

            builder
                .Property(token => token.Created)
                .IsRequired();

            builder
                .HasOne(token => token.User)
                .WithMany(user => user.RefreshTokens).HasForeignKey(user => user.UserLogin);
        }
    }
}
