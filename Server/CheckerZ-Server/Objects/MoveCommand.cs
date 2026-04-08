using CheckerZ_Server.Enums;

namespace CheckerZ_Server.Objects
{
    // Object that represents the desired action of the computer per turn(includes index for start postion and target postion)
    public class MoveCommand
    {
        public GameAction Action { get; set; }

        // The server points to the exact piece using Grid Math! No pixels!

        public int StartRow { get; set; }
        public int StartCol { get; set; }

        // Where it lands
        public int TargetRow { get; set; }
        public int TargetCol { get; set; }

        public MoveCommand(BoardLocation boardLocation,int targetRow,int targetCol, GameAction action)
        {
            StartRow = boardLocation.Row;
            StartCol = boardLocation.Col;
            TargetRow = targetRow;
            TargetCol = targetCol;
            Action = action;
            
        }
    }
}
