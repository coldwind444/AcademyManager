using Firebase.Auth;
using Firebase.Storage;
using System.IO;

namespace AcademyManager.Models
{
    public class StorageManager
    {
        public StorageManager() { }
        public async Task<bool> UploadFileToFirebaseStorage(string localFilePath, string termID, string courseID, string classID, string title)
        {
            // Get a FileStream
            FileStream stream = null;
            try
            {
                stream = File.Open(localFilePath, FileMode.Open);
            }
            catch
            {
                return false;
            }
            string filename = Path.GetFileName(localFilePath);

            // Authenticate with Firebase Authentication
            var auth = new FirebaseAuthProvider(new FirebaseConfig(Authentication.APIKey));
            var email = Authentication.Email;
            var password = Authentication.Password;
            var authResult = await auth.SignInWithEmailAndPasswordAsync(email, password);

            // Initialize Firebase Storage client with authentication token
            var firebaseStorage = new FirebaseStorage(Authentication.StorageBucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(authResult.FirebaseToken)
                });

            // Upload file to storage
            string downloadUrl = null;
            try
            {
                var task = firebaseStorage
                    .Child(termID)
                    .Child(courseID)
                    .Child(classID)
                    .Child(filename)
                    .PutAsync(stream);

                // Await the task to wait until upload completes and get the download url
                downloadUrl = await task;
            }
            catch
            {
                return false;
            }
            if (downloadUrl != null)
            {
                DatabaseManager db = new DatabaseManager();
                bool success = await db.UploadDocumentAsync(termID, courseID, classID, new KeyValuePair<string, string>(title, downloadUrl));
                if (success) return true;
            }
            return false;
        }
    }
}
