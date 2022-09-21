namespace WebAPI_MXH.models
{
    public class Post:BaseEntity
    {
        public string Content { get; set; }
        public string Title { get; set; }





        public Guid AuthorId { get; set; }
        public User Author { get; set; }


        public Guid GroupId { get; set; }
        public Group Group { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<User> Likers { get; set; }
    }
}
