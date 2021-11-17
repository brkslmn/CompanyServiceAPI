using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Net.Http.Headers;
using CompanyServiceAPI.Services;
using System.Net.Http;
using System;

namespace CompanyServiceAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : Controller
    {
        //public bool UploadFile(string FileName)
        //{
        //    bool rslt = false;
        //    using (var client = new HttpClient())
        //    {
        //        using (var content = new MultipartFormDataContent())
        //        {
        //            var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(FileName));
        //            fileContent.Headers.ContentDisposition = new
        //                ContentDispositionHeaderValue("attachment")
        //            {
        //                FileName = Path.GetFileName(FileName)
        //            };
        //            content.Add(fileContent);

        //            var requestUri = "http://localhost:8170/Home/UploadFile/";
        //            try
        //            {
        //                var result = client.PostAsync(requestUri, content).Result;
        //                rslt = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                // log error
        //                rslt = false;
        //            }
        //        }
        //    }
        //    return rslt;
        //}
    }
}