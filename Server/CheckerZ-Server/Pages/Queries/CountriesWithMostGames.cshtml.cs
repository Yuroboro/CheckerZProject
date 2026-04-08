using CheckerZ_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CheckerZ_Server.Pages.Queries
{
    public class CountriesWithMostGamesModel : PageModel
    {
        private readonly DataContext _context;
        public CountriesWithMostGamesModel(DataContext context)
        {
            _context = context;
        }

        public IList<GameCounter> TopTwoCountries { get; set; } = default;
        public async Task OnGetAsync()
        {
            TopTwoCountries = await _context.Player
                .Where(p => !string.IsNullOrEmpty(p.Country))
                .GroupBy(p => p.Country).Select(group  => new GameCounter
                {
                    Name = group.Key,
                    Amount = group.SelectMany(p => p.Games).Count()
                }).OrderByDescending(group => group.Amount).Take(2).ToListAsync();
        }
    }
}
