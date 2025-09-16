using BusinessLayer.Interfaces;
using Core.Result;
using Core.Security;
using DataAccessLayer.Interfaces;
using Entities;
using Entities.DTO;

namespace BusinessLayer.Implementation
{
    public class UserBusiness:IUserBusiness
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        public UserBusiness(IUserRepository userRepo,IRoleRepository roleRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }

        public async Task<ResultWrapper> DeleteUser(int userId)
        {
            var user = await _userRepo.GetById(userId);
            if (user == null)
            {
                return new ResultWrapper
                {
                    Success = false,
                    Message = "Greska prilikom brisanja"
                };
            }
            bool result = await _userRepo.Delete(user);

            if (result)
            {
                return new ResultWrapper
                {
                    Success = true,
                    Message = "Korisnik je uspesno obrisan"
                };
            }
            else
            {
                return new ResultWrapper
                {
                    Success = false,
                    Message = "Greska prilikom brisanja"
                };
            }
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _userRepo.GetAll();
            var roles = await _roleRepo.GetAll();

            var result = from u in users
                         join r in roles
                         on u.RoleId equals r.RoleId
                         select new UserDTO
                         {
                             UserId = u.UserId,
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             Email = u.Email,
                             Phone = u.Phone,
                             Role = r.Name
                         };

            return result.ToList();

        }

        public async Task<Role> GetRole(LoginDTO loginDTO)
        {
            var user = await _userRepo.GetByEmail(loginDTO.Email!);
            var role = await _roleRepo.GetById(user!.RoleId);

            return role;
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            UserDTO userDTO = new();

            var user = await _userRepo.GetById(userId);

            if (user != null)
            {
                userDTO.UserId = userId;
                userDTO.FirstName = user.FirstName;
                userDTO.LastName = user.LastName;
                userDTO.Email = user.Email;
                userDTO.Phone = user.Phone;
                userDTO.Role = user.RoleId == 1 ? "Administrator" : user.RoleId == 2 ? "Korisnik" : "Gost";
            }
            return userDTO;
        }

        public async Task<ResultWrapper> Login(LoginDTO loginDTO)
        {
            var user = await _userRepo.GetByEmail(loginDTO.Email!);
            if (user == null)
            {
                return new ResultWrapper
                {
                    Success = false,
                    Message = "Korisnik ne postoji"
                };
            }
            else
            {
                if (HashingHelper.VerifyHash(loginDTO.Password!, user.PasswordHash!, user.PasswordSalt!) == true)
                {

                    return new ResultWrapper
                    {
                        Success = true,
                        Message = "Uspesno ste se prijavili"
                    };
                }
                else
                {
                    return new ResultWrapper
                    {
                        Success = false,
                        Message = "Neuspesna prijava"
                    };
                }
            }

        }

        public async Task<ResultWrapper> Register(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return new ResultWrapper
                {
                    Success = false,
                    Message = "Greska prilikom registracije"
                };
            }

            byte[] passwordHash;
            byte[] passwordSalt;

            HashingHelper.CreateHash(registerDTO.Password!, out passwordHash, out passwordSalt);

            User user = new User
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                Phone = registerDTO.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 2
            };

            await _userRepo.Add(user);

            return new ResultWrapper
            {
                Success = true,
                Message = "Uspesno ste se registrovali"
            };
        }

        public async Task<ResultWrapper> UpdateUser(UserDTO user)
        {
            var u = await _userRepo.GetById(user.UserId);
            if (u == null)
            {
                return new ResultWrapper
                {
                    Success = false,
                    Message = "Korinik nije azuriran"
                };
            }

            var result = await _userRepo.Update(u);
            if (result)
            {
                return new ResultWrapper
                {
                    Success = true,
                    Message = "Korisnik je azuriran"
                };
            }
            else
            {
                return new ResultWrapper
                {
                    Success = false,
                    Message = "Korinik nije azuriran"
                };
            }
        }
    }
}
