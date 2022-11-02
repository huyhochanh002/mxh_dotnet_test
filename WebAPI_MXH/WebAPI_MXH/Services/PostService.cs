using Microsoft.EntityFrameworkCore;
using WebAPI_MXH.Data;
using WebAPI_MXH.DTO;
using WebAPI_MXH.models;

namespace WebAPI_MXH.Services
{
    public class PostService
    {
        private readonly AppDBContext _context;
        public PostService(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }
        public async Task<ApiResult<bool>> AddPost(PostDTO post)
        {
            try
            {
                var UserExist = await _context.Users.FirstOrDefaultAsync(u => u.Id == post.AuthorId);
                if (UserExist != null)
                {
                    Post postmain = new Post();
                    postmain.Title = post.Title;
                    postmain.Content = post.Content;
                    postmain.AuthorId = post.AuthorId;
                    if (post.GroupId != null)
                    {
                        postmain.GroupId = post.GroupId;
                    }
                    await _context.Posts.AddAsync(postmain);
                    await _context.SaveChangesAsync();
                    return new ApiResult<bool>()
                    {
                        data = true,
                        IsSussces = true

                    };
                }
                throw new Exception("User Này Không Tồn Tại");
            }
            catch (Exception e)
            {
                return new ApiResult<bool>()
                {
                    data = false,
                    IsSussces = false,
                    Errormessge = " Lỗi rồi add user "

                };

            }
        }

        public async Task<ApiResult<bool>> UpdatePost(PostDTO postDTO, Guid id)
        {
            var PostExist = await _context.Posts.FirstOrDefaultAsync(u => u.Id == id);
            if (PostExist == null)
            {
                return new ApiResult<bool>()
                {
                    data = false,
                    IsSussces = false,
                    Errormessge = "Post này không tồn tại "


                };
            }
            PostExist.Title = postDTO.Title;
            PostExist.Content = postDTO.Content;

            await _context.SaveChangesAsync();
            return new ApiResult<bool>()
            {
                data = true,
                IsSussces = true

            };

        }

        public async Task<ApiResult<bool>> DeletePost(Guid Id)
        {
            var PostExist = await _context.Posts.FirstOrDefaultAsync(u => u.Id == Id);
            if (PostExist == null)
            {
                return new ApiResult<bool>()
                {

                    IsSussces = false,
                    Errormessge = "Post Không tồn tại "

                };
            }
            else
            {
                _context.Posts.Remove(PostExist);
                await _context.SaveChangesAsync();
                return new ApiResult<bool>()
                {
                    data = true,
                    IsSussces = true

                };
            }
        }

        // ApiResult 
        public async Task<ApiResult<List<Post>>> GetAllPost()
        {
            var Post = await _context.Posts.AsNoTracking().ToListAsync();
            return new ApiResult<List<Post>>()
            {
                data = Post,
                IsSussces = true

            };
        }
        public async Task<ApiResult<Post>> GetByUser(Guid id)
        {

            var Post = await _context.Posts.FirstOrDefaultAsync(u => u.AuthorId == id);
            return new ApiResult<Post>()
            {
                data = Post,
                IsSussces = true

            };

        }
        public async Task<ApiResult<Post>> FindPostByID(Guid id)
        {
            var Post = await _context.Posts.FirstOrDefaultAsync(u => u.AuthorId == id);
            return new ApiResult<Post>()
            {
                data = Post,
                IsSussces = true

            };
        }


        public async Task<ApiResult<List<UserDTO>>> GetPostLiker(Guid id)
        {
            var postExist = await _context.Posts.FirstOrDefaultAsync(u => u.Id == id);
            if (postExist != null)
            {
                var listuser = _context.Users.AsEnumerable().Where(u => postExist.PostLikers.Any(ug => ug.PostId == id)).Select(u => new UserDTO()
                {
                    Id = u.Id
                ,
                    displayname = u.DisplayName
                }).ToList();
                return new ApiResult<List<UserDTO>>()
                {
                    data = listuser,
                    IsSussces = true

                };
            }
            else
            {
                return new ApiResult<List<UserDTO>>()
                {
                    
                    IsSussces = false,
                    Errormessge="Không tồn tại post"

                };
            }
        }
        public async Task<ApiResult<bool>> AddPostLiker(Guid UserID, Guid PostID)
        {
            try
            {
                ///
                var PostExist = await _context.Posts.FirstOrDefaultAsync(u => u.Id == PostID);
                if (PostExist == null)
                {
                    return new ApiResult<bool>()
                    {
                        IsSussces = true,
                        Errormessge = "Add postliker thất bại 1"

                    };
                }

                PostExist.PostLikers.Add(new PostLiker() { PostId = PostID, UserId = UserID });
                await _context.SaveChangesAsync();
                return new ApiResult<bool>()
                {
                    data = true,
                    IsSussces = true

                };
            }
            catch (Exception e2)
            {
                return new ApiResult<bool>()
                {

                    IsSussces = true,
                    Errormessge = "Add PostLiker Thất Bại 2"

                };
            }
        }
    }

}
