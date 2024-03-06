using TunaPianoStudentAssessment.Models;
using TunaPianoStudentAssessment.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddNpgsql<TunaPianoStudentAssessmentDbContext>(builder.Configuration["TunaPianoStudentAssessmentDbConnectionString"]);
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//***Create a song
app.MapPost("/songs/new", (TunaPianoStudentAssessmentDbContext db, SongDto dto) =>
{
    try
    {
        Song newSong = new(){ Title = dto.Title, ArtistId = dto.ArtistId, Album = dto.Album, Length = dto.Length };
        db.Songs.Add(newSong);
        db.SaveChanges();
        return Results.Created($"/songs/new/{newSong.Id}", newSong);
    }
    catch
    {
        return Results.BadRequest("Unable to create a new song. Please try again.");
    }

});

//***Delete a song
app.MapDelete("/songs/delete/{id}", (TunaPianoStudentAssessmentDbContext db, int id) =>
{
    Song removeSong = db.Songs.FirstOrDefault(s => s.Id == id);
    if (removeSong != null)
    {
        db.Songs.Remove(removeSong);
        db.SaveChanges();
    }
    
    return Results.NoContent();
});

//***Update a song
app.MapPut("/songs/update/{id}", (TunaPianoStudentAssessmentDbContext db, SongDto dto, int id) =>
{
    var songToUpdate = db.Songs.SingleOrDefault(s => s.Id == id);
    if (songToUpdate == null)
    {
        return Results.NotFound();
    }
        songToUpdate.Title = dto.Title;
        songToUpdate.ArtistId = dto.ArtistId;
        songToUpdate.Length = dto.Length;
        db.SaveChanges();

    return Results.NoContent();
});

//***View all songs
app.MapGet("/songs", (TunaPianoStudentAssessmentDbContext db) =>
{
    return db.Songs.ToList();
});

//***View single song w/ genre & artist details
app.MapGet("/songs/{id}", (TunaPianoStudentAssessmentDbContext db, int id) =>
{
    return db.Songs
        .Include(s => s.Artist)
        .Include(s => s.Genres)
        .FirstOrDefault(s => s.Id == id);
});

//***Create an artist
app.MapPost("/artists/new", (TunaPianoStudentAssessmentDbContext db, ArtistDto dto) =>
{
    try
    {
        Artist newArtist = new() { Name = dto.Name, Age = dto.Age, Bio = dto.Bio };
        db.Artists.Add(newArtist);
        db.SaveChanges();
        return Results.Created($"/artists/new/{newArtist.Id}", newArtist);
    }
    catch
    {
        return Results.BadRequest("Unable to create new artist. Please try again.");
    }
});

//***Delete an artist
app.MapDelete("/artists/delete/{id}", (TunaPianoStudentAssessmentDbContext db, int id) =>
{
    var removeArtist = db.Artists.FirstOrDefault(a => a.Id == id);
    if (removeArtist != null)
    {
        db.Artists.Remove(removeArtist);
        db.SaveChanges();
    }
    return Results.NoContent();
});

//***Update an artist
app.MapPut("/artists/update/{id}", (TunaPianoStudentAssessmentDbContext db, ArtistDto dto, int id) =>
{
    var artistToUpdate = db.Artists.SingleOrDefault(a => a.Id == id);
    if (artistToUpdate == null)
    {
        return Results.NotFound();
    }
    artistToUpdate.Name = dto.Name;
    artistToUpdate.Age = dto.Age;
    artistToUpdate.Bio = dto.Bio;
    db.SaveChanges();
    return Results.NoContent();
});

//***View all artists
app.MapGet("/artists", (TunaPianoStudentAssessmentDbContext db) =>
{
    return db.Artists.ToList();
});

//**View single artist w/ songs
app.MapGet("/artists/{id}", (TunaPianoStudentAssessmentDbContext db, int id) =>
{
    return db.Artists
        .Include(a => a.Songs)
        .ThenInclude(s => s.Genres)
        .FirstOrDefault(a => a.Id == id);
});

//***Create a genre
app.MapPost("/genres", (TunaPianoStudentAssessmentDbContext db, GenreDto dto) =>
{
    try
    {
        Genre newGenre = new() { Description = dto.Description };
        db.Genres.Add(newGenre);
        db.SaveChanges();
        return Results.Created($"/genres/new/{newGenre.Id}", newGenre);
    }
    catch
    {
        return Results.BadRequest("Unable to create new genre. Please try again.");
    }

});

//***Delete a genre
app.MapDelete("/genre/delete/{id}", (TunaPianoStudentAssessmentDbContext db, int id) =>
{
    var removeGenre = db.Genres.FirstOrDefault(g => g.Id == id);
    if (removeGenre != null)
    {
        db.Genres.Remove(removeGenre);
        db.SaveChanges();
    }
    return Results.NoContent();
});

//***Update a genre
app.MapPut("/genres/update/{id}", (TunaPianoStudentAssessmentDbContext db, GenreDto dto, int id) =>
{
    Genre genreToUpdate = db.Genres.SingleOrDefault(g => g.Id == id);
    if (genreToUpdate == null)
    {
        return Results.NotFound();
    }
    genreToUpdate.Description = dto.Description;
    db.SaveChanges();
    return Results.NoContent();
});

//***View all genres
app.MapGet("/genres", (TunaPianoStudentAssessmentDbContext db) =>
{
    return db.Genres.ToList();
});

//***View single genre w/ songs
app.MapGet("/genres/{id}", (TunaPianoStudentAssessmentDbContext db, int id) =>
{
    return db.Genres
        .Include(g => g.Songs)
        .FirstOrDefault(g => g.Id == id);
});

//**Add a genre to a song
app.MapPost("/songs/newgenre", (TunaPianoStudentAssessmentDbContext db, SongGenreDto dto) =>
{
    Song songPick = db.Songs
          .Include(s => s.Genres)
          .SingleOrDefault(s => s.Id == dto.SongId);
    Genre genrePick = db.Genres.SingleOrDefault(g => g.Id == dto.GenreId);
    try
    {
        songPick.Genres.Add(genrePick);
        db.SaveChanges();
        return Results.NoContent();
    }
    catch
    {
        return Results.BadRequest("Unable to add genre to song. Please try again.");
    }
});

//**Delete a genre from a song
app.MapDelete("/songs/{songId}/removegenre/{genreId}", (TunaPianoStudentAssessmentDbContext db, int songId, int genreId) =>
{
    Song songPick = db.Songs
            .Include(s => s.Genres)
            .SingleOrDefault(s => s.Id == songId);
    Genre genrePick = db.Genres.SingleOrDefault(g => g.Id == genreId);
    try
    {
        songPick.Genres.Remove(genrePick);
        db.SaveChanges();
        return Results.NoContent();
    }
    catch
    {
        return Results.BadRequest("Unable to remove genre from song. Please try again.");
    }
});


app.Run();


