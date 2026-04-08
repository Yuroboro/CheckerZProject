using CheckerZ_Server.Models;
using CheckerZ_Server.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

// An object to handle client server communication for the server
namespace CheckerZ_Server.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly DataContext _context;
        public ServerController(DataContext context)
        {
            _context = context;
        }

        // Send players list to client
        [HttpGet("LoginSession/{code}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayersBySession(int code)
        {

            var sessionPlayers = await _context.Player
                .Where(p => p.SessionID == code)
                .ToListAsync();

            if (sessionPlayers == null || sessionPlayers.Count == 0)
            {
                return NotFound("Session Not Found");
            }


            foreach (var p in sessionPlayers)
            {
                p.SessionID = 0;
            }

            await _context.SaveChangesAsync();


            return Ok(sessionPlayers);
        }

        //Recieve games from client and storing it in server
        [HttpPost("SaveGame")]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            _context.Game.Add(game);
            await _context.SaveChangesAsync();
            await _context.Entry(game).Reference(g => g.Player).LoadAsync();
            return Ok(game);
        }

        //Sending the game list that contains player name id and game date.
        //Used to sync changes in the games database in both client and server
        [HttpGet("SyncGames")]
        public async Task<ActionResult> GetGames()
        {
            var games = await _context.Game.Select(g => new
            {
                PlayerID = g.PlayerId,
                GameDate = g.GameDate,
                PlayerName = g.Player!.Name
            }).ToListAsync();
            return Ok(games);
        }
        // Gets current state of the board and sends choosen move by the server
        [HttpPost("ComputerMove")]
        public ActionResult PostComputerMove([FromBody] GameStateRequest gameStateRequest)
        {
            List<BoardLocation> playerLocations = gameStateRequest.PlayerLocations;
            List<BoardLocation> computerLocations = gameStateRequest.ComputerLocations;
            MoveCommand moveCommand = MoveSelector.SelectMove(playerLocations, computerLocations);
            return Ok(moveCommand);
        }
    }
}

