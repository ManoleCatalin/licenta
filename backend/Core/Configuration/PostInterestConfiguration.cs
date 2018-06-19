using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configuration
{
    public class PostInterestConfiguration : IEntityTypeConfiguration<PostInterest>
    {
        public void Configure(EntityTypeBuilder<PostInterest> builder)
        {
            builder
           .HasKey(t => new { t.PostId, t.InterestId });

            builder
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostInterests)
                .HasForeignKey(pt => pt.PostId);

            builder
                .HasOne(pt => pt.Interest)
                .WithMany(t => t.PostInterests)
                .HasForeignKey(pt => pt.InterestId);
        }
    }
}
