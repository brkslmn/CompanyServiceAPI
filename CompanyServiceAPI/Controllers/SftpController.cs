using CompanyServiceAPI.Models;
using CompanyServiceAPI.Services;
using CompanyServiceAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace CompanyServiceAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SftpController : ControllerBase
	{
		private ISftpService _sftpService;
		private readonly ILogger<SftpController> _logger;
		public SftpConfig SftpConfig { get; }
		private readonly FileParser _helpers;
		private List<IFormFile> partingFiles;

		public SftpController(
			ISftpService sftpService,
			ILogger<SftpController> logger,
			Microsoft.Extensions.Options.IOptions<SftpConfig> sftpConfig,
			FileParser helpers

		)
		{
			_sftpService = sftpService;
			SftpConfig = sftpConfig.Value;
			_logger = logger;
			_helpers = helpers;
		}

		[HttpGet]
		public IActionResult GetSftpFiles(string remoteDirectory)
		{
			try
			{
				List<string> filesList = new List<string>();
				List<string> filesDir = new List<string>();
				

				var files = _sftpService.ListAllFilesFtp(remoteDirectory);
				foreach (var file in files)
				{
					if (file.Type == FluentFTP.FtpFileSystemObjectType.Directory)//FluentFTP.FtpFileSystemObjectType.Di....
					{

						filesDir.Add(file.Name);

					}
					else if (file.Type != FluentFTP.FtpFileSystemObjectType.Directory)
					{
						filesList.Add(file.Name);
					}
					else
					{
						return BadRequest("Empty Folder");
					}
				}
				return Ok(new
				{
					dirs = filesDir,
					files= filesList
					
				});
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Failed in listing files under [{remoteDirectory}]");
				return null;
			}
		}
	

		[HttpGet("DownloadFile")]
		public IActionResult DownloadSftpFile(string remoteFilePath, string localFilePath)
		{
			try
			{
				_sftpService.DownloadFile(remoteFilePath, localFilePath); 
				return Ok();
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"Failed in downloading file [{localFilePath}] to [{remoteFilePath}]");
				return BadRequest(new { message = exception.Message });
			}
		}
		[HttpPost("UploadFile")]
		public IActionResult UploadSftpFile(string remotePath)
		{
			try
			{
				//localFilePath = "C://Users//brkslmn/Desktop/Upload";
				
				var thisfile = Request.Form.Files;
				var Keylist = Request.Form.Keys;

				var MaxFileSize = Request.Form["MaxFileSize"];


				foreach (var file in thisfile)
				{
					partingFiles = _helpers.SplitFile(file, int.Parse(MaxFileSize));

					foreach (var partfile in partingFiles)
					{
						var Path = Request.Form.Keys.Last().ToString();
						_sftpService.UploadFile(partfile, Path);
					}
				}	
				return Ok(new
				{
					//fileName = thisfile.Name,
					devices = Request.Form.Keys.First().ToString(),
					path = Request.Form.Keys.Last().ToString()
				}) ;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed in uploading file");
				return BadRequest(new { message = ex.Message });
			}
		}
		[HttpDelete("DeleteFile")]
		public IActionResult DeleteSftpFiles(string remoteFilePath)
		{
			try
			{
				_sftpService.DeleteFile(remoteFilePath);
				return Ok(new
				{
					remoteFilePath
				});

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed in deleting file[{ remoteFilePath} ]");
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}
