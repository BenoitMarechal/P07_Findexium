using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Controllers.Domain
{
    public class Rating
    {
       
        [Key]
        public string Id { get; set; }
        [Required]
        public string MoodysRating { get; set; }
        [Required]
        public string SandPRating { get; set; }
        [Required]
        public string FitchRating { get; set; }
        public byte? OrderNumber { get; set; }
    }
}