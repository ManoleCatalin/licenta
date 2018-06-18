using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.SourceUrl).HasMaxLength(2048).IsRequired();
            builder.Property(p => p.Title).HasMaxLength(256).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired();

            builder
            .HasOne(pt => pt.User)
            .WithMany(t => t.Posts)
            .HasForeignKey(pt => pt.UserId)
            .IsRequired();

            builder.HasMany(x => x.Favorites)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId);
        }
    }
}
