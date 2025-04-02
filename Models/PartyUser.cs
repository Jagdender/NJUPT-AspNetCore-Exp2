namespace NJUPT_AspNetCore_Exp2.Models;

public sealed record class PartyUser
{
    public required int PartyId { get; init; }
    public required string UserId { get; init; }

    public static readonly PartyUser[] Seed =
    [
        .. Party.Seed.Select(
            (party, index) => new PartyUser() { PartyId = party.Id, UserId = User.Seed[index].Id }
        ),
        new() { PartyId = Party.Seed.First().Id, UserId = User.Seed.Last().Id },
    ];
}
