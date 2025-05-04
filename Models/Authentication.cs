using DotNetEnv;

namespace AcademyManager.Models
{
    public static class Authentication
    {
        static Authentication()
        {
            // Load the .env file on initialization
            Env.Load();
        }

        public static string DatabaseURL => Env.GetString("DATABASE_URL");
        public static string DatabaseSecret => Env.GetString("DATABASE_SECRET");
        public static string StorageBucket => Env.GetString("STORAGE_BUCKET");
        public static string Email => Env.GetString("EMAIL");
        public static string Password => Env.GetString("PASSWORD");
        public static string APIKey => Env.GetString("API_KEY");
    }
}
