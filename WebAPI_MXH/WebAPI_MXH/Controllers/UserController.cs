using Microsoft.AspNetCore.Mvc;
using WebAPI_MXH.DTO;
using WebAPI_MXH.models;
using WebAPI_MXH.Services;

namespace WebAPI_MXH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly UserService _userservice;
        public UserController(UserService userService)
        {
            _userservice = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var users = await _userservice.GetAllUser();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByID([FromQuery]  Guid id)
        {
            var userExist = await _userservice.FindById(id);
            if (userExist == null)
            {
                return NotFound("User không tồn tại ");
            }
            return Ok(userExist);

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userdto)
        {
     
            try
            {
                User useradd =new User();
                useradd.DisplayName = userdto.DisplayName;
                useradd.Email = userdto.Email;
                useradd.Phone = userdto.Phone;
                useradd.Address = userdto.Address;
                useradd.DateOfBirth = userdto.DateOfBirth;


                var result = await _userservice.AddUser(useradd);
                return Ok(result);
            }
            catch(Exception e)
            {

            }
            return BadRequest(new { message = "Email Đã Tồn Tại ! " });
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(CreateUserDto userdto)
        {
            if (ModelState.IsValid)
            {
                var userExist = await _userservice.FindByEmail(userdto.Email);
                if (userExist == null)
                {
                    return NotFound("User không tồn tại");
                }
                var result = await _userservice.UpdateUser(userdto);
                return Ok(result);
            }
            else
            {
                return BadRequest("Lỗi Xuất Hiện ! ");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromQuery] Guid id)
        {
            var userExist = await _userservice.FindById(id);
            if (userExist == null)
            {
                return NotFound("User không tồn tại ");
            }
            else
            {
                await _userservice.DeleteUSer(id);
            }
            return Ok(userExist);

        }

    }
}

