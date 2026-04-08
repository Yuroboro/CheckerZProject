using CheckerZ_Server.Objects;

namespace CheckerZ_Server.API
{
    // Object to represent the board state in client side
    public class GameStateRequest
    {
        public List<BoardLocation>? PlayerLocations { get; set; }
        public List<BoardLocation>? ComputerLocations { get; set; }
    }
}
