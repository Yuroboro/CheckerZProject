using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CheckerZ_Server.Models;

namespace CheckerZ_Server.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _context;
        public List<string> Names { get; set; }

        public IndexModel(DataContext context)
        {
            _context = context;
            Names = _context.Player
                    .Select(p => p.Name.ToLower()) // Convert everything to lowercase so "Avi" becomes "avi" and "avI" becomes "avi"
                    .Distinct()                    // Remove the duplicates (now guaranteed to catch case differences)
                    .OrderBy(n => n)               // Sort the unique names alphabetically (A-Z)
                    .ToList();
        }

        public IList<Game> Games { get; set; } = default!;
        public async Task OnGetAsync()
        {
            await ShowAllGames();
        }

        //query 26

        public async Task OnPostGamesByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                await ShowAllGames();
                return;
            }
            var gamesByName = await _context.Game
            .Include(g => g.Player)
            .Where(g => g.Player.Name.ToLower() == name)
             .ToListAsync();
            Games = gamesByName;
        }

        public async Task ShowAllGames()
        {
            Games = await _context.Game
                    .Include(g => g.Player).Where(g => g.GameDate != null).ToListAsync();
        }
    }
}
