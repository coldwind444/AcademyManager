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
        #region Account
        public async Task UpdateAccountAsync(Account acc, int type)
        {
            SetResponse response;
            if (type == 1)
            {
                response = await client.SetAsync("Accounts/InstructorAccounts/" + acc.UserID, acc);
            } else
            {
                response = await client.SetAsync("Accounts/StudentAccounts/" + acc.UserID, acc);
            }
        }
        public async Task SetPasswordAsync(string id, string pass, int type)
        {
            SetResponse response;
            if (type == 1)
            {
                response = await client.SetAsync($"Accounts/InstructorAccounts/{id}/Password", pass);
            }
            else
            {
                response = await client.SetAsync($"Accounts/StudentAccounts/{id}/Password", pass);
            }
        }
        public async Task<Account> GetAccountAsync(string id, int type)
        {
            FirebaseResponse response;
            if (type == 1)
            {
                response = await client.GetAsync("Accounts/InstructorAccounts/" + id);
            } else
            {
                response = await client.GetAsync("Accounts/StudentAccounts/" + id);
            }
            Account result = response.ResultAs<Account>();
            return result;
        }
        #endregion
        #region Student
        public async Task UpdateStudentAsync(string uuid, StudentUser user)
        {
            SetResponse response = await client.SetAsync("Students/" + uuid, user);
        }
        public async Task<StudentUser> GetStudentAsync(string id)
        {
            Account account = await GetAccountAsync(id, 2);
            string uid = "";
            if (account != null) uid = account.UUID;
            else return null;
            FirebaseResponse response = await client.GetAsync("Students/" + uid);
            StudentUser result = response.ResultAs<StudentUser>();
            return result;
        }
        #endregion
        #region Instructors
        public async Task UpdateInstructorAsync(string uuid, InstructorUser user)
        {
            SetResponse response = await client.SetAsync("Instructors/" + uuid, user);
        }
        public async Task<InstructorUser> GetInstructorAsync(string id)
        {
            Account account = await GetAccountAsync(id, 1);
            string uid = "";
            if (account != null) uid = account.UUID;
            else return null;
            FirebaseResponse response = await client.GetAsync("Instructors/" + uid);
            InstructorUser result = response.ResultAs<InstructorUser>();
            return result;
        }
        #endregion
        #region Term
        public async Task UpdateTermAsync(Term term)
        {
            SetResponse response = await client.SetAsync("Terms/" + term.TermID, term);
        }
        public async Task<Term> GetTermAsync(string id)
        {
            FirebaseResponse response = await client.GetAsync("Terms/" + id);
            Term result = response.ResultAs<Term>();
            return result;
        }
        #endregion
        #region Course
        public async Task UpdateCourseAsync(string termID, Course course)
        {
            SetResponse response = await client.SetAsync($"Terms/{termID}/Courses/{course.CourseID}", course);
        }
        public async Task<Course> GetCourseAsync(string termID, string courseId)
        {
            FirebaseResponse response = await client.GetAsync($"Terms/{termID}/Courses/{courseId}");
            Course result = response.ResultAs<Course>();
            return result;
        }
        #endregion
        #region Class
        public async Task UpdateClassAsync(string termID, string courseID, Class cls)
        {
            SetResponse response = await client.SetAsync($"Terms/{termID}/Courses/{courseID}/Classes/{cls.ClassID}", cls);
        }
        public async Task<Class> GetClassAsync(string termID, string courseId, string classID)
        {
            FirebaseResponse response = await client.GetAsync($"Terms/{termID}/Courses/{courseId}/Classes/{classID}");
            Class result = response.ResultAs<Class>();
            return result;
        }
        #endregion
        #region Admin
        public async Task UpdateAdminAsync(Admin ad)
        {
            SetResponse response = await client.SetAsync($"Admin/{ad.UUID}", ad);
        }
        public async Task<Admin> GetAdminAsync(string uuid)
        {
            FirebaseResponse response = await client.GetAsync($"Admin/{uuid}");
            Admin result = response.ResultAs<Admin>();
            return result;
        }
        public async Task UpdateAdminPassword(Admin ad)
        {
            SetResponse response = await client.SetAsync($"Admin/{ad.UUID}/Password", ad.Password);
        }
        #endregion
    }
}
