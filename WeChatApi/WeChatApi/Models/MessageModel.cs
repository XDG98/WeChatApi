using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatApi.Models
{
    #region 接口调用参数  请求参数

    /// <summary>
    /// 消息Model
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// 必须  调用接口凭证
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 必须  普通用户openid
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 必须  消息类型，文本为text，图片为image，语音为voice，视频消息为video，
        /// 音乐消息为music，图文消息（点击跳转到外链）为news，卡券为wxcard，
        /// 小程序为miniprogrampage，图文消息（点击跳转到消息页面）为mpnews
        /// </summary>
        public string msgtype { get; set; }
        /// <summary>
        /// 必须  文本消息内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 必须  发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 必须  缩略图/小程序卡片图片的媒体ID，小程序卡片图片建议大小为520*416
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 可无  图文消息/视频消息/音乐消息/小程序卡片的标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 可无  图文消息/视频消息/音乐消息的描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 必须  音乐链接
        /// </summary>
        public string musicurl { get; set; }
        /// <summary>
        /// 必须  高品质音乐链接，wifi环境优先使用该链接播放音乐
        /// </summary>
        public string hqmusicurl { get; set; }
        /// <summary>
        /// 可无  图文消息被点击后跳转的链接
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 可无  图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
        /// </summary>
        public string picurl { get; set; }
        /// <summary>
        /// 必须  小程序的appid，要求小程序的appid需要与公众号有关联关系
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 必须  小程序的页面路径，跟app.json对齐，支持参数，比如pages/index/index?foo=bar
        /// </summary>
        public string pagepath { get; set; }
    }

    #endregion
}