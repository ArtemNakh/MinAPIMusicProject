using MinAPIMusicProject.Models;

namespace MinAPIMusicProject.DTOs
{
    public class LikedTrackDTO
    {
        public int TrackId { get; set; }
        public string TrackTitle { get; set; }
        public Genre Genre { get; set; }
        public int DurationInSeconds { get; set; }
    }
}
