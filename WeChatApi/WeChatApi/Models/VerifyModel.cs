using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatApi.Models
{
    /// <summary>
    /// 验证消息真实性
    /// </summary>
    public class VerifyModel
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 随机数字
        /// </summary>
        public string nonce { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string echostr { get; set; }
    }
}