using Microsoft.AspNetCore.Mvc;
using WebAPI_MXH.DTO;
using WebAPI_MXH.models;
using WebAPI_MXH.Services;

namespace WebAPI_MXH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController:ControllerBase
    {
        private readonly PostService _Postservice;
        public PostController(PostService postService)
        {
            _Postservice = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostList()
        {
            var posts = await _Postservice.GetAllPost();

            return Ok(posts) ;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostByID([FromQuery] Guid id)
        {
            var PostExist = await _Postservice.FindPostByID(id);
            if (PostExist == null)
            {
                return NotFound("User không tồn tại ");
            }
            return Ok(PostExist);
        }


        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody]PostDTO postdto)
        {
            var result = await _Postservice.AddPost(postdto);
            if (result.IsSussces)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { message = "Post này đã tồn tại hoặc không có User " });
            }
           
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> EditPost([FromQuery] Guid id,PostDTO postdto)
        {
            if (ModelState.IsValid)
            {
                var PostExist = await _Postservice.FindPostByID(id);
                if (PostExist != null)
                {
                    return NotFound("Post không tồn tại");
                }
                var result = await _Postservice.UpdatePost(postdto,id);
                return Ok(result);
            }
            else
            {
                return BadRequest("Lỗi Xuất Hiện ! ");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromQuery] Guid id)
        {
            var PostExist = await _Postservice.FindPostByID(id);
            if (PostExist == null)
            {
                return NotFound("User không tồn tại ");
            }
            else
            {
                await _Postservice.DeletePost(id);
            }
            return Ok(PostExist);

        }

    }
}
