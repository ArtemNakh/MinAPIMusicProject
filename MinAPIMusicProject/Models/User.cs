namespace MinAPIMusicProject.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Login { get; set; }
    public string Password { get; set; }

    public virtual ICollection<Playlist> Playlists { get; set; }

    public virtual ICollection<Track> LikedTracks { get; set; } = new List<Track>();
}