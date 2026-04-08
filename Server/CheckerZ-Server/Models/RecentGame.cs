namespace CheckerZ_Server.Models
{
    // Object to be used in query 23 (Shows names of players with last game date)
    public class RecentGame
    {
        public string Name { get; set; }
        public DateTime? RecentDate {  get; set; }
    }
}
