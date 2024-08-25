using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinAPIMusicProject.Data;
using MinAPIMusicProject.DTOs;

namespace MinAPIMusicProject.Endpoints
{
    public static class UserTrackEndpoints
    {
        public static void AddUserTrackEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoint = app.MapGroup("/api/users");

            endpoint.MapPost("/{userId}/like/{trackId}", async (
                MusicContext context,
                [FromRoute] int userId,
                [FromRoute] int trackId) =>
            {
                var user = await context.Users.Include(u => u.LikedTracks)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    return Results.NotFound("User not found");
                }

                var track = await context.Tracks.FindAsync(trackId);
                if (track == null)
                {
                    return Results.NotFound("Track not found");
                }

                if (user.LikedTracks.Any(t => t.Id == trackId))
                {
                    return Results.Conflict("Track already liked");
                }

                user.LikedTracks.Add(track);
                await context.SaveChangesAsync();

                return Results.Ok("Track liked successfully");
            })
            .WithDescription("Like a track by a user");

            endpoint.MapGet("/{userId}/liked-tracks", async (
                MusicContext context,
                [FromRoute] int userId) =>
            {
                var user = await context.Users.Include(u => u.LikedTracks)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    return Results.NotFound("User not found");
                }

                var likedTracks = user.LikedTracks.Select(track => new LikedTrackDTO
                {
                    TrackId = track.Id,
                    TrackTitle = track.Title,
                    Genre =track.Genre,
                    DurationInSeconds = track.DurationInSeconds
                });

                return Results.Ok(likedTracks);
            })
            .WithDescription("Get tracks liked by a user");
        }
    }
}
