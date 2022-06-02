using System.ComponentModel.DataAnnotations;
namespace WebApi.Models.Posts
{
    public class PostRequest
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
    }
}
