
namespace HighProtein_Bogucki.Models
{
    public class Carousel
    {
        public string? ImageLink { get; set; }

        public string? Link { get; set; }

        public Carousel(string? imageLink, string? link)
        {
            ImageLink = imageLink;
            Link = link;
        }
    }
}
