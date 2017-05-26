using QX_Frame.App.WebApi;
using QX_Frame.FilesCenter.Helper;
using QX_Frame.Helper_DG;
using QX_Frame.Helper_DG.Extends;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

/**
 * author:qixiao
 * create:2017-5-26 16:54:46
 * */
namespace QX_Frame.FilesCenter.Controllers
{
    public class FilesController : WebApiControllerBase
    {
        //Get : api/Files
        public HttpResponseMessage Get(string fileName)
        {
            HttpResponseMessage result = null;

            DirectoryInfo directoryInfo = new DirectoryInfo(IO_Helper_DG.RootPath_MVC + @"Files/Files");
            FileInfo foundFileInfo = directoryInfo.GetFiles().Where(x => x.Name == fileName).FirstOrDefault();
            if (foundFileInfo != null)
            {
                FileStream fs = new FileStream(foundFileInfo.FullName, FileMode.Open);

                result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StreamContent(fs);
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = foundFileInfo.Name;
            }
            else
            {
                result = new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return result;
        }

        //POST : api/Files
        public async Task<IHttpActionResult> Post()
        {
            string root = IO_Helper_DG.RootPath_MVC;

            IO_Helper_DG.CreateDirectoryIfNotExist(root + "/temp");

            var provider = new MultipartFormDataStreamProvider(root + "/temp");

            // Read the form data.  
            await Request.Content.ReadAsMultipartAsync(provider);

            List<string> fileNameList = new List<string>();

            StringBuilder sb = new StringBuilder();

            long fileTotalSize = 0;

            int fileIndex = 1;

            // This illustrates how to get the file names.
            foreach (MultipartFileData file in provider.FileData)
            {
                //new folder
                string newRoot = root + @"Files/Files";

                IO_Helper_DG.CreateDirectoryIfNotExist(newRoot);

                if (File.Exists(file.LocalFileName))
                {
                    //new fileName
                    string fileName = file.Headers.ContentDisposition.FileName.Substring(1, file.Headers.ContentDisposition.FileName.Length - 2);

                    string newFileName = Guid.NewGuid() + "." + fileName.Split('.')[1];

                    string newFullFileName = newRoot + "/" + newFileName;

                    fileNameList.Add($"Files/Files/{newFileName}");

                    FileInfo fileInfo = new FileInfo(file.LocalFileName);

                    fileTotalSize += fileInfo.Length;

                    sb.Append($" #{fileIndex} Uploaded file: {newFileName} ({ fileInfo.Length} bytes)");

                    fileIndex++;

                    File.Move(file.LocalFileName, newFullFileName);

                    Trace.WriteLine("1 file copied , filePath=" + newFullFileName);
                }
            }

            return Json(Return_Helper.Success_Msg_Data_DCount_HttpCode($"{fileNameList.Count} file(s) /{fileTotalSize} bytes uploaded successfully!     Details -> {sb.ToString()}", fileNameList, fileNameList.Count));
        }
    }
}
