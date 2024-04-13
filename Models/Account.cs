using System.Text;
using System.Security.Cryptography;

namespace AcademyManager.Models
{
    public class Account
    {
        public string UserID { get; set; }
        public string UUID { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
        private string Password { get; set; }
        public Account(string stdid, string email, string password = null, int type = 0)
        {
            UserID = stdid;
            Email = email;
            Guid uid = Guid.NewGuid();
            UUID = uid.ToString();
            if (password != null) Password = HashPasswords(password);
            Type = type;
        }
        public bool IsVerified(string id, string email)
        {
            return id == UserID && Email == email; 
        }
        public bool Match(string pass, string id)
        {
            return UserID == id && pass == Password;
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
