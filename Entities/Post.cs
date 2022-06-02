namespace WebApi.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
    }
}
