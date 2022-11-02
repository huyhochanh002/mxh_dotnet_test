using Microsoft.EntityFrameworkCore;
using WebAPI_MXH.Data;
using WebAPI_MXH.DTO;
using WebAPI_MXH.models;

namespace WebAPI_MXH.Services
{
    public class UserService
    {
        //inject
        private readonly AppDBContext _context;
        public UserService(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }

        // add user 
        public async Task<ApiResult<bool>> AddUser(User user)
        {
            try {
                var userExist = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (userExist == null)
                {
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return new ApiResult<bool>()
                    {
                        data = true,
                        IsSussces = true

                    };
                }
                throw new Exception("Email Đã Tồn Tại !");
            }
            catch(Exception e)
            {
                return new ApiResult<bool>()
                {
                    
                    IsSussces = false,
                    Errormessge="Lỗi nhập "

                };
            }
        }
        // Chỉnh sữa user
        public async Task<ApiResult<bool>> UpdateUser( /*Guid Id,*/CreateUserDto userdto)
        {
            var userExist = await _context.Users.FirstOrDefaultAsync(u=>u.Email==userdto.Email);
            if(userExist == null)
            {
                return new ApiResult<bool>()
                {

                    IsSussces = false,
                    Errormessge = "User không tồn tại "

                };
            }
            userExist.DisplayName= userdto.DisplayName;
            //userExist.Email= userdto.Email; // mail này giữ nguyên
            userExist.Phone= userdto.Phone;
            userExist.Address= userdto.Address;
            userExist.DateOfBirth= userdto.DateOfBirth;
            await _context.SaveChangesAsync();
            return new ApiResult<bool>()
            {
                data=true,
                IsSussces = false
                

            };

        }
        // Xóa User
        public async Task<ApiResult<bool>> DeleteUSer(Guid Id)
        {
            var userExist = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if (userExist==null)
            {
                return new ApiResult<bool>()
                {

                    IsSussces = false,
                    Errormessge = "User Không Tồn tại"

                };
            }
            else
            {
                 _context.Users.Remove(userExist);
                await _context.SaveChangesAsync();
                return new ApiResult<bool>()
                {
                    data = true,
                    IsSussces = true,
                   

                };
            }
        }
        // GET ALL
        public async Task<ApiResult<List<User>>> GetAllUser()
        {
            var listuser = await _context.Users.AsNoTracking().ToListAsync();
            return new ApiResult<List<User>>()
            {
                data = listuser,
                IsSussces = true
               
            };

        }

        public async Task<ApiResult<User>> FindByEmail(string email)
        {
            
            var userbyemail = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return new ApiResult<User>()
            {
                data = userbyemail,
                IsSussces = true

            };
        }

        public async Task<ApiResult<User>> FindById(Guid id)
        {
          

            var userbyid = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return new ApiResult<User>()
            {
                data = userbyid,
                IsSussces = true

            };

        }





    }
}
