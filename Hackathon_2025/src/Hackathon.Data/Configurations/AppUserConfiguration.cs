using Hackathon.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hackathon.Data.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.FullName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.FacePersonId)
               .IsRequired(false);

        builder.Property(x => x.ProfileImageUrl)
               .HasMaxLength(500)
               .IsRequired(false);

        builder.HasIndex(x => x.Email).IsUnique();
    }
}
