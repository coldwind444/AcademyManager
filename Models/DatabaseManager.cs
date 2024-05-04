using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Security.Cryptography;
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
                MessageBox.Show("Không thể kết nối đến CSDL.\n Vui lòng kiểm tra lại đường truyền.", "Lỗi kết nối", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #region Account
        public async Task<bool> UpdateAccountAsync(Account acc, int type)
        {
            SetResponse response;
            if (type == 1)
            {
                response = await client.SetAsync("Accounts/InstructorAccounts/" + acc.UserID, acc);
            } else
            {
                response = await client.SetAsync("Accounts/StudentAccounts/" + acc.UserID, acc);
            }
            if (response != null) return true;
            return false;
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
        public async Task<bool> UpdateStudentAsync(string uuid, StudentUser user)
        {
            SetResponse response = await client.SetAsync("Students/" + uuid, user);
            if (response != null) return true;
            return false;
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
        public async Task<bool> UpdateInstructorAsync(string uuid, InstructorUser user)
        {
            SetResponse response = await client.SetAsync("Instructors/" + uuid, user);
            if (response != null) return true;
            return false;
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
        public async Task<bool> UpdateTermAsync(Term term)
        {
            SetResponse response = await client.SetAsync("Terms/" + term.TermID, term);
            if (response != null) return true;
            return false;
        }
        public async Task<Term> GetTermAsync(string id)
        {
            FirebaseResponse response = await client.GetAsync("Terms/" + id);
            Term result = response.ResultAs<Term>();
            return result;
        }
        public async Task<string> GetCurrentTermAsync()
        {
            FirebaseResponse response = await client.GetAsync("CurrentTerm");
            string result = response.ResultAs<string>();
            return result;
        }
        #endregion
        #region Course
        public async Task<Course> GetCourseAsync(string termID, string courseId)
        {
            FirebaseResponse response = await client.GetAsync($"Terms/{termID}/Courses/{courseId}");
            Course result = response.ResultAs<Course>();
            return result;
        }
        #endregion
        #region Class
        public async Task<Class> GetClassAsync(string termID, string courseId, string classID)
        {
            FirebaseResponse response = await client.GetAsync($"Terms/{termID}/Courses/{courseId}/Classes/{classID}");
            Class result = response.ResultAs<Class>();
            return result;
        }
        public async Task<bool> UpdateScoreAsync(string termid, string courseid, string classid, Dictionary<string, StudentRecord> score)
        {
            string path = $"Terms/{termid}/Courses/{courseid}/Classes/{classid}/Students";
            SetResponse res = await client.SetAsync(path, score);
            if (res != null) return true;
            return false;
        }
        #endregion
        #region Admin
        public async Task<Admin> GetAdminAsync(string uuid)
        {
            FirebaseResponse response = await client.GetAsync($"Admin/{uuid}");
            Admin result = response.ResultAs<Admin>();
            return result;
        }
        public async Task<bool> UpdateAdminPassword(Admin ad)
        {
            SetResponse response = await client.SetAsync($"Admin/{ad.UUID}/Password", ad.Password);
            if (response != null) return true;
            return false;
        }
        #endregion
        #region Student List
        public async Task<bool> AddStudentAsync(string termid, string courseid, string classid, string stid, StudentRecord rec)
        {
            string path = $"Terms/{termid}/Courses/{courseid}/Classes/{classid}/Students/{stid}";
            SetResponse response = await client.SetAsync(path, rec);
            if (response != null) return true;
            return false;
        }
        public async Task<bool> RemoveStudentAsync(string termid, string courseid, string classid, string stid)
        {
            string path = $"Terms/{termid}/Courses/{courseid}/Classes/{classid}/Students/{stid}";
            FirebaseResponse response = await client.DeleteAsync(path);
            if (response != null) return true;
            return false;
        }
        #endregion
        #region Notifications
        public async Task RemoveNotificationAsync(string uuid, int type, int nid)
        {
            if (type == 1)
            {
                string path = $"Instructors/{uuid}/Notifications/{nid}";
                FirebaseResponse res = await client.DeleteAsync(path);
            } else
            {
                string path = $"Students/{uuid}/Notifications/{nid}";
                FirebaseResponse res = await client.DeleteAsync(path);
            }
        }
        public async Task<bool> SendNotificationAsync(string receiverid, int type, Notification n)
        {
            Account acc = await GetAccountAsync(receiverid, type);
            if (acc == null) { return false; }
            string path;
            if (type == 1) path = $"Instructors/{acc.UUID}/Notifications/{n.ID}";
            else path = $"Students/{acc.UUID}/Notifications/{n.ID}";
            SetResponse response = await client.SetAsync(path, n);
            if (response != null) return true;
            return false;
        }
        #endregion
        #region Documents
        public async Task<bool> UploadDocumentAsync(string termid, string courseid, string classid, KeyValuePair<string, string> doc)
        {
            string path = $"Terms/{termid}/Courses/{courseid}/Classes/{classid}/Documents/{doc.Key}";
            SetResponse response = await client.SetAsync(path, doc.Value);
            if (response != null) return true;
            return false;
        }
        #endregion
    }
}
