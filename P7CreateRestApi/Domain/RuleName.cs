using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Controllers
{
    public class RuleName
    {
        // TODO: Map columns in data table RULENAME with corresponding fields
        [Key]
        public int Id { get; set; }
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