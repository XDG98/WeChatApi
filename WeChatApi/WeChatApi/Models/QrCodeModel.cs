using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatApi.Models
{
    /// <summary>
    /// 二维码
    /// </summary>
    public class QrCodeModel
    {
        /// <summary>
        /// 该二维码有效时间，以秒为单位。 最大不超过2592000（即30天）
        /// 此字段如果不填，则默认有效期为30秒。
        /// </summary>
        public int expire_seconds { get; set; }
        /// <summary>
        /// 二维码类型，QR_SCENE为临时的整型参数值，QR_STR_SCENE为临时的字符串参数值
        /// QR_LIMIT_SCENE为永久的整型参数值，QR_LIMIT_STR_SCENE为永久的字符串参数值
        /// </summary>
        public string action_name { get; set; }
        /// <summary>
        /// 二维码详细信息
        /// </summary>
        public string action_info { get; set; }
        /// <summary>
        /// 场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）
        /// </summary>
        public int scene_id { get; set; }
        /// <summary>
        /// 场景值ID（字符串形式的ID），字符串类型，长度限制为1到64
        /// </summary>
        public string scene_str { get; set; }


        #region 请求ticket响应结果参数

        /// <summary>
        /// 获取的二维码ticket，凭借此ticket可以在有效时间内换取二维码。
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 该二维码有效时间，以秒为单位。 最大不超过2592000（即30天）。
        /// </summary>
        //public int expire_seconds { get; set; }
        /// <summary>
        /// 二维码图片解析后的地址，开发者可根据该地址自行生成需要的二维码图片
        /// </summary>
        public string url { get; set; }

        #endregion

    }
}