namespace NJUPT_AspNetCore_Exp2.Models;

public sealed class Party
{
    public int Id { get; init; }

    public required string Name { get; init; }
    public required string Location { get; init; }
    public required DateOnly Date { get; init; }

    public List<User> Users { get; init; } = [];

    public static readonly Party[] Seed =
    [
        .. Enumerable
            .Range(1, 3)
            .Select(index => new Party()
            {
                Id = index,
                Name = $"Party{index}",
                Location = $"L{index}",
                Date = new(2020, 12, 20 + index),
            }),
    ];
}
