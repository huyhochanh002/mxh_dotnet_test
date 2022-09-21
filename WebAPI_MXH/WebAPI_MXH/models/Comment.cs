namespace WebAPI_MXH.models
{
    public class Comment:BaseEntity
    {
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public Guid PostId { get; set; }
        public Post post { get; set; }
    }
}
