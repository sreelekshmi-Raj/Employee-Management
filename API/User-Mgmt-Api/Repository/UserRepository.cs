using Microsoft.EntityFrameworkCore;
using User_Mgmt_Api.Data;
using User_Mgmt_Api.Model;

namespace User_Mgmt_Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
           var users= await context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user= await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return null;
            return user;
        }
        public async Task<User> InsertAsync(User user)
        {
            user.Id=Guid.NewGuid();
            await context.Users.AddAsync(user);
            context.SaveChanges();
            //SaveAsync();
            return user;
        }
        public async Task<User> UpdateAsync(Guid id, User user)
        {
            var userDB = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            userDB.Name=user.Name;
            userDB.Email=user.Email;
            userDB.City=user.City;
            userDB.Country=user.Country;
            userDB.Region=user.Region;
            userDB.PostalCode=user.PostalCode;
            userDB.Phone=user.Phone;

            SaveAsync();
            return userDB;
        }
        public async Task<User> DeleteAsync(Guid id)
        {
            var userDB =await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userDB == null)
                return null;
            context.Users.Remove(userDB);
            SaveAsync();
            return userDB;
        }
        public void SaveAsync()
        {
            context.SaveChangesAsync();
        }
    }
}
