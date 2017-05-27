using QX_Frame.Helper_DG;
using System;
using System.Web.Http;

namespace QX_Frame.FilesCenter.Controllers
{
    public class DefaultController : ApiController
    {
        public IHttpActionResult Get()
        {
            string ipAddress = Request.GetIpAddressFromRequest();
            return Json(new { Uid = Guid.NewGuid(), Name = "Default -- qixiao !", Value = "QX_Frame.Value" ,ipAddress= ipAddress });
        }

    }
}
