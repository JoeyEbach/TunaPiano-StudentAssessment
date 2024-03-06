using Microsoft.EntityFrameworkCore;
using TunaPianoStudentAssessment.Models;

public class TunaPianoStudentAssessmentDbContext : DbContext
{
	public DbSet<Artist> Artists { get; set; }
	public DbSet<Song> Songs { get; set; }
	public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.Entity<Artist>().HasData(new Artist[]
		{
			new Artist {Id = 1, Name = "Kanye West", Age = 34, Bio = "Greatest rapper of all time"},
			new Artist {Id = 2, Name = "ColdPlay", Age = 46, Bio = "90s rock band"},
			new Artist {Id = 3, Name = "Michael Jackson", Age = 64, Bio = "King of Pop"},
			new Artist {Id = 4, Name = "Keith Urban", Age = 50, Bio = "Country Superstar"}
		});

		modelBuilder.Entity<Song>().HasData(new Song[]
		{
			new Song {Id = 1, Title = "Power", Album = "Black Skinhead", ArtistId = 1, Length = 2.32M},
			new Song {Id = 2, Title = "Fix You", Album = "XO", ArtistId = 2, Length = 3.43M},
			new Song {Id = 3, Title = "Scream", Album = "Scream", ArtistId = 3, Length = 2.59M},
			new Song {Id = 4, Title = "Kiss A Girl", Album = "Tonight", ArtistId = 4, Length = 2.35M}
		});

		modelBuilder.Entity<Genre>().HasData(new Genre[]
		{
			new Genre {Id = 1, Description = "Hip Hop"},
			new Genre {Id = 2, Description = "Rock"},
			new Genre {Id = 3, Description = "Pop"},
			new Genre {Id = 4, Description = "Country"}
		});  
    }
	
public TunaPianoStudentAssessmentDbContext(DbContextOptions<TunaPianoStudentAssessmentDbContext> context) : base(context)
	{

	}
}




