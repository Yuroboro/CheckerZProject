using CheckerZ_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CheckerZ_Server.Pages.Queries
{
    public class PlayerGameAmountModel : PageModel
    {
        private readonly DataContext _context;


        public PlayerGameAmountModel(DataContext context)
        {
            _context = context;
        }

        public IList<GameCounter> GameAmounts { get; set; } = default;
        public async Task OnGetAsync()
        {
            GameAmounts = await _context.Player
                .Select(p => new GameCounter
                {
                    Name = p.Name,
                    Amount = p.Games.Count()
                }).OrderByDescending(p=>p.Amount).ToListAsync();
        }
    }
}
