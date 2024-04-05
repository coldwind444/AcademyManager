using System.Text;
using System.Security.Cryptography;

namespace AcademyManager.Models
{
    public class Account
    {
        public string ID { get; set; }
        public int Type { get; set; }
        private string Password { get; set; }
        public Account(string id, string password, int type)
        {
            ID = id;
            Password = HashPasswords(password);
            Type = type;
        }

        public bool Match(Account account)
        {
            return account.ID == ID && account.Password == Password;
        }
        public string HashPasswords(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder encodedpass = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    encodedpass.Append(bytes[i].ToString("x2"));
                }
                return encodedpass.ToString();
            }
        }
    }
}
