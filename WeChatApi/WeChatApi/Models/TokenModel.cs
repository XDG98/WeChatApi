using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatApi.Models
{
    /// <summary>
    /// 凭证
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// 状态代码
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 状态信息
        /// </summary>
        public int expires_in { get; set; }
    }
}