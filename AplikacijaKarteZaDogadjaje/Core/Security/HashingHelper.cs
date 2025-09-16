using System.Security.Cryptography;
using System.Text;

namespace Core.Security
{
    public class HashingHelper
    {
        public static void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hash = new HMACSHA512())
            {
                passwordSalt = hash.Key;
                passwordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (passwordSalt == null)
            {
                return false;
            }
            using (var hash = new HMACSHA512(passwordSalt))
            {
                var computeHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }
    }
}
