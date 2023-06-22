using User_Mgmt_Api.Model;

namespace User_Mgmt_Api.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<User> InsertAsync(User user);
        Task<User> UpdateAsync(Guid id,User user);
        Task<User> DeleteAsync(Guid id);
        void SaveAsync();
    }
}
