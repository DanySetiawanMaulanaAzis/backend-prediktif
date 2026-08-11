using prediktif.Models;

namespace prediktif.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
