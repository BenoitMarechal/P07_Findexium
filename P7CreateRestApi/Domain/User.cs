using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class User
    {
        
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}