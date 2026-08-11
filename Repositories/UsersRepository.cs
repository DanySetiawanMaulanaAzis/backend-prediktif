using Dapper;
using Microsoft.Data.SqlClient;
using prediktif.Interfaces;
using prediktif.Models;
using System.Data;

namespace prediktif.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IConfiguration _configuration;

        public UsersRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using var db = Connection;

            string sql = @"SELECT * FROM Users";

            return await db.QueryAsync<User>(sql);
        }

        public async Task<User> GetById(int id)
        {
            using var db = Connection;

            string sql = @"SELECT * FROM Users
                           WHERE UserId = @UserId";

            return await db.QueryFirstOrDefaultAsync<User>(sql, new
            {
                UserId = id
            });
        }

        public async Task<User> GetByNameAndPassword(string name, string password)
        {
            using var db = Connection;

            string sql = @"SELECT * FROM Users
                           WHERE Name = @Name AND Password = @Password";

            return await db.QueryFirstOrDefaultAsync<User>(sql, new
            {
                Name = name,
                Password = password
            });
        }

        public async Task<int> Create(CreateUserRequest user)
        {
            using var db = Connection;

            string sql = @"
                INSERT INTO Users
                (
                    Name,
                    Password,
                    Is_Operator,
                    Is_Technician,
                    Is_Engineer
                )
                VALUES
                (
                    @Name,
                    @Password,
                    @Is_Operator,
                    @Is_Technician,
                    @Is_Engineer
                )";

            return await db.ExecuteAsync(sql, user);
        }

        public async Task<int> Update(UpdateUserRequest user)
        {
            using var db = Connection;

            string sql = @"
                UPDATE Users
                SET
                    Name = @Name,
                    Password = @Password,
                    Is_Operator = @Is_Operator,
                    Is_Technician = @Is_Technician,
                    Is_Engineer = @Is_Engineer
                WHERE UserId = @UserId";

            return await db.ExecuteAsync(sql, user);
        }

        public async Task<int> Delete(int id)
        {
            using var db = Connection;

            string sql = @"DELETE FROM Users
                           WHERE UserId = @UserId";

            return await db.ExecuteAsync(sql, new
            {
                UserId = id
            });
        }
    }
}
