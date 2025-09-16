using Core.Security;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Entities;

IUserRepository _userRepo = new UserRepository();

string password = "conga123";
byte[] passwordHash;
byte[] passwordSalt;

HashingHelper.CreateHash(password, out passwordHash,out passwordSalt);

User user = new User
{
    FirstName = "Nemanja",
    LastName = "Stojadinovic",
    Email = "banana@gmail.com",
    Phone = "12345",
    RoleId = 1,
    PasswordSalt=passwordSalt,
    PasswordHash = passwordHash
};

bool add = await _userRepo.Add(user);

Console.WriteLine(add);
