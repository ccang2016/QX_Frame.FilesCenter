
using System.Net;
using QX_Frame.Helper_DG;
/**
* author:qixiao
* create:2017-5-19 15:15:05
* */
namespace QX_Frame.FilesCenter.Helper
{
    public abstract class Return_Helper
    {
        public static object IsSuccess_Msg_Data_HttpCode(bool isSuccess, string msg, dynamic data, HttpStatusCode httpCode = HttpStatusCode.OK)
        {
            return Return_Helper_DG.IsSuccess_Msg_Data_HttpCode(isSuccess, msg, data, httpCode);
        }
        public static object Success_Msg_Data_DCount_HttpCode(string msg, dynamic data = null, int dataCount = 0, HttpStatusCode httpCode = HttpStatusCode.OK)
        {
            return Return_Helper_DG.Success_Msg_Data_DCount_HttpCode(msg, data, dataCount, httpCode);
        }
        public static object Error_Msg_Ecode_Elevel_HttpCode(string msg, int errorCode = 0, int errorLevel = 0, HttpStatusCode httpCode = HttpStatusCode.InternalServerError)
        {
            return Return_Helper_DG.Error_Msg_Ecode_Elevel_HttpCode(msg, errorCode, errorLevel, httpCode);
        }
    }
}
