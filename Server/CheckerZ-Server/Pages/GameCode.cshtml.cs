using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CheckerZ_Server.Pages
{
    public class GameCodeModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Code{ get; set; }
        public void OnGet()
        {
        }
    }
}
