using System;
using System.Web.Http;

namespace QX_Frame.FilesCenter.Controllers
{
    public class DefaultController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Json(new { Uid = Guid.NewGuid(), Name = "Default -- qixiao !", Value = "QX_Frame.Value" });
        }
    }
}
