using Azure.Storage.Blobs;
using System.Text.RegularExpressions;

namespace API.Services
{
    public class FileUpload
    {
        //private readonly string _connectioString;
        //private readonly string _containerName;

        //public FileUpload(IConfiguration configuration)
        //{
        //    _connectioString = configuration.GetValue<string>("BlobConnectionString");
        //    _containerName = configuration.GetValue<string>("BlobContainerName");
        //}

        public string UploadBase64IMage(string base64Image, string container)
        {
            string connectionString = Settings.ConexaoAzure;
            var fileName = Guid.NewGuid().ToString() + ".jpg";

            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

            byte[] imageBytes = Convert.FromBase64String(data);

            var blobClient = new BlobClient(connectionString, container, fileName);

            using (var stream = new MemoryStream(imageBytes))
            {
                blobClient.Upload(stream);
            }

            return blobClient.Uri.ToString();
        }
    }
}
