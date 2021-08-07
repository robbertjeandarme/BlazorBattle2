namespace BlazorBattle2.Shared
{
    public class UserStatisticResponse
    {
        public int Rank { get; set; }
        
        public int UserId { get; set; }
            
        public string UserName { get; set; }

        public int Battles { get; set; }
        
        public int Victories { get; set; }
        
        public int Defeats { get; set; }
    }
}