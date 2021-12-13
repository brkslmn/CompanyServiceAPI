using System;
using System.Collections.Generic;
using System.IO;
using CompanyServiceAPI.Models;
using FluentFTP;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using Microsoft.AspNetCore.Http;


namespace CompanyServiceAPI.Services
{
    public interface ISftpService
    {
        dynamic ListAllFilesFtp(string remoteDirectory);
        void DownloadFile(string remoteFilePath, string localFilePath);
        void UploadFile(IFormFile file,string remotePath);
        void DeleteFile(string remoteFilePath);

    }

    public class SftpService : ISftpService
    {

		private readonly ILogger<SftpService> _logger;

        
        public SftpConfig SftpConfig { get; }

        public SftpService(ILogger<SftpService> logger, Microsoft.Extensions.Options.IOptions<SftpConfig> sftpConfig)
        {
            _logger = logger;
            SftpConfig = sftpConfig.Value;
        }

        public dynamic ListAllFilesFtp(string remoteDirectory)
        {
            using var client = new FtpClient(SftpConfig.Host, SftpConfig.Port, SftpConfig.UserName, SftpConfig.Password);
            
            client.Connect();
            
            return client.GetListing(remoteDirectory);

        }

        public void UploadFile(IFormFile file,string remotePath)
        {
            using var client = new FtpClient(SftpConfig.Host, SftpConfig.Port, SftpConfig.UserName, SftpConfig.Password);
            try
            {
                client.Connect();
                string sftpPath = remotePath + "/" + file.FileName;

                client.Upload(file.OpenReadStream(), sftpPath);
  
                
                _logger.LogInformation($"Finished uploading file to.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Failed in uploading file to.");
            }
            finally
            {
                client.Disconnect();
            }
        }


        public void DownloadFile(string remoteFilePath, string localFilePath)
        {
            using var client = new FtpClient(SftpConfig.Host, SftpConfig.Port, SftpConfig.UserName, SftpConfig.Password);
            try
            {
                client.Connect();
                using var s = File.Create(localFilePath);
                client.Download(s, remoteFilePath);
                _logger.LogInformation($"Finished downloading file [{localFilePath}] from [{remoteFilePath}]");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Failed in downloading file [{localFilePath}] from [{remoteFilePath}]");
            }
            finally
            {
                client.Disconnect();
            }
        }

        public void DeleteFile(string remoteFilePath)
        {
            using var client = new FtpClient(SftpConfig.Host, SftpConfig.Port, SftpConfig.UserName, SftpConfig.Password);
            try
            {
                client.Connect();
                client.DeleteFile(remoteFilePath);
                _logger.LogInformation($"File [{remoteFilePath}] deleted.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Failed in deleting file [{remoteFilePath}]");
            }
            finally
            {
                client.Disconnect();
            }
        }
	}
}