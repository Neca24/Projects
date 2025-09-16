using Core.Interfaces;
using Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User?> GetByEmail(string email);
        Task<User> GetById(int id);
    }
}
