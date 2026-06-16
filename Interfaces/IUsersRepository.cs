using prediktif.Models;

namespace prediktif.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> GetById(int id);

        Task<int> Create(User user);

        Task<int> Update(User user);

        Task<int> Delete(int id);
    }
}
