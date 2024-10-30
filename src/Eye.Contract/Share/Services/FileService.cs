using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Contract.Share.Static
{
    public static class FileService
    {
        public static bool IsValidFile(string folderPath, string fileName)
        {
            string manifestPath = Path.Combine(folderPath, fileName);
            return File.Exists(manifestPath);
        }
    }
}
