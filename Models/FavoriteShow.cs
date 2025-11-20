namespace FinalProject.Models
{
    public class FavoriteShow
    {
        public int Id { get; set; }                    
        public string Title { get; set; } = "";
        public string Genre { get; set; } = "";
        public int Seasons { get; set; }
        public bool IsAnimated { get; set; }
    }
}
