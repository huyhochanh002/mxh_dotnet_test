namespace WebAPI_MXH.models
{
    public class UserGroup
    {

        public Guid GroupId { get; set; }
        public Group Group { get; set; }



        public Guid UserId { get; set; }
        public  User User { get; set; }


        public DateTime JoinAt { get; set; } = DateTime.UtcNow;



    }
}
