using CheckerZ_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckerZ_Server.Pages.Players
{
    public class RegisterModel : PageModel
    {
        private readonly DataContext _context;

        public RegisterModel(DataContext context)
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

        public List<string> countries = ["Israel", "France", "USA","UK","Japan","China","Morroco","Brazil"];

        //prints the register page to screen
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


        //For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            for(int i = 0;i<NumPlayers; i++)
            {
                var exists = await _context.Player.AnyAsync(p => p.Id == Players[i].Id);
                if (exists)
                {
                    ModelState.AddModelError($"Players[{i}].Id", $"Sorry,ID already exists");
                    return Page();

                }
                _context.Player.Add(Players[i]);
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("/UserLogin");
        }
    }
}
