using WebAPI_MXH.models;

namespace WebAPI_MXH.DTO
{
    public class PostDTO 
    {
        public string Content { get; set; }
        public string Title { get; set; }


        public Guid AuthorId { get; set; }

        public Guid? GroupId { get; set; }
    }
}
