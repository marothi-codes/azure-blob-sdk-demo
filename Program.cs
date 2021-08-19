using Azure.Storage.Blobs;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace PhotoSharingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            // Connect to the storage account.
            var connectionString = configuration.GetConnectionString("StorageAccount");
            string containerName = "photos";

            var container = new BlobContainerClient(connectionString, containerName);

            // Create the container if one does not exist already.
            container.CreateIfNotExists();

            // Upload an example image to the container.
            string blobName = "docs-and-friends-selfie-stick";
            string fileName = "docs-and-friends-selfie-stick.png";

            BlobClient blobClient = container.GetBlobClient(blobName);
            blobClient.Upload(fileName, true);

            var blobs = container.GetBlobs();

            foreach (var blob in blobs) 
            {
                Console.WriteLine($"{blob.Name} --> Created On: {blob.Properties.CreatedOn:yyyy-MM-dd HH:mm:ss} Size: {blob.Properties.ContentLength}");
            }
        }
    }
}
