using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configuration
{
    public class UserInterestConfiguration : IEntityTypeConfiguration<UserInterest>
    {
        public void Configure(EntityTypeBuilder<UserInterest> builder)
        {
            builder
           .HasKey(t => new { t.UserId, t.InterestId });

            builder
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserInterest)
                .HasForeignKey(pt => pt.UserId);
        }
    }
}
