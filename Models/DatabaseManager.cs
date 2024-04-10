using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Windows;

namespace AcademyManager.Models
{
    public class DatabaseManager
    {
        private static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = Authentication.DatabaseSecret,
            BasePath = Authentication.DatabaseURL,
        };

        IFirebaseClient client = new FireSharp.FirebaseClient(config);
        public DatabaseManager()
        {
            if (client == null)
            {
                MessageBox.Show("Cannot connect to database.\n Please check your connection.", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }
        }
        public async Task AddAccount(Account acc)
        {
            SetResponse response = await client.SetAsync("Accounts/" + acc.Email, acc);
        }
        public async Task UpdateAccount(Account acc)
        {
            SetResponse response = await client.SetAsync("Accounts/" + acc.Email, acc);
        }
        public async Task<Account> GetAccountAsync(string email)
        {
            FirebaseResponse response = await client.GetAsync("Accounts/" + email);
            Account result = response.ResultAs<Account>();
            return result;
        }
        public async Task AddUserAsync(User user)
        {
            SetResponse response = await client.SetAsync("Users/" + user.ID, user);
        }
        public async Task UpdateUserAsync(User user)
        {
            SetResponse response = await client.SetAsync("Users/" + user.ID, user);
        }
        public async Task<User> GetUserAsync(string email)
        {
            Account account = await GetAccountAsync(email);
            string uid = account.ID;
            FirebaseResponse response = await client.GetAsync("Users/" + uid);
            User result = response.ResultAs<User>();
            return result;
        }
    }
}
