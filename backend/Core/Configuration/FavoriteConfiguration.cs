using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configuration
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.CreatedAt).IsRequired();
            builder.Property(u => u.User).IsRequired();
            builder.Property(u => u.Post).IsRequired();
        }
    }
}
