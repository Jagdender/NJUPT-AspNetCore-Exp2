using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NJUPT_AspNetCore_Exp2.Models;

namespace NJUPT_AspNetCore_Exp2.Controllers;

public class UserController(DatabaseContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = await context.Users.ToListAsync();
        return View(model);
    }

    public new async Task<IActionResult> Redirect(string id)
    {
        var user = await context.Users.FirstOrDefaultAsync(m => m.Email == id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    public async Task<IActionResult> Details(string id)
    {
        var user = await context
            .Users.Include(user => user.Parties)
            .FirstOrDefaultAsync(m => m.Email == id);

        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }
}
