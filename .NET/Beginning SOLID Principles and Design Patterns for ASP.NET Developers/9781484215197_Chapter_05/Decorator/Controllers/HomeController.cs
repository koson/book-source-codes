using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Hosting;

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
            string fileName = hostingEnvironment.MapPath("images/computer.png");
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
            string fileName = hostingEnvironment.MapPath("images/computer.png");
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
