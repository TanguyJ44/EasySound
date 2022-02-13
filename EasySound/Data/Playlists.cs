using System.ComponentModel.DataAnnotations;

namespace EasySound.Data
{
    public class Playlists
    {
        [Key]
        public int PlaylistId { get; set; }
        public string? UserID { get; set; }
        public string? PlaylistName { get; set; }
    }
}
