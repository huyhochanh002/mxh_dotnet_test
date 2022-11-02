using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebAPI_MXH.Data;
using WebAPI_MXH.DTO;
using WebAPI_MXH.models;
using Group = WebAPI_MXH.models.Group;

namespace WebAPI_MXH.Services
{
    public class GroupService
    {
        //inject
        private readonly AppDBContext _context;
        public GroupService(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }
        //Tạo Group Mới
        public async Task<ApiResult<bool>> AddNewGroup(Group group)
        {
            try
            {
                var groupExist = await _context.Groups.FirstOrDefaultAsync(u => u.Id == group.Id);
                if (groupExist == null)
                {
                    await _context.Groups.AddAsync(group);
                    await _context.SaveChangesAsync();
                    return new ApiResult<bool>()
                    {
                        data = true,
                        IsSussces = true

                    };
                }
                throw new Exception("Đã Tồn Tại !");
            }
            catch (Exception e)
            {
                return new ApiResult<bool>()
                {
                    
                    IsSussces = false,
                    Errormessge="Lỗi Thêm Group mới"

                };
            }
        }
        // Chỉnh sữa Group
        public async Task<ApiResult<bool>> UpdateUser(Group group)
        {
            var GroupExist = await _context.Groups.FirstOrDefaultAsync(u => u.Id == group.Id);
            if (GroupExist == null)
            {
                return new ApiResult<bool>()
                {

                    IsSussces = false,
                    Errormessge = "Group Này không tồn tại "

                };
            }
            GroupExist.GroupName = group.GroupName;
            GroupExist.Description= group.Description;
            GroupExist.Rules = group.Rules;

            await _context.SaveChangesAsync();
            return new ApiResult<bool>()
            {
                data=true,
                IsSussces = true,
                
            };
        }

        // Xóa Group
        public async Task<ApiResult<bool>> DeleteGroup(Guid Id)
        {
            var GroupExist = await _context.Groups.FirstOrDefaultAsync(u => u.Id == Id);
            if (GroupExist == null)
            {
                return new ApiResult<bool>()
                {

                    IsSussces = false,
                    Errormessge = "Group Không tồn tại "

                };
            }
            else
            {
                _context.Groups.Remove(GroupExist);
                await _context.SaveChangesAsync();
                return new ApiResult<bool>()
                {

                    IsSussces = true,
                    data=true,

                };
            }
        }
        // GET ALL
        public async Task<ApiResult<List<Group>>> GetAllGroup()
        {
            var list= await _context.Groups.AsNoTracking().ToListAsync();
            return new ApiResult<List<Group>>()
            {

                IsSussces = true,
                data = list,

            };
        }

        public async Task<ApiResult<Group>> FindById(Guid id)
        {
            var Groupid= await _context.Groups.FirstOrDefaultAsync(u => u.Id == id);
            return new ApiResult<Group>()
            {

                IsSussces = true,
                data = Groupid,

            };

        }
        //Kiểm tra user tồn tại 
        public async Task<User> FindUser(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        }
        ///// MEMBER GROUP 
        public async Task<ApiResult<List<User>>> GetAllUserGroup(Guid id)
        {
            var GroupExist = await _context.Groups.FirstOrDefaultAsync(u=> u.Id==id);
            
            if (GroupExist == null)
            {
                
                var list = (List<User>)_context.Users.Where(u =>GroupExist.UsersInGroup.Any(ug => ug.UserId == u.Id));
                return new ApiResult<List<User>>()
                {

                    IsSussces = true,
                    data = list

                };

            }
            throw new Exception("Thất Bại");
            return new ApiResult<List<User>>()
            {

                IsSussces = false,
               Errormessge="Không tồn tại "

            };

        }
        public async Task<ApiResult<bool>> AddUserGroup(User userer,Guid id)
        {
            try
            {
                var groupExist = await _context.Groups.FirstOrDefaultAsync(u => u.Id == id);
                if (groupExist != null)
                {
                   var userwill= await _context.Comments.FirstOrDefaultAsync(u => u.Id == userer.Id);
                    if (userwill != null)
                    {
                        groupExist.UsersInGroup.Add(new UserGroup() {GroupId=id,UserId=userwill.Id});
                    }

                    await _context.SaveChangesAsync();
                    return new ApiResult<bool>()
                    {

                        IsSussces = true,
                        data = true,

                    };
                }
                throw new Exception("Group Không Tồn Tại !");
               
            }
            catch (Exception e)
            {
                return new ApiResult<bool>()
                {

                    IsSussces = false,
                    Errormessge="Group không tồn tại "

                };
            }
        }

    }
}
