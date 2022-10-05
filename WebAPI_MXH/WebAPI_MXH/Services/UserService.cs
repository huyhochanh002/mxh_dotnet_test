using Microsoft.EntityFrameworkCore;
using WebAPI_MXH.Data;
using WebAPI_MXH.models;

namespace WebAPI_MXH.Services
{
    public class UserService
    {
        private readonly AppDBContext _context;
        public UserService(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }

        // add user 
        public async Task<bool> AddUser(User user)
        {
            try {
                var userExist = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (userExist == null)
                {
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                throw new Exception("Email Đã Tồn Tại !");
                return false;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        // Chỉnh sữa user
        public async Task<bool> UpdateUser( Guid Id,User user)
        {
            var userExist = await _context.Users.FirstOrDefaultAsync(u =>u.Id == Id);
            if(userExist == null)
            {
                return false;
            }
            userExist.DisplayName= user.DisplayName;
            userExist.Email= user.Email;
            userExist.Address= user.Address;
            userExist.DateOfBirth= user.DateOfBirth;
            await _context.SaveChangesAsync();
            return true;
     
        }
        public async Task<bool> DeleteUSer(Guid Id, User user)
        {
            var userExist = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if (userExist==null)
            {
                return false;
            }
            else
            {
                 _context.Users.Remove(userExist);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        // GET ALL
        public async Task<List<User>> GetAllUser()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
 
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        }

        public async Task<User> FindById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        }





    }
}
