using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CheckerZ_Server.Models;

namespace CheckerZ_Server.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _context;

        public IndexModel(DataContext context)
        {
            _context = context;
        }

        public IList<Player> Players { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Players = await _context.Player.ToListAsync();
        }

        //query 22
        public async Task OnPostShowPlayerData()
        {
            var players = await _context.Player.Where(p => p.Games.Any()).ToListAsync();
            Players = players.OrderBy(p => p.Name).ToList();
        }

        //query 25
        public async Task OnPostShowFirstPlayerByCountry()
        {

            var Firstplayers = await _context.Player.Where(p => p.Games.Any()).
                GroupBy(p => p.Country)
                .Select(group => group.OrderBy(p =>
                p.Games.Min(g => g.GameDate)).First()).ToListAsync();

            Players = Firstplayers;
        }
        
        //query 29
        public async Task OnPostSortByCountries()
        {
            var CountryPlayers = await _context.Player.OrderBy(p => p.Country).ToListAsync();
            Players = CountryPlayers;
        }
    }
}
