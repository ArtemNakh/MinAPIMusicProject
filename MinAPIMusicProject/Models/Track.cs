﻿namespace MinAPIMusicProject.Models;

public class Track
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int DurationInSeconds { get; set; }
    public virtual Artist Artist { get; set; }
    public virtual Genre Genre { get; set; }

    public int Listened { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<User> LikedByUsers { get; set; } = new List<User>();
}

/*
 * User     Track + [Listened]        
 * api/tracks/id/play
 * api/tracks/
 */