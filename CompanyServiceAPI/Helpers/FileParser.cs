using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyServiceAPI.Helpers
{
	
	public class FileParser
	{
		public string FileName { get; set; }
		public float MaxFileSizeMB { get; set; }
		public List<IFormFile> FileParts { get; set; }
		public IFormFile FormFileParts { get; set; }

		public FileParser()
		{
			FileParts = new List<IFormFile>();
		}



		public List<IFormFile> SplitFile(IFormFile file, float MaxFileSize)
		{
			
			string FileName = file.FileName;
			int BufferChunkSize = (int)(MaxFileSize * 1024);
			const int READBUFFER_SIZE = 1024;
			byte[] FSBuffer = new byte[READBUFFER_SIZE];
			using (FileStream FS = new FileStream(FileName, FileMode.Create, (FileAccess)FileShare.ReadWrite))
			{
				
				file.CopyTo(FS);
				int TotalFileParts = 0;
				if (file.Length < BufferChunkSize)
				{
					TotalFileParts = 1;

				}
				else
				{
					float PreciseFileParts = ((float)file.Length / (float)BufferChunkSize);
					TotalFileParts = (int)Math.Ceiling(PreciseFileParts);
				}

				int FilePartCount = 0;
				FS.Position = 0;
				while (FS.Position < FS.Length)
				{
					string FilePartName = string.Format("{0}.part_{1}.{2}", FileName, (FilePartCount + 1).ToString(), TotalFileParts.ToString());

					
					using (FileStream FilePart = new FileStream(FilePartName, FileMode.Create, (FileAccess)FileShare.ReadWrite))
					{
						int bytesRemaining = BufferChunkSize;
						int bytesRead = 0;
						var ms = new MemoryStream();
						
						while (bytesRemaining > 0 && (bytesRead = FS.Read(FSBuffer, 0, Math.Min(bytesRemaining, READBUFFER_SIZE))) > 0)
						{
							ms.Write(FSBuffer, 0, bytesRead);
							bytesRemaining -= bytesRead;
						}
						
						FileParts.Add(new FormFile(ms,0,ms.Length, FilePartName, FilePartName));
						
					}
					FilePartCount++;
					File.Delete(FilePartName);



				}
				FS.Close();
				File.Delete(FS.Name);
			}
			
			return FileParts;
		}

	}

	


}

