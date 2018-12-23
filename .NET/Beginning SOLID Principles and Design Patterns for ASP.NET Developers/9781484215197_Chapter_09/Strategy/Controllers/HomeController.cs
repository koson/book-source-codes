using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.Net.Http.Headers;
using System.IO;
using Strategy.Core;


namespace Strategy.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            string[] files = Directory.GetFiles(AppSettings.SourceFolder);
            List<string> fileNames = new List<string>();
            foreach(string file in files)
            {
                fileNames.Add(Path.GetFileName(file)); 
            }
            return View(fileNames);
        }

        [HttpPost]
        public IActionResult CompressFile(string selectedfile, string compressiontype)
        {
            string extension = "";
            string contentType = "";
            CompressionContext context = null;
            switch (compressiontype)
            {
                case "Deflate":
                    extension = ".cmp";
                    contentType = "application/deflate";
                    context = new CompressionContext(new DeflateAlgorithm());
                    break;
                case "GZip":
                    extension = ".gz";
                    contentType = "application/gzip";
                    context = new CompressionContext(new GZipAlgorithm());
                    break;
                case "Zip":
                    extension = ".zip";
                    contentType = "application/zip";
                    context = new CompressionContext(new ZipAlgorithm());
                    break;
            }
            string source = AppSettings.SourceFolder + $"\\{selectedfile}";
            string destination = AppSettings.DestinationFolder + $"\\{Path.GetFileNameWithoutExtension(selectedfile)}{extension}";
            context.Compress(source, destination);
            return File(destination, contentType,Path.GetFileName(destination));
        }
    }
}
