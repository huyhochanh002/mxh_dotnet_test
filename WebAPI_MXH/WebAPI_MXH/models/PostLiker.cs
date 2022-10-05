namespace WebAPI_MXH.models
{
    public class PostLiker
    {
        public Guid PostId { get; set; }

        public Post Post { get; set; }

        public Guid UserId {get; set; }

        public User User { get; set; }

        public DateTime JoinAt { get; set; } = DateTime.UtcNow;

    }
}
