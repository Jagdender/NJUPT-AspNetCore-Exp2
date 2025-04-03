using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NJUPT_AspNetCore_Exp2.Models;

namespace NJUPT_AspNetCore_Exp2.Controllers;

public sealed class PartyController(DatabaseContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = await context.Parties.ToArrayAsync();
        return View(model);
    }

    public async Task<IActionResult> Register(int id)
    {
        var party = await context.Parties.FirstOrDefaultAsync(p => p.Id == id);
        ViewData["Party"] = party;

        return party is null ? NotFound() : View();
    }

    [HttpPost]
    public async Task<IActionResult> Submit([FromForm] User user, int id)
    {
        var party = await context.Parties.FirstOrDefaultAsync(p => p.Id == id);
        if (party is null)
            return NotFound();

        user = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email) ?? user;

        party.Users.Add(user);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Redirect), nameof(User), new { user.Id });
    }

    public async Task<IActionResult> Details(int id)
    {
        var model = await context
            .Parties.Include(p => p.Users)
            .FirstOrDefaultAsync(p => p.Id == id);

        return model is null ? NotFound() : View(model);
    }
}
