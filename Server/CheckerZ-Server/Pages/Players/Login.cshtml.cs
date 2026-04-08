using CheckerZ_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CheckerZ_Server.Pages.Players
{
    public class LoginModel : PageModel
    {
        private readonly DataContext _context;
        public LoginModel(DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<Player> Players { get; set; }


        [BindProperty]
        // current rows for register players
        public int NumPlayers { get; set; } = 1;
        // range for number of players chosen
        public List<int> PlayerAmountRange = Enumerable.Range(1, 10).ToList();

        public IActionResult OnGet()
        {

            Players = new List<Player> { new Player() };

            return Page();
        }

        public IActionResult OnPostRowAmountAsync(int NumPlayers)
        {
            this.NumPlayers = NumPlayers;

            // if players list is empty then initialize players list

            if (Players == null) Players = new List<Player>();

            //when extending player number add more rows
            while (Players.Count < NumPlayers)
            {
                Players.Add(new Player());
            }
            // when reducing player number remove rows 

            while (Players.Count > NumPlayers)
            {
                Players.RemoveAt(Players.Count - 1);
            }

            //overrides and updates the page

            ModelState.Clear();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Random rnd = new Random();
            int gameSession;
            if (Players == null || Players.Count == 0) return Page();

            for (int i = 0; i < NumPlayers; i++)
            {
                ModelState.Remove($"Players[{i}].Country");
                ModelState.Remove($"Players[{i}].Id");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }


            for (int i = 0; i < NumPlayers; i++)
            {
                var dbPlayer = await _context.Player.FirstOrDefaultAsync(p => p.Name == Players[i].Name &&
                p.PhoneNumber == Players[i].PhoneNumber);
                if (dbPlayer == null)
                {
                    ModelState.AddModelError($"Players[{i}].Name", "player doesn't exist");
                    return Page();
                }
                Players[i] = dbPlayer;
            }

            gameSession = rnd.Next(10000, 99999); // מספר בן 5 ספרות

            foreach (var p in Players)
            {
                p.SessionID = gameSession;
                _context.Player.Update(p);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("/GameCode", new { Code = gameSession });
        }


    }
}
