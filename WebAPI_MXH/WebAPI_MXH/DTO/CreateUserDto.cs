namespace WebAPI_MXH.DTO
{
    public class CreateUserDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
