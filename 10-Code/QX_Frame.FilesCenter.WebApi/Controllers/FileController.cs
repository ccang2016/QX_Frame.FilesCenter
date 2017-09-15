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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace QX_Frame.FilesCenter.Controllers
{
    public class FileController : WebApiControllerBase
    {
        const string UPLOAD_DIR = @"Upload";

        //Get : api/File?fileName=1
        public HttpResponseMessage Get(string fileName)
        {
            HttpResponseMessage result = null;

            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(IO_Helper_DG.RootPath_MVC, UPLOAD_DIR));
            FileInfo foundFileInfo = directoryInfo.GetFiles().Where(x => x.Name == fileName).FirstOrDefault();
            if (foundFileInfo != null)
            {
                FileStream fs = new FileStream(foundFileInfo.FullName, FileMode.Open);
                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(fs)
                };
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = foundFileInfo.Name
                };

            }
            else
            {
                result = new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return result;
        }
        //Get : api/File?fileName=1&folderName=2
        public HttpResponseMessage Get(string folderName, string fileName)
        {
            HttpResponseMessage result = null;

            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(IO_Helper_DG.RootPath_MVC, UPLOAD_DIR, folderName));
            FileInfo foundFileInfo = directoryInfo.GetFiles().Where(x => x.Name == fileName).FirstOrDefault();
            if (foundFileInfo != null)
            {
                FileStream fs = new FileStream(foundFileInfo.FullName, FileMode.Open);
                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(fs)
                };
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = foundFileInfo.Name
                };
            }
            else
            {
                result = new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return result;
        }

        //POST : api/File
        public async Task<IHttpActionResult> Post()
        {
            string SAVE_DIR = UPLOAD_DIR;
            //save folder
            string root = Path.Combine(IO_Helper_DG.RootPath_MVC, SAVE_DIR);
            //check path is exist if not create it
            IO_Helper_DG.CreateDirectoryIfNotExist(root);

            IList<string> fileNameList = new List<string>();

            StringBuilder sb = new StringBuilder();

            long fileTotalSize = 0;

            int fileIndex = 1;

            //get files from request
            HttpFileCollection files = HttpContext.Current.Request.Files;

            await Task.Run(() =>
            {
                foreach (var f in files.AllKeys)
                {
                    HttpPostedFile file = files[f];
                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        string fileLocalFullName = Path.Combine(root, file.FileName);

                        file.SaveAs(fileLocalFullName);

                        fileNameList.Add(Path.Combine(SAVE_DIR, file.FileName));

                        FileInfo fileInfo = new FileInfo(fileLocalFullName);

                        fileTotalSize += fileInfo.Length;

                        sb.Append($" #{fileIndex} Uploaded file: {file.FileName} ({ fileInfo.Length} bytes)");

                        fileIndex++;

                        Trace.WriteLine("1 file copied , filePath=" + fileLocalFullName);
                    }
                }
            });
            return OK($"{fileNameList.Count} file(s) /{fileTotalSize} bytes uploaded successfully! Details -> {sb.ToString()} -- qx_frame {DateTime_Helper_DG.Get_DateTime_Now_24HourType()}", fileNameList, fileNameList.Count);
        }

        //POST : api/File/id
        public async Task<IHttpActionResult> Post(string id)
        {
            string SAVE_DIR = string.IsNullOrEmpty(id) ? UPLOAD_DIR : Path.Combine(UPLOAD_DIR, id);
            //save folder
            string root = Path.Combine(IO_Helper_DG.RootPath_MVC, SAVE_DIR);
            //check path is exist if not create it
            IO_Helper_DG.CreateDirectoryIfNotExist(root);

            IList<string> fileNameList = new List<string>();

            StringBuilder sb = new StringBuilder();

            long fileTotalSize = 0;

            int fileIndex = 1;

            //get files from request
            HttpFileCollection files = HttpContext.Current.Request.Files;

            await Task.Run(() =>
            {
                foreach (var f in files.AllKeys)
                {
                    HttpPostedFile file = files[f];
                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        string fileLocalFullName = Path.Combine(root, file.FileName);

                        file.SaveAs(fileLocalFullName);

                        fileNameList.Add(Path.Combine(SAVE_DIR, file.FileName));

                        FileInfo fileInfo = new FileInfo(fileLocalFullName);

                        fileTotalSize += fileInfo.Length;

                        sb.Append($" #{fileIndex} Uploaded file: {file.FileName} ({ fileInfo.Length} bytes)");

                        fileIndex++;

                        Trace.WriteLine("1 file copied , filePath=" + fileLocalFullName);
                    }
                }
            });
            return OK($"{fileNameList.Count} file(s) /{fileTotalSize} bytes uploaded successfully! Details -> {sb.ToString()} -- qx_frame {DateTime_Helper_DG.Get_DateTime_Now_24HourType()}", fileNameList, fileNameList.Count);
        }
    }
}
