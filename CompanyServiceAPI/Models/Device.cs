using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyServiceAPI.Models
{
	public class Device
	{
		[Key]
		public int id { get; set; }
		public string DeviceName { get; set; }
		public string Version { get; set; }
		public byte UploadFile { get; set; }
	}
}
