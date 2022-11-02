using Microsoft.AspNetCore.Mvc;
using WebAPI_MXH.DTO;
using WebAPI_MXH.models;
using WebAPI_MXH.Services;

namespace WebAPI_MXH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController:Controller
    {
        private readonly CommentService _CommentService;
        public CommentController(CommentService commentService)
        {
            _CommentService = commentService;
        }

        // get all user
        [HttpGet]
        public async Task<IActionResult> GetAllComment()
        {
            var comment = await _CommentService.GetAllComment();

            return Ok(comment);
        }

        //get comment by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentbyID(Guid id)
        {
            var CommentExist = await _CommentService.FindById(id);
            if (CommentExist == null)
            {
                return NotFound("User không tồn tại ");
            }
            return Ok(CommentExist);
        }

        // tạo comment
        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentDTO comment)
        {
            var UserExist = await _CommentService.FindById(comment.AuthorId);
            if (UserExist != null)
            {
                Comment newcom = new Comment();
                newcom.Content = comment.Content;
                var result = await _CommentService.AddComment(newcom);
                return Ok(result);
            }
            else
            {
                return BadRequest(new { message = "Comment Đã tồn tại Tồn Tại ! " });
            }
        }

        // chỉnh sữa comment

        [HttpPut]
        public async Task<IActionResult> EditComment(CommentDTO commentedit)
        {
            if (ModelState.IsValid)
            {
                var comment = await _CommentService.FindById(commentedit.Id);
                if (comment == null)
                {
                    return NotFound("Comment không tồn tại");
                }
                comment.Content = commentedit.Content;
                var result = await _CommentService.UpdateComment(comment);
                return Ok(result);
            }
            else
            {
                return BadRequest("Lỗi Xuất Hiện ! ");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromQuery] Guid id)
        {
            var CommentExist = await _CommentService.FindById(id);
            if (CommentExist == null)
            {
                return NotFound("User không tồn tại ");
            }
            else
            {
                await _CommentService.DeleteComment(id);
            }
            return Ok(CommentExist);

        }




    }
}
