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
    /// POST 向用户发送消息: 发送客服消息接口 /message/custom/send
    /// </summary>
    public class SendMessageController : Controller
    {
        // POST: SendMessage

        /// <summary>
        /// 向用户发送消息页面
        /// </summary>
        /// <param name="errorInfo">请求状态信息</param>
        /// <returns></returns>
        public ActionResult SendMessageToUser_View(string errorInfo)
        {
            ViewBag.errorInfo = errorInfo;
            return View();
        }

        #region POST：向用户发送消息: 发送客服消息接口 /message/custom/send

        /// <summary>
        /// POST：向用户发送消息: 发送客服消息接口 /message/custom/send
        /// </summary>
        [HttpPost]
        public ActionResult SendMessageToUser(FormCollection frm)
        {
            //发送状态信息
            var errorInfo = "";
            //发送用户消息内容
            var content = "";
            if (!string.IsNullOrEmpty(frm["content"]))
            {
                content = frm["content"];
            }
            else
            {
                errorInfo = "不能发送空白消息！";
                return RedirectToAction("SendMessageToUser_View", new { errorInfo = errorInfo });
            }

            //实例化基础支持控制器
            FoundationSupportController FoundationSupport = new FoundationSupportController();
            //消息Message请求参数 Model
            MessageModel message = new MessageModel();
            //获取Token令牌
            var token = FoundationSupport.GetToken().Split(',')[0];

            //请求地址
            string serviceAddress = string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", token);
            //创建 HTTP Request 请求实例
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            //请求方式
            request.Method = "POST";
            //请求标头的值类型
            request.ContentType = "application/json";

            //向用户发送消息 Request 请求参数
            message.touser = "odTGxwW1BBnf3VumvWZZC5T7AYy8";
            message.msgtype = "text";
            //requestMessage.content = "这是调用.Net后台发送的消息";
            string strContent = @"{""touser"":""" + message.touser + "\",\"msgtype\":\"" + message.msgtype + "\",\"" + message.msgtype + "\":{\"content\":\"" + content + "\"}}";
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

            StatusModel status = new StatusModel();
            //数据序列化 as model
            JavaScriptSerializer json = new JavaScriptSerializer();
            var statusJson = json.Deserialize(characters, typeof(StatusModel)) as StatusModel;
            //获取消息  Response 响应参数
            status.errcode = statusJson.errcode;
            status.errmsg = statusJson.errmsg;

            if (statusJson.errcode == 0)
            {
                errorInfo = "Success 成功发送,消息内容：【" + content + "】";
                return RedirectToAction("SendMessageToUser_View", new { errorInfo = errorInfo });
            }
            else if (statusJson.errcode != 0)
            {
                ViewBag.errorInfo = "Error 发送失败!\t错误代码：" + statusJson.errcode + "\n错误信息：" + statusJson.errmsg;
                return RedirectToAction("SendMessageToUser_View", new { errorInfo = errorInfo });
            }

            return Content(statusJson.errcode.ToString());
        }

    }

    #endregion

}