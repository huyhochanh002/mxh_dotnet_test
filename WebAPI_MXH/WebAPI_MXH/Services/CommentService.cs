using Microsoft.EntityFrameworkCore;
using WebAPI_MXH.Data;
using WebAPI_MXH.models;

namespace WebAPI_MXH.Services
{
    public class CommentService
    {
        //inject
        private readonly AppDBContext _context;
        public CommentService(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }
        // Thêm Comment 
        public async Task<bool> AddComment(Comment comment)
        {
            try
            {
                var userExist = await _context.Users.FirstOrDefaultAsync(u => u.Id==comment.AuthorId);
                if (userExist == null)
                {
                    await _context.Comments.AddAsync(comment);
                    await _context.SaveChangesAsync();
                    return true;
                }
                throw new Exception("Thêm Comment Thất Bại");
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        // Chỉnh sữa Comment
        public async Task<bool> UpdateComment( Comment Comment)
        {
            var CommentExist = await _context.Comments.FirstOrDefaultAsync(u => u.Id == Comment.Id);
            if (Comment == null)
            {
                return false;
            }
            CommentExist.Content = Comment.Content;
            await _context.SaveChangesAsync();
            return true;

        }
        // Xóa Comment
        public async Task<bool> DeleteComment(Guid Id)
        {
            var CommentExist = await _context.Comments.FirstOrDefaultAsync(u => u.Id == Id);
            if (CommentExist == null)
            {
                return false;
            }
            else
            {
                _context.Comments.Remove(CommentExist);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        // GET ALL
        public async Task<List<Comment>> GetAllComment()
        {
            return await _context.Comments.AsNoTracking().ToListAsync();

        }
        public async Task<Comment> FindById(Guid id)
        {
            return await _context.Comments.FirstOrDefaultAsync(u => u.Id == id);

        }
        public async Task<List<Comment>> GetAllCommentbyID(Guid id)
        {
            var UserExist = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(UserExist != null)
            {
                List<Comment> comments = new List<Comment>();
                comments=UserExist.Comments.Where(u => u.AuthorId == id).ToList();
                return comments;     
            }
            throw new Exception("Thất Bại");
            return null;

        }

    }
}
