using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatApi.Models
{
    /// <summary>
    /// 请求状态
    /// </summary>
    public class StatusModel
    {
        /// <summary>
        /// 状态代码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 状态信息
        /// </summary>
        public string errmsg { get; set; }
    }
}