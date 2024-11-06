using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Controllers
{
    public class RuleName
    {
       
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Json { get; set; }
        [Required]
        public string Template { get; set; }
        [Required]
        public string SqlStr { get; set; }
        [Required]
        public string SqlPart { get; set; }
    }
}