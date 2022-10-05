namespace WebAPI_MXH.models
{
    public class User : BaseEntity
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }





        public ICollection<Post> Posts { get; set; }
     
        public ICollection<UserGroup> UserGroups { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<PostLiker> PostLikers { get; set; }
        


    }
}
