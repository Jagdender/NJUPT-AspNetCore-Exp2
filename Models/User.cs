namespace NJUPT_AspNetCore_Exp2.Models;

public sealed class User
{
    public required string Name { get; init; }

    public required string Email { get; init; }

    public required string PhoneNumber { get; init; }

    public List<Party> Parties { get; init; } = [];

    public static readonly User[] Seed =
    [
        new()
        {
            Name = "Alice Jones",
            Email = "alice@example.com",
            PhoneNumber = "555-123-5678",
        },
        new()
        {
            Name = "Peter Davies",
            Email = "peter@example.com",
            PhoneNumber = "555-456-7890",
        },
        new()
        {
            Name = "Dora Francis",
            Email = "dora@example.com",
            PhoneNumber = "555-456-1234",
        },
        new()
        {
            Name = "Bob Smith",
            Email = "bob@example.com",
            PhoneNumber = "555-123-1234",
        },
    ];

    public string Id => Email;
}
