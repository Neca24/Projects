using Core.Interfaces;
using Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IRoleRepository:IRepository<Role>
    {
        Task<Role> GetById(int id);
    }
}
