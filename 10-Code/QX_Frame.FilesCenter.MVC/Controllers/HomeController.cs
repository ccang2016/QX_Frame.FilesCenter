using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadTest()
        {
            return View();
        }

        [HttpPost]
        //uploadify是逐次触发Uploadify的，所以每次只有一个文件，不需要foreach，
        public ActionResult Uploadify(HttpPostedFileBase uploadfile)
        {
            if (uploadfile != null && uploadfile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(uploadfile.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                uploadfile.SaveAs(path);
            }
            return Content("");
        }

        public ActionResult Upload(HttpPostedFileBase Filedata)
        {
            // 如果没有上传文件
            if (Filedata == null ||
                string.IsNullOrEmpty(Filedata.FileName) ||
                Filedata.ContentLength == 0)
            {
                return this.HttpNotFound();
            }

            // 保存到 ~/photos 文件夹中，名称不变
            string filename = System.IO.Path.GetFileName(Filedata.FileName);
            string virtualPath =
                string.Format("~/Uploads/{0}", filename);
            // 文件系统不能使用虚拟路径
            string path = this.Server.MapPath(virtualPath);

            Filedata.SaveAs(path);
            return this.Json(new { result = "success" });
        }
    }
}
