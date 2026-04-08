namespace CheckerZ_Server.Objects
{
    //Object that represnts a location of a piece on the board sent from client side
    public class BoardLocation
    {
        public int Row { get; set; }

        public int Col { get; set; }

        public bool isReversed {  get; set; }
        public BoardLocation(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }
        public override string ToString()
        {
            return $"[{Row},{Col}]";
        }
    }
}
