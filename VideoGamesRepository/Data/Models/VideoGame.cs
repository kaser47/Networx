using System.ComponentModel.DataAnnotations;
using VideoGamesRepository.Models.Enums;

namespace VideoGamesRepository.Data.Models
{
    public class VideoGame
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public int PublishedYear { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double Price { get; set; }
    }
}
