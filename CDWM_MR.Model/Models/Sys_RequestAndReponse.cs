using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 每一次请求和响应信息
    /// </summary>
    public class Sys_RequestAndReponse
    {

        /// <summary>
        /// 请求响应的地址信息
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string UrlInfo { get; set; }
    }
}
