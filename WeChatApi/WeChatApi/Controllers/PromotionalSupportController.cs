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
    /// 推广支持: 创建二维码ticket接口 /qrcode/create
    /// </summary>
    public class PromotionalSupportController : Controller
    {
        // GET: PromotionalSupport

        /// <summary>
        /// 获取二维码页面
        /// </summary>
        /// <param name="errorInfo">请求状态信息</param>
        /// <param name="ticketUrl">二维码地址</param>
        /// <returns></returns>       
        public ActionResult GetTicket_View()
        {
            var ticketUrl = GetQrCode();
            if (!string.IsNullOrEmpty(ticketUrl))
            {
                ViewBag.ticketUrl = ticketUrl;
            }
            
            return View();
        }

        #region GET：推广支持: 创建二维码ticket接口 /qrcode/create

        /// <summary>
        /// GET：推广支持: 创建二维码ticket接口 /qrcode/create
        /// </summary>
        /// <returns></returns>
        public string GetTicket()
        {
            //实例化基础支持控制器
            FoundationSupportController FoundationSupport = new FoundationSupportController();
            //获取Token令牌
            var token = FoundationSupport.GetToken().Split(',')[0];

            //请求地址
            string serviceAddress = string.Format("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}", token);
            //创建 HTTP Request 请求实例
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            //请求方式
            request.Method = "POST";
            //请求标头的值类型
            request.ContentType = "application/json";

            QrCodeModel qrCode = new QrCodeModel();
            //获取二维码ticket Request 请求参数
            qrCode.action_name = "QR_LIMIT_STR_SCENE";
            qrCode.scene_str = "test";
            string strContent = @"{""action_name"": """ + qrCode.action_name + "\", \"action_info\": {\"scene\": {\"scene_str\": \"" + qrCode.scene_str + "\"}}}";

            //将参数字符写入流中特定编码
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }

            //创建 HTTP Response 响应实例
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //获取响应体的编码方式
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            //实现TextReader    使其以一种特定编码从字节流中读取字符
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            //读取流当前位置到结束位置所有字符
            var characters = reader.ReadToEnd();

            //数据序列化
            JavaScriptSerializer json = new JavaScriptSerializer();
            //tokne令牌
            var qrCodeJson = json.Deserialize(characters, typeof(QrCodeModel)) as QrCodeModel;
            //errorInfo
            var errorJson = json.Deserialize(characters, typeof(StatusModel)) as StatusModel;
            if (errorJson.errcode != 0 && !string.IsNullOrWhiteSpace(errorJson.errmsg))
            {
                return errorJson.errcode + "," + errorJson.errmsg;
            }
            else
            {
                return qrCodeJson.ticket + "," + qrCodeJson.url;
            }

        }

        #endregion

        #region GET：推广支持: 换取二维码 /showqrcode

        /// <summary>
        /// GET：推广支持: 换取二维码 /showqrcode
        /// </summary>
        /// <returns></returns>
        public string GetQrCode()
        {
            //获取二维码ticket令牌
            var ticket = GetTicket().Split(',')[0];
            var ticketUrl = "";
            if (!string.IsNullOrEmpty(ticket))
            {
                //请求地址
                ticketUrl = string.Format("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}", ticket);
                return ticketUrl;
            }
            else
            {
                return "Error";
            }
            //请求地址
            //string serviceAddress = string.Format("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}", ticket);
            //创建 HTTP Request 请求实例
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            //请求方式
            //request.Method = "GET";
            //请求标头的值类型
            //request.ContentType = "text/html;charset=UTF-8";

            //创建 HTTP Response 响应实例
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //获取流 该流读取服务器响应体  将流放入字节流序列视图中
            //Stream myResponseStream = response.GetResponseStream();
            //实现TextReader    使其以一种特定编码从字节流中读取字符
            //StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            //读取流当前位置到结束位置所有字符
            //var characters = myStreamReader.ReadToEnd();
            //结束读取字符
            //myStreamReader.Close();
            //结束获取流
            //myResponseStream.Close();

            //数据序列化 as model
            //JavaScriptSerializer json = new JavaScriptSerializer();
            //var statusJson = json.Deserialize(characters, typeof(StatusModel)) as StatusModel;
            //var qrCodeJson = json.Deserialize(characters, typeof(QrCodeModel)) as QrCodeModel;

            //if (statusJson.errcode == 0)
            //{
                //errorInfo = "请求成功！二维码地址：【" + qrCodeJson.url + "】";

                //return RedirectToAction("GetTicket_View", new { errorInfo = errorInfo });
                //return 
            //}
            //else if (statusJson.errcode != 0)
            //{
                //errorInfo = "请求失败！\t错误代码：" + statusJson.errcode + "\n错误信息：" + statusJson.errmsg;
                //return RedirectToAction("GetTicket_View", new { errorInfo = errorInfo });
            //}

            //return Content(statusJson.errcode.ToString());
        }

        #endregion

    }
}