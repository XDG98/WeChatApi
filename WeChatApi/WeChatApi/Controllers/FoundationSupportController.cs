using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WeChatApi.Models;

namespace WeChatApi.Controllers
{
    /// <summary>
    /// 基础支持: 获取access_token接口 /token
    /// </summary>
    public class FoundationSupportController : Controller
    {
        // GET: FoundationSupport

        #region GET：基础支持 获取access_token接口 /token

        /// <summary>
        /// GET：基础支持 获取access_token接口 /token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            var grant_type = "client_credential";
            var appid = "wx01664cc255657421";
            var secret = "e6a5a463614fc2593a0a6780e67f4327";

            //请求地址
            string serviceAddress = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}", grant_type, appid, secret);
            //创建 HTTP Request 请求实例
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            //请求方式
            request.Method = "GET";
            //请求标头的值类型
            request.ContentType = "text/html;charset=UTF-8";

            //创建 HTTP Response 响应实例
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //获取流 该流读取服务器响应体  将流放入字节流序列视图中
            Stream myResponseStream = response.GetResponseStream();
            //实现TextReader    使其以一种特定编码从字节流中读取字符
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            //读取流当前位置到结束位置所有字符
            var characters = myStreamReader.ReadToEnd();
            //结束读取字符
            myStreamReader.Close();
            //结束获取流
            myResponseStream.Close();

            //数据序列化
            JavaScriptSerializer json = new JavaScriptSerializer();
            //tokne令牌
            var tokenInfo = json.Deserialize(characters, typeof(TokenModel)) as TokenModel;
            //errorInfo
            var errorInfo = json.Deserialize(characters, typeof(StatusModel)) as StatusModel;
            if (errorInfo.errcode != 0 && !string.IsNullOrWhiteSpace(errorInfo.errmsg))
            {
                return errorInfo.errcode + "," + errorInfo.errmsg;
            }
            else
            {
                return tokenInfo.access_token + "," + tokenInfo.expires_in;
            }

        }

        #endregion

    }

}