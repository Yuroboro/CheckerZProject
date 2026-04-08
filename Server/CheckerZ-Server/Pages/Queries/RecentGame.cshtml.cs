using CheckerZ_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CheckerZ_Server.Pages.Queries
{
    public class RecentGameModel : PageModel
    {
        private readonly DataContext _context;


        public RecentGameModel(DataContext context)
        {
            _context = context;
        }

        public IList<RecentGame> RecentGames { get; set; } = default;
        public async Task OnGetAsync()
        { 
            RecentGames = await _context.Player
                .Select(p => new RecentGame
                {
                    Name = p.Name,
                    RecentDate = p.Games.Max(g => g.GameDate)
                }).ToListAsync();
        }
    }
}
