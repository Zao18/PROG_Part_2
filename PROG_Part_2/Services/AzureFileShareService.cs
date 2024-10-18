﻿using Azure.Storage.Files.Shares.Models;
using Azure.Storage.Files.Shares;
using PROG_Part_2.Models;

namespace PROG_Part_2.Services
{
    public class AzureFileShareService
    {
        private readonly string _connectionString;
        private readonly string _fileShareName;

        public AzureFileShareService(string connectionString, string fileShareName)
        {
            _connectionString = connectionString ??
                throw new ArgumentNullException(nameof(connectionString));
            _fileShareName = fileShareName ??
                throw new ArgumentNullException($"{nameof(fileShareName)}");
        }
        public async Task UploadFileAsync(string directoryName, string fileName, Stream fileStream)
        {
            try
            {
                var serviceClient = new ShareServiceClient(_connectionString);
                var shareClient = serviceClient.GetShareClient(_fileShareName);

                var directoryClient = shareClient.GetDirectoryClient(directoryName);
                await directoryClient.CreateIfNotExistsAsync();

                var fileClient = directoryClient.GetFileClient(fileName);

                await fileClient.CreateAsync(fileStream.Length);
                await fileClient.UploadRangeAsync(new Azure.HttpRange(0, fileStream.Length), fileStream);

            }
            catch (Exception ex)
            {
                throw new Exception("Error downloading file :" + ex.Message, ex);
            }
        }
        public async Task<List<Claims>> ListFileAsync(string directoryName)
        {
            var fileModels = new List<Claims>();
            try
            {
                var serviceClient = new ShareServiceClient(_connectionString);
                var shareCLient = serviceClient.GetShareClient(_fileShareName);

                var directoryClient = shareCLient.GetDirectoryClient(directoryName);
                await foreach (ShareFileItem item in directoryClient.GetFilesAndDirectoriesAsync())
                {
                    if (!item.IsDirectory)
                    {
                        var fileClient = directoryClient.GetFileClient(item.Name);
                        var properties = await fileClient.GetPropertiesAsync();
                        fileModels.Add(new Claims { DocumentName = item.Name, Size = properties.Value.ContentLength, LastModified = properties.Value.LastModified });
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Error listing files:" + ex.Message, ex); }
            return fileModels;
        }
        public async Task<Stream> DownLoadFileAsync(string directoryName, string fileName)
        {
            try
            {
                var serviceClient = new ShareServiceClient(_connectionString);
                var shareClient = serviceClient.GetShareClient(_fileShareName);
                var directoryClient = shareClient.GetDirectoryClient(directoryName);
                var fileClient = directoryClient.GetFileClient(fileName);

                var downloadInfo = await fileClient.DownloadAsync();
                return downloadInfo.Value.Content;
            }
            catch (Exception ex)
            {
                throw new Exception("Error downloading file: " + ex.Message, ex);
            }
        }

    }
}

