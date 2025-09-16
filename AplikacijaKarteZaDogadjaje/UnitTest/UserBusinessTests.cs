using BusinessLayer.Implementation;
using DataAccessLayer.Interfaces;
using Entities;
using Entities.DTO;
using Moq;
using Xunit;

namespace UnitTest
{
    public class UserBusinessTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IRoleRepository> _roleRepoMock;
        private readonly UserBusiness _userBusiness;

        public UserBusinessTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _roleRepoMock = new Mock<IRoleRepository>();
            _userBusiness = new UserBusiness(_userRepoMock.Object, _roleRepoMock.Object);
        }

        [Fact]
        public async Task DeleteUser_UserNotFound_ReturnsError()
        {
            _userRepoMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((User)null);

            var result = await _userBusiness.DeleteUser(1);

            Assert.False(result.Success);
            Assert.Equal("Greska prilikom brisanja", result.Message);
        }

        [Fact]
        public async Task DeleteUser_Success_ReturnsSuccessMessage()
        {
            var user = new User { UserId = 1 };
            _userRepoMock.Setup(r => r.GetById(1)).ReturnsAsync(user);
            _userRepoMock.Setup(r => r.Delete(user)).ReturnsAsync(true);

            var result = await _userBusiness.DeleteUser(1);

            Assert.True(result.Success);
            Assert.Equal("Korisnik je uspesno obrisan", result.Message);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsMappedList()
        {
            var users = new List<User>
            {
                new User { UserId = 1, FirstName = "Pera", LastName = "Peric", Email = "pera@test.com", Phone = "123", RoleId = 1 }
            };
            var roles = new List<Role>
            {
                new Role { RoleId = 1, Name = "Admin" }
            };

            _userRepoMock.Setup(r => r.GetAll()).ReturnsAsync(users);
            _roleRepoMock.Setup(r => r.GetAll()).ReturnsAsync(roles);

            var result = await _userBusiness.GetAllUsers();

            Assert.Single(result);
            Assert.Equal("Admin", result.First().Role);
        }

        [Fact]
        public async Task GetRole_UserNotFound_ReturnsError()
        {
            _userRepoMock.Setup(r => r.GetByEmail(It.IsAny<string>())).ReturnsAsync((User)null);

            var result = await _userBusiness.GetRole(new LoginDTO { Email = "test@test.com" });

            Assert.False(result.Success);
            Assert.Equal("Error", result.Message);
        }

        [Fact]
        public async Task GetRole_UserFound_ReturnsRoleName()
        {
            var user = new User { RoleId = 1 };
            var role = new Role { RoleId = 1, Name = "Admin" };

            _userRepoMock.Setup(r => r.GetByEmail(It.IsAny<string>())).ReturnsAsync(user);
            _roleRepoMock.Setup(r => r.GetById(1)).ReturnsAsync(role);

            var result = await _userBusiness.GetRole(new LoginDTO { Email = "test@test.com" });

            Assert.True(result.Success);
            Assert.Equal("Admin", result.Message);
        }

        [Fact]
        public async Task GetUserById_UserFound_ReturnsDTO()
        {
            var user = new User { UserId = 1, FirstName = "Pera", LastName = "Peric", Email = "pera@test.com", Phone = "123", RoleId = 1 };
            _userRepoMock.Setup(r => r.GetById(1)).ReturnsAsync(user);

            var result = await _userBusiness.GetUserById(1);

            Assert.Equal("Administrator", result.Role);
            Assert.Equal("Pera", result.FirstName);
        }

        [Fact]
        public async Task Login_UserNotFound_ReturnsError()
        {
            _userRepoMock.Setup(r => r.GetByEmail(It.IsAny<string>())).ReturnsAsync((User)null);

            var result = await _userBusiness.Login(new LoginDTO { Email = "test@test.com", Password = "pass" });

            Assert.False(result.Success);
            Assert.Equal("Korisnik ne postoji", result.Message);
        }

        [Fact]
        public async Task Register_NullDTO_ReturnsError()
        {
            var result = await _userBusiness.Register(null);

            Assert.False(result.Success);
            Assert.Equal("Greska prilikom registracije", result.Message);
        }

        [Fact]
        public async Task UpdateUser_UserNotFound_ReturnsError()
        {
            _userRepoMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((User)null);

            var result = await _userBusiness.UpdateUser(new UserDTO { UserId = 1 });

            Assert.False(result.Success);
            Assert.Equal("Korinik nije azuriran", result.Message);
        }

        [Fact]
        public async Task UpdateUser_Success_ReturnsSuccessMessage()
        {
            var user = new User { UserId = 1 };
            _userRepoMock.Setup(r => r.GetById(1)).ReturnsAsync(user);
            _userRepoMock.Setup(r => r.Update(user)).ReturnsAsync(true);

            var result = await _userBusiness.UpdateUser(new UserDTO { UserId = 1 });

            Assert.True(result.Success);
            Assert.Equal("Korisnik je azuriran", result.Message);
        }
    }
}
