using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eDnevnik.Helper
{
    public class FileUploadDelete
    {
        public static void Delete(IWebHostEnvironment hostingEnvironment, string folder, string pathZaBrisanje)
        {
            string strPhysicalFolder = Path.Combine(hostingEnvironment.WebRootPath, folder);
            string strFileFullPath = Path.Combine(strPhysicalFolder, pathZaBrisanje);
            if (System.IO.File.Exists(strFileFullPath))
            {
                System.IO.File.Delete(strFileFullPath);
            }
        }
        

        public static string GetRoute(string name)
        {
            return "~/imgUpload/" + name;
        }

        public static string Upload(IWebHostEnvironment hostingEnvironment, IFormFile file, string folder, string pathZaBrisanjeStarog = null)
        {

            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, folder);
                uniqueFileName = Guid.NewGuid().ToString().Substring(0, 10) + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fs);
                fs.Close();

                if (pathZaBrisanjeStarog != null)
                {
                    Delete(hostingEnvironment, folder, pathZaBrisanjeStarog);
                }
            }

            return uniqueFileName;
        }

    }

}
