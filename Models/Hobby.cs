namespace FinalProject.Models
{
    public class Hobby
    {
        public int Id { get; set; }                 
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int YearsDoingIt { get; set; }
        public bool IsActive { get; set; }
    }
}
