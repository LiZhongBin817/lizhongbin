using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Model
{
    /// <summary>
    /// 表格数据，支持分页
    /// </summary>
    public class TableModel<T>
    {
        /// <summary>
        /// 返回编码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 记录总数
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 返回数据集
        /// </summary>
        public T data { get; set; }
    }
}
