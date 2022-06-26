using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrontToBack5.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string Position { get; set; }
        public string İmageUrl { get; set; }

        [NotMapped]
        public IFormFile TeamPhoto { get; set; }
    }
}
