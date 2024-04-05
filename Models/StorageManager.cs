using Firebase.Storage;
using System.IO;
using Firebase.Auth;

namespace AcademyManager.Models
{
    public class StorageManager
    {
        public StorageManager() { }
        public async Task UploadFileToFirebaseStorage(string localFilePath)
        {
            // Get any Stream - it can be FileStream, MemoryStream or any other type of Stream
            var stream = File.Open(localFilePath, FileMode.Open);

            // Authenticate with Firebase Authentication
            var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBYRtGXfyYiaPdtd3FOUiSyhL9fmXaxsn4"));
            var email = Authentication.Email;
            var password = Authentication.Password;
            var authResult = await auth.SignInWithEmailAndPasswordAsync(email, password);

            // Initialize Firebase Storage client with authentication token
            var firebaseStorage = new FirebaseStorage(Authentication.StorageBucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(authResult.FirebaseToken)
                });

            // Constructr FirebaseStorage, path to where you want to upload the file and Put it there
            var task = firebaseStorage
                .Child("files")
                .Child("auth.json")
                .PutAsync(stream);

            // Track progress of the upload
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            // await the task to wait until upload completes and get the download url
            var downloadUrl = await task;
        }
    }
}
