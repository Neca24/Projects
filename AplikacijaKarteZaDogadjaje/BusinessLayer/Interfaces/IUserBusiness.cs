using Core.Result;
using Entities;
using Entities.DTO;

namespace BusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        Task<ResultWrapper> Register(RegisterDTO registerDTO);
        Task<ResultWrapper> Login(LoginDTO loginDTO);
        Task<Role> GetRole(LoginDTO loginDTO);
        Task<List<UserDTO>> GetAllUsers();
        Task<ResultWrapper> DeleteUser(int userId);
        Task<ResultWrapper> UpdateUser(UserDTO user);
        Task<UserDTO> GetUserById(int userId);
    }
}
