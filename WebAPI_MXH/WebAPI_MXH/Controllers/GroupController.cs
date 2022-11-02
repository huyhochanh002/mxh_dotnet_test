using Microsoft.AspNetCore.Mvc;
using WebAPI_MXH.DTO;
using WebAPI_MXH.models;
using WebAPI_MXH.Services;

namespace WebAPI_MXH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController:Controller
    {
        private readonly GroupService _GroupService;
        public GroupController(GroupService groupService)
        {
            _GroupService = groupService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGroup()
        {
            var group = await _GroupService.GetAllGroup();

            return Ok(group);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupByID([FromQuery] Guid id)
        {
            var GroupExist = await _GroupService.FindById(id);
            if (GroupExist == null)
            {
                return NotFound("User không tồn tại ");
            }
            return Ok(GroupExist);
        }
        // get all user group
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetUserGroup([FromQuery] Guid id)
        {
            var usergroup = await _GroupService.GetAllUserGroup(id);
            return Ok(usergroup);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> Creategroup(GroupDTO groupdto , [FromQuery] Guid id)
        {
            var GroupExsist = await _GroupService.FindById(id);
            if (GroupExsist == null)
            {
                Group group = new Group();
                group.GroupName = groupdto.GroupName;
                group.Description=groupdto.Description;
                group.Rules=groupdto.Rules;
                var result = await _GroupService.AddNewGroup(group);
                return Ok(result);
            }
            else
            {
                return BadRequest(new { message = " đã tồn tại hoặc không có User " });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPost(GroupDTO group, [FromQuery] Guid id)
        {
            if (ModelState.IsValid)
            {
                var GroupExist = await _GroupService.FindById(id);
                if (GroupExist == null)
                {
                    return NotFound("Group không tồn tại");
                }

                GroupExist.data.GroupName = group.GroupName;
                GroupExist.data.Description = group.Description;
                GroupExist.data.Rules = group.Rules;
                var result = await _GroupService.UpdateUser(GroupExist.data);
                return Ok(result);
            }
            else
            {
                return BadRequest("Lỗi Xuất Hiện ! ");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup([FromQuery] Guid id)
        {
            var GroupExist = await _GroupService.FindById(id);
            if (GroupExist == null)
            {
                return NotFound("User không tồn tại ");
            }
            else
            {
                await _GroupService.DeleteGroup(id);
            }
            return Ok(GroupExist);

        }

        [HttpGet("adduser/{id}")]
        public async Task<IActionResult> AddUserGroup( Guid iduser,Guid idgroup)
        {
            var userExist = await _GroupService.FindUser(iduser);
            if (userExist == null)
            {
                return NotFound("User không tồn tại ");
            }
                var result = _GroupService.AddUserGroup(userExist, idgroup);
    
            return Ok(result);
        }



    }
}
