using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username).HasMaxLength(25).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(256).IsRequired();
            builder.Property(u => u.FirstName).HasMaxLength(25).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(25).IsRequired();
            builder.Property(u => u.CreatedAt).IsRequired();
        }
    }
}
