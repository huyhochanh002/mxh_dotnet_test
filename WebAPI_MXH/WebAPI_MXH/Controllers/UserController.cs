using Microsoft.AspNetCore.Mvc;
using WebAPI_MXH.models;
using WebAPI_MXH.Services;

namespace WebAPI_MXH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
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
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            var emailExist = await _userservice.FindByEmail(user.Email);
            if(emailExist == null)
            {
                var result = await _userservice.AddUser(user);
                return Ok(result);
            }
            else
            {
                return BadRequest(new { message = "Email Đã Tồn Tại ! " });
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                var userExist = await _userservice.FindById(user.Id);
                if (userExist == null)
                {
                    return NotFound("User không tồn tại");
                }
                var result = await _userservice.UpdateUser(user.Id, user);
                return Ok(result);
            }
            else
            {
                return BadRequest("Lỗi Xuất Hiện ! ");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByID([FromQuery] Guid id)
        {
            var userExist = await _userservice.FindById(id);
            if (userExist == null)
            {
                return NotFound("User không tồn tại ");
            }
            else
            {
                return Ok(userExist);
            }
        }


    }
}
