using CheckerZ_Server.Enums;
using CheckerZ_Server.Models;
using Microsoft.VisualBasic;

namespace CheckerZ_Server.Objects
{
    // Object to allow the server to select a desired computer move to be sent to the client.
    public static class MoveSelector
    {
        private const int ROWS = 8;
        private const int COLS = 4;
        private static Random rng = new Random();

        public static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        // Function to select a possible move 
        public static MoveCommand SelectMove(List<BoardLocation> playerLocations, List<BoardLocation> computerLocations)
        {
            if (computerLocations.Count > 1)
                Shuffle(computerLocations);
            for (int i = 0; i < computerLocations.Count; i++)
            {
                int targetRow;
                int targetCol;
                GameAction action;
                if (TryCaptureDownLeft(out action, out targetRow, out targetCol, i, playerLocations, computerLocations)
                    || TryCaptureDownRight(out action, out targetRow, out targetCol, i, playerLocations, computerLocations))
                {
                    return new MoveCommand(computerLocations[i], targetRow, targetCol, action);
                }
            }
            for (int i = 0; i < computerLocations.Count; i++)
            {
                int targetRow;
                int targetCol;
                GameAction action;
                if (TryMoveDownLeft(out action, out targetRow, out targetCol, i, playerLocations, computerLocations)
                    || TryMoveDownRight(out action, out targetRow, out targetCol, i, playerLocations, computerLocations)
                    || TryMoveUpLeft(out action, out targetRow, out targetCol, i, playerLocations, computerLocations)
                    || TryMoveUpRight(out action, out targetRow, out targetCol, i, playerLocations, computerLocations))
                {
                    return new MoveCommand(computerLocations[i], targetRow, targetCol, action);
                }

            }
            return null;
        }
        //Logic for capture right
        public static bool TryCaptureDownRight(out GameAction action, out int targetRow, out int targetCol, int pieceIndex, List<BoardLocation> playerLocations, List<BoardLocation> computerLocations)
        {
            targetRow = 0;
            targetCol = 0;
            action = GameAction.CaptureRight;
            if (computerLocations[pieceIndex].Row + 2 < ROWS && computerLocations[pieceIndex].Col + 2 < COLS)
            {
                int startRow = computerLocations[pieceIndex].Row;
                int startCol = computerLocations[pieceIndex].Col;
                BoardLocation midSquare = null!;
                BoardLocation targetSquare = null!;
                // checks if 
                for (int i = 0; i < playerLocations.Count; i++)
                {
                    // check if there is a midpiece of the player
                    if (playerLocations[i].Row == startRow + 1 && playerLocations[i].Col == startCol + 1)
                    {
                        midSquare = playerLocations[i];
                    }
                    // check if theres a player piece in target place 
                    if (playerLocations[i].Row == startRow + 2 && playerLocations[i].Col == startCol + 2)
                    {
                        targetSquare = playerLocations[i];
                    }
                }
                // check if there is a computer piece in target place
                for (int i = 0; i < computerLocations.Count; i++)
                {
                    if (computerLocations[i].Row == startRow + 2 && computerLocations[i].Col == startCol + 2)
                    {
                        targetSquare = computerLocations[i];
                    }
                }

                if (midSquare != null && targetSquare == null)
                {
                    targetRow = startRow + 2;
                    targetCol = startCol + 2;
                    return true;
                }
            }
            return false;
        }
        //Logic for capture left
        public static bool TryCaptureDownLeft(out GameAction action, out int targetRow, out int targetCol, int pieceIndex, List<BoardLocation> playerLocations, List<BoardLocation> computerLocations)
        {
            targetRow = 0;
            targetCol = 0;
            action = GameAction.CaptureLeft;
            if (computerLocations[pieceIndex].Row + 2 < ROWS && computerLocations[pieceIndex].Col - 2 >= 0)
            {
                int startRow = computerLocations[pieceIndex].Row;
                int startCol = computerLocations[pieceIndex].Col;
                BoardLocation midSquare = null!;
                BoardLocation targetSquare = null!;
                for (int i = 0; i < playerLocations.Count; i++)
                {
                    // check if there is a midpiece of the player
                    if (playerLocations[i].Row == startRow + 1 && playerLocations[i].Col == startCol - 1)
                    {
                        midSquare = playerLocations[i];
                    }
                    // check if theres a player piece in target place
                    if (playerLocations[i].Row == startRow + 2 && playerLocations[i].Col == startCol - 2)
                    {
                        targetSquare = playerLocations[i];
                    }
                }
                // check if there is a computer piece in target place

                for (int i = 0; i < computerLocations.Count; i++)
                {
                    if (computerLocations[i].Row == startRow + 2 && computerLocations[i].Col == startCol - 2)
                    {
                        targetSquare = computerLocations[i];
                    }
                }

                if (midSquare != null && targetSquare == null)
                {
                    targetRow = startRow + 2;
                    targetCol = startCol - 2;
                    return true;
                }
            }
            return false;
        }
        //Logic for move down right
        public static bool TryMoveDownRight(out GameAction action, out int targetRow, out int targetCol, int pieceIndex, List<BoardLocation> playerLocations, List<BoardLocation> computerLocations)
        {
            targetRow = 0;
            targetCol = 0;
            action = GameAction.DownRight;
            if (computerLocations[pieceIndex].Col + 1 < ROWS && computerLocations[pieceIndex].Col + 1 < COLS)
            {
                int startRow = computerLocations[pieceIndex].Row;
                int startCol = computerLocations[pieceIndex].Col;
                BoardLocation midPiece = null!;
                // check if there is a midpiece of the player
                for (int i = 0; i < playerLocations.Count; i++)
                {
                    if (playerLocations[i].Row == startRow + 1 && playerLocations[i].Col == startCol + 1)
                    {
                        midPiece = playerLocations[i];
                    }
                }
                // check if there is a midpiece of the computer
                for (int i = 0; i < computerLocations.Count; i++)
                {
                    if (computerLocations[i].Row == startRow + 1 && computerLocations[i].Col == startCol + 1)
                    {
                        midPiece = computerLocations[i];
                    }
                }

                if (midPiece == null)
                {
                    targetRow = startRow + 1;
                    targetCol = startCol + 1;
                    return true;
                }
            }
            return false;
        }

        //Logic for move down left
        public static bool TryMoveDownLeft(out GameAction action, out int targetRow, out int targetCol, int pieceIndex, List<BoardLocation> playerLocations, List<BoardLocation> computerLocations)
        {
            targetRow = 0;
            targetCol = 0;
            action = GameAction.Downleft;
            if (computerLocations[pieceIndex].Row + 1 < ROWS && computerLocations[pieceIndex].Col - 1 >= 0)
            {
                int startRow = computerLocations[pieceIndex].Row;
                int startCol = computerLocations[pieceIndex].Col;
                BoardLocation midPiece = null!;
                // check if there is a midpiece of the player
                for (int i = 0; i < playerLocations.Count; i++)
                {
                    if (playerLocations[i].Row == startRow + 1 && playerLocations[i].Col == startCol - 1)
                    {
                        midPiece = playerLocations[i];
                    }
                }
                // check if there is a midpiece of the computer
                for (int i = 0; i < computerLocations.Count; i++)
                {
                    if (computerLocations[i].Row == startRow + 1 && computerLocations[i].Col == startCol - 1)
                    {
                        midPiece = computerLocations[i];
                    }
                }

                if (midPiece == null)
                {
                    targetRow = startRow + 1;
                    targetCol = startCol - 1;
                    return true;
                }
            }
            return false;

        }
        //Logic for move up right (reverse right for the computer)
        public static bool TryMoveUpRight(out GameAction action, out int targetRow, out int targetCol, int pieceIndex, List<BoardLocation> playerLocations, List<BoardLocation> computerLocations)
        {
            targetRow = 0;
            targetCol = 0;
            action = GameAction.UpRight;
            if (computerLocations[pieceIndex].isReversed)
            {
                return false;
            }
            if (computerLocations[pieceIndex].Row - 1 >= 0 && computerLocations[pieceIndex].Col + 1 < COLS)
            {
                int startRow = computerLocations[pieceIndex].Row;
                int startCol = computerLocations[pieceIndex].Col;
                BoardLocation midPiece = null!;
                // check if there is a midpiece of the player
                for (int i = 0; i < playerLocations.Count; i++)
                {
                    if (playerLocations[i].Row == startRow - 1 && playerLocations[i].Col == startCol + 1)
                    {
                        midPiece = playerLocations[i];
                    }
                }
                // check if there is a midpiece of the computer
                for (int i = 0; i < computerLocations.Count; i++)
                {
                    if (computerLocations[i].Row == startRow - 1 && computerLocations[i].Col == startCol + 1)
                    {
                        midPiece = computerLocations[i];
                    }
                }
                if (midPiece == null)
                {
                    targetRow = startRow - 1;
                    targetCol = startCol + 1;
                    return true;
                }
            }
            return false;

        }

        //Logic for move up left (reverse left for the computer)
        public static bool TryMoveUpLeft(out GameAction action, out int targetRow, out int targetCol, int pieceIndex, List<BoardLocation> playerLocations, List<BoardLocation> computerLocations)
        {
            targetRow = 0;
            targetCol = 0;
            action = GameAction.UpRight;
            if (computerLocations[pieceIndex].isReversed)
            {
                return false;
            }
            if (computerLocations[pieceIndex].Row - 1 >= 0 && computerLocations[pieceIndex].Col - 1 >= 0)
            {
                int startRow = computerLocations[pieceIndex].Row;
                int startCol = computerLocations[pieceIndex].Col;
                BoardLocation midPiece = null!;
                // check if there is a midpiece of the player
                for (int i = 0; i < playerLocations.Count; i++)
                {
                    if (playerLocations[i].Row == startRow - 1 && playerLocations[i].Col == startCol - 1)
                    {
                        midPiece = playerLocations[i];
                    }
                }
                // check if there is a midpiece of the computer
                for (int i = 0; i < computerLocations.Count; i++)
                {
                    if (computerLocations[i].Row == startRow - 1 && computerLocations[i].Col == startCol - 1)
                    {
                        midPiece = computerLocations[i];
                    }
                }

                if (midPiece == null)
                {
                    targetRow = startRow - 1;
                    targetCol = startCol - 1;
                    return true;
                }
            }
            return false;
        }
    }
}
