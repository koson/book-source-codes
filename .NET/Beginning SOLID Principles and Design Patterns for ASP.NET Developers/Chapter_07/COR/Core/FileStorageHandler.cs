using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace COR.Core
{
    public class FileStorageHandler : IHandler
    {
        public IHandler NextHandler { get; set; }

        public void Process(string filename, string filecontent)
        {
            filename = AppSettings.StoragePath + "\\" + Path.GetFileNameWithoutExtension(filename) + DateTime.Now.ToString("yyyy-MM-dd") + Path.GetExtension(filename);
            System.IO.File.AppendAllText(filename, filecontent);
            using (AppDbContext db = new AppDbContext())
            {
                FileStoreEntry fse = new FileStoreEntry();
                fse.FileName = filename;
                fse.UploadedOn = DateTime.Now;
                db.FileStore.Add(fse);
                db.SaveChanges();
            }

            if (NextHandler != null)
            {
                NextHandler.Process(filename, filecontent);
            }
        }
    }
}
