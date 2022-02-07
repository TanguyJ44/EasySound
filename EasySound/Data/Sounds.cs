using System.ComponentModel.DataAnnotations;

namespace EasySound.Data
{
    public class Sounds
    {
        [Key]
        public int SoundId { get; set; }
        public string? PlaylistId { get; set; }
        public string? SoundName { get; set; }
    }
}
