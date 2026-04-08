using CheckerZ_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CheckerZ_Server.Pages.Queries
{
    public class GroupByGameAmountModel : PageModel
    {
        private readonly DataContext _context;

        public GroupByGameAmountModel(DataContext context)
        {
            _context = context;
        }

        public IList<Player> gameAmountGroups { get; set; }
        public async Task OnGetAsync()
        {
            gameAmountGroups = await _context.Player.Include(p => p.Games).OrderByDescending(p => p.Games.Count()).ToListAsync();
        }
    }
}
