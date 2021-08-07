namespace BlazorBattle2.Shared
{
    public class UserUnit
    {
        public int Id { get; set; }
        
        public Unit Unit { get; set; }
        
        public int UserId { get; set; }
        
        public int UnitId { get; set; }
        
        public int HitPoints { get; set; }
    }
}