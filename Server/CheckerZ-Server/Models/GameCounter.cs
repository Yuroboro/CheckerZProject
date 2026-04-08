using System.ComponentModel.DataAnnotations;

namespace CheckerZ_Server.Models
{
    // Object for query 27 (Show player names with game amount)
    // Used in query 30 (Top 2 Contries with most countries)
    public class GameCounter
    {
        public string Name { get; set; }
        [Display(Name = "Game Amount")]
        public int Amount {  get; set; }
    }
}
