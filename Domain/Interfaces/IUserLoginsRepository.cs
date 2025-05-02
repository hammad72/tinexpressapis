using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserLoginsRepository
    {
        Task<List<userlogins>> GetAllAsync();
        Task<userlogins> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<int> AddAsync(userlogins ul);
        Task UpdateAsync(userlogins ul);
        Task<userlogins?> GetByEmailandPassword(string email, string password);

        Task AddLoginDetailAsync(logindetail ld);
        Task AddUpdateLoginModel(loginmodel lm);
        Task<loginmodel?> GetLoginModel(string refreshToken);
        Task<socialinfo> socialGoogle(string requestToken);
        Task<socialinfo> socialFb(string requestToken);
        Task<userlogins?> GetByEmail(string email);
    }
}
