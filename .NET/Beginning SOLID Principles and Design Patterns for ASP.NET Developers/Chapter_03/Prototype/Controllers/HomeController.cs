using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Prototype.Core;




namespace Prototype.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IList<IFormFile> files)
        {
            foreach (var file in files)
            {
                ContentDispositionHeaderValue header = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                string fileName = header.FileName;
                fileName = fileName.Trim('"');
                fileName = Path.GetFileName(fileName);

                MemoryStream ms = new MemoryStream();
                Stream s = file.OpenReadStream();
                s.CopyTo(ms);
                byte[] data = ms.ToArray();
                s.Dispose();
                ms.Dispose();

                UploadedFile primaryObj = new UploadedFile();
                primaryObj.FileName = fileName;
                primaryObj.ContentType = file.ContentType;
                primaryObj.Size = file.Length;
                primaryObj.TimeStamp = DateTime.Now;
                primaryObj.FileContent = data;

                IUploadedFile backupObj = primaryObj.Clone();

                //IUploadedFile backupObj = primaryObj.DeepCopy();

                //send primaryObj to main system
                //send backupObj to backup system
            }
            ViewBag.Message = files.Count +  " file(s) uploaded successfully!";
            return View("Index");
        }
    }
}
