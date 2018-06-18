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

            builder
            .HasOne(u => u.User)
            .WithMany(u => u.Favorites)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            builder
           .HasOne(u => u.Post)
           .WithMany(u => u.Favorites)
           .HasForeignKey(u => u.PostId);
        }
    }
}
