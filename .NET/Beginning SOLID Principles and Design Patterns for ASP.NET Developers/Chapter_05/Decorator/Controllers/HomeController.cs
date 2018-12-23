using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

using Decorator.Core;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Adapter.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment hostingEnvironment;

        public HomeController(IHostingEnvironment env)
        {
            this.hostingEnvironment = env;
        }

        public IActionResult Index()
        {
            return View();

        }

        public IActionResult GetImageOriginal()
        {
            var fi = hostingEnvironment.WebRootFileProvider.GetFileInfo("/images/computer.png");
            string fileName = fi.PhysicalPath;

            IPhoto photo = new Photo(fileName);
            Bitmap bmp = photo.GetPhoto();
            MemoryStream stream = new MemoryStream();
            bmp.Save(stream, ImageFormat.Png);
            byte[] data = stream.ToArray();
            stream.Close();
            return File(data, "image/png");
        }



        public IActionResult GetImageWatermarked()
        {
            var fi = hostingEnvironment.WebRootFileProvider.GetFileInfo("/images/computer.png");
            string fileName = fi.PhysicalPath;
            IPhoto photo = new Photo(fileName);
            WatermarkDecorator decorator = new WatermarkDecorator(photo, "Copyright (C) 2015.");
            Bitmap bmp = decorator.GetPhoto();
            MemoryStream stream = new MemoryStream();
            bmp.Save(stream, ImageFormat.Png);
            byte[] data = stream.ToArray();
            stream.Close();
            return File(data, "image/png");
        }


    }
}
