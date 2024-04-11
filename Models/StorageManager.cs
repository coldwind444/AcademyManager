using Firebase.Storage;
using System.IO;
using Firebase.Auth;

namespace AcademyManager.Models
{
    public class StorageManager
    {
        public StorageManager() { }
        public async Task UploadFileToFirebaseStorage(string localFilePath, string termID, string courseID, string classID, string title)
        {
            // Get a FileStream
            var stream = File.Open(localFilePath, FileMode.Open);
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
            var task = firebaseStorage
                .Child(termID)
                .Child(courseID)
                .Child(classID)
                .Child(filename)
                .PutAsync(stream);

            // Await the task to wait until upload completes and get the download url
            var downloadUrl = await task;

            // Update document's url and title to database
            DatabaseManager db = new DatabaseManager();
            Term term = await db.GetTermAsync(termID);
            term.Courses[courseID].Classes[classID].Documents.Add(new KeyValuePair<string, string>(title, downloadUrl));
        }
    }
}
