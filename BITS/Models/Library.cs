namespace BITS.Models
{
    public class Library
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public bool Favorite {  get; set; }
    }
}