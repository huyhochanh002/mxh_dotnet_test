namespace WebAPI_MXH.models
{
    public class Group:BaseEntity
    {
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string   Rules { get; set; }

        public ICollection<UserGroup> UsersInGroup { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
