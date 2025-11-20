namespace FinalProject.Models
{
    public class BreakfastFood
    {
        public int Id { get; set; }                   
        public string Name { get; set; } = "";
        public bool IsSweet { get; set; }
        public bool IsSavory { get; set; }
        public int Calories { get; set; }
    }
}
