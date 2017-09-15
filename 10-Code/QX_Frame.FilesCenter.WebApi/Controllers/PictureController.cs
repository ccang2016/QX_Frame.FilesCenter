/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-5-26 16:54:46
 * Update:2017-09-15 18:00:58
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
using QX_Frame.App.WebApi;
using QX_Frame.Bantina;
using QX_Frame.Bantina.Extends;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace QX_Frame.FilesCenter.Controllers
{
    public class PictureController : WebApiControllerBase
    {
        const string UPLOAD_DIR = @"Upload";

        //POST : api/Picture
        public async Task<IHttpActionResult> Post()
        {
            string SAVE_DIR = UPLOAD_DIR;
            //check file type
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new Exception_DG("unsupported media type", 2005);
            }
            string dir = Path.Combine(IO_Helper_DG.RootPath_MVC, "/temp");

            IO_Helper_DG.CreateDirectoryIfNotExist(dir);

            var provider = new MultipartFormDataStreamProvider(dir);

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
                string saveDir = Path.Combine(IO_Helper_DG.RootPath_MVC, SAVE_DIR);

                IO_Helper_DG.CreateDirectoryIfNotExist(saveDir);

                if (File.Exists(file.LocalFileName))
                {
                    //get file name
                    string fileName = file.Headers.ContentDisposition.FileName.Substring(1, file.Headers.ContentDisposition.FileName.Length - 2);
                    //create new file name
                    string newFileName = String.Concat(Guid.NewGuid(), ".", fileName.Split('.')[1]);

                    string newFullFileName = Path.Combine(saveDir, newFileName);

                    fileNameList.Add(Path.Combine(SAVE_DIR, newFileName));

                    FileInfo fileInfo = new FileInfo(file.LocalFileName);

                    fileTotalSize += fileInfo.Length;

                    sb.Append($" #{fileIndex} Uploaded file: {newFileName} ({ fileInfo.Length} bytes)");

                    fileIndex++;

                    File.Move(file.LocalFileName, newFullFileName);

                    Trace.WriteLine("1 file copied , filePath=" + newFullFileName);
                }
            }
            return OK($"{fileNameList.Count} file(s) /{fileTotalSize} bytes uploaded successfully! Details -> {sb.ToString()} -- qx_frame {DateTime_Helper_DG.Get_DateTime_Now_24HourType()}", fileNameList, fileNameList.Count);
        }

        //POST : api/Picture/id
        public async Task<IHttpActionResult> Post(string id)
        {
            string SAVE_DIR = string.IsNullOrEmpty(id) ? UPLOAD_DIR : Path.Combine(UPLOAD_DIR, id);
            //check file type
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new Exception_DG("unsupported media type", 2005);
            }
            string dir = Path.Combine(IO_Helper_DG.RootPath_MVC, "/temp");

            IO_Helper_DG.CreateDirectoryIfNotExist(dir);

            var provider = new MultipartFormDataStreamProvider(dir);

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
                string saveDir = Path.Combine(IO_Helper_DG.RootPath_MVC, SAVE_DIR);

                IO_Helper_DG.CreateDirectoryIfNotExist(saveDir);

                if (File.Exists(file.LocalFileName))
                {
                    //get file name
                    string fileName = file.Headers.ContentDisposition.FileName.Substring(1, file.Headers.ContentDisposition.FileName.Length - 2);
                    //create new file name
                    string newFileName = String.Concat(Guid.NewGuid(), ".", fileName.Split('.')[1]);

                    string newFullFileName = Path.Combine(saveDir, newFileName);

                    fileNameList.Add(Path.Combine(SAVE_DIR, newFileName));

                    FileInfo fileInfo = new FileInfo(file.LocalFileName);

                    fileTotalSize += fileInfo.Length;

                    sb.Append($" #{fileIndex} Uploaded file: {newFileName} ({ fileInfo.Length} bytes)");

                    fileIndex++;

                    File.Move(file.LocalFileName, newFullFileName);

                    Trace.WriteLine("1 file copied , filePath=" + newFullFileName);
                }
            }
            return OK($"{fileNameList.Count} file(s) /{fileTotalSize} bytes uploaded successfully! Details -> {sb.ToString()} -- qx_frame {DateTime_Helper_DG.Get_DateTime_Now_24HourType()}", fileNameList, fileNameList.Count);
        }
    }
}
