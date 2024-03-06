using System.ComponentModel.DataAnnotations;

namespace TunaPianoStudentAssessment.DTOs
{
    public class SongDto
    {
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public string Album { get; set; }
        public decimal Length { get; set; }
    }
}

