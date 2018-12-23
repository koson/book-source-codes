using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.IO;
using System.IO.Compression;

namespace Strategy.Core
{
    public class ZipAlgorithm : ICompressionAlgorithm
    {
        public void Compress(string source, string destination)
        {
            using (ZipArchive zip = ZipFile.Open(destination, ZipArchiveMode.Create))
            {
                zip.CreateEntryFromFile(source, Path.GetFileName(source));
            }
        }
    }

}
