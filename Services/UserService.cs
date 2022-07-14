using ChatApp.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ChatApp.Models;

namespace ChatApp.Services
{
    public class UserService
    {
        private readonly DataStorage dataStorage = DataStorage.GetDataStorage();

        

        public List<User>? FindFriend(User user, string name)
        {
            List<User>? userList = user.FriendList?.FindAll(friend => friend.UserName == name);
            if (userList == null)
            {
                return null;
            }
            return userList;
        }

        public string LoginByUsername(string username, string password)
        {
            User? aUser = dataStorage.Users.GetFirstOrDefault(user => user.UserName == username);

            if (aUser == null)
            {
                return "user not found";
            }
            if (aUser.Password != HashPassword(password, aUser.Salt))
            {
                return "Wrong password";
            }
            return $"username: {aUser.UserName} and password: {aUser.Password}";
        }


        public void RegisterUser(string username, string password)
        {
            int userId = GenerateUserId();

            User user = new User();
            user.Id = userId;
            user.UserName = username;
            user.Salt = GetRandomSalt(user.Salt);
            user.Password = HashPassword(password, user.Salt);
            dataStorage.Users.Add(user);
        }

        public int GenerateUserId()
        {
            int id;
            if (dataStorage.Users.GetAll().ToArray() == null)
            {
                id = 0;
            }
            id = dataStorage.Users.GetAll().ToArray().Length;
            return id;
        }


        public string HashPassword(string password, byte[]? salt)
        {

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
            return hashed;
        }

        public byte[] GetRandomSalt(byte[]? salt)
        {
            if (salt == null)
            {
                salt = new byte[16];
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }
            }
            return salt;
        }

    }
}
