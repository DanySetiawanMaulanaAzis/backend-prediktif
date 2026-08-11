using prediktif.Models;

namespace prediktif.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> GetById(int id);

        Task<User> GetByNameAndPassword(string name, string password);

        Task<int> Create(CreateUserRequest user);

        Task<int> Update(UpdateUserRequest user);

        Task<int> Delete(int id);
    }
}
