using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NJUPT_AspNetCore_Exp2.Models;

public sealed class EntityConfiguration
    : IEntityTypeConfiguration<User>,
        IEntityTypeConfiguration<Party>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Email);
        builder.HasData(User.Seed);
        builder.Ignore(user => user.Id);

        builder
            .HasMany(user => user.Parties)
            .WithMany(party => party.Users)
            .UsingEntity<PartyUser>(e =>
            {
                e.HasKey(p => new { p.PartyId, p.UserId });
                e.HasData(PartyUser.Seed);
            });
    }

    public void Configure(EntityTypeBuilder<Party> builder)
    {
        builder.HasKey(party => party.Id);
        builder.HasData(Party.Seed);
    }
}
