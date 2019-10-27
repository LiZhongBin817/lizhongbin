using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 抄表数据信息下载
    /// </summary>
    public class v_downloaddatainfo
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_downloaddatainfo()
        {
        }

        /// <summary>
        /// 抄表数据id
        /// </summary>
        public System.Int32 datainfoid { get; set; }

        /// <summary>
        /// 抄表册ID,来源于mr_b_bookinfo::id
        /// </summary>
        public System.Int32? bookid { get; set; }

        /// <summary>
        /// 任务单id
        /// </summary>
        public int taskid { get; set; }

        /// <summary>
        /// 抄表册编号
        /// </summary>
        public string bookno { get; set; }

        /// <summary>
        /// 自动帐号(系统自动生成)(关联水表用户信息)
        /// </summary>
        public System.String autoaccount { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public System.String username { get; set; }

        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        public System.String meternum { get; set; }

        /// <summary>
        /// 上期抄表时间
        /// </summary>
        public System.DateTime? lastreadtime { get; set; }

        /// <summary>
        /// 上期止码
        /// </summary>
        public System.Decimal? lastendnum { get; set; }

        /// <summary>
        /// 三月平均用水量
        /// </summary>
        public System.Decimal? avenum { get; set; }

        /// <summary>
        /// 上期用水量
        /// </summary>
        public System.Decimal? lastwaternum { get; set; }

        /// <summary>
        /// 帐户余额
        /// </summary>
        public System.Decimal? balance { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public System.String telephone { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public System.String regionname { get; set; }

        /// <summary>
        /// 小区名称
        /// </summary>
        public System.String areaname { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get; set; }

        /// <summary>
        /// 用水性质名称
        /// </summary>
        public System.String naturename { get; set; }

        /// <summary>
        /// 抄表员名称
        /// </summary>
        public System.String mrreadername { get; set; }

        /// <summary>
        /// 表册内水表顺序
        /// </summary>
        public System.Int32? meterseq { get; set; }

        /// <summary>
        /// 抄表信息
        /// </summary>
        public System.String readinfo { get; set; }

        /// <summary>
        /// 水表类型名称
        /// </summary>
        public System.String metertypename { get; set; }

        /// <summary>
        /// 安装地址
        /// </summary>
        public System.String installaddress { get; set; }

        /// <summary>
        /// GIS位置(190808新增)
        /// </summary>
        public System.String GISPlace { get; set; }

        /// <summary>
        /// 口径(190808新增)
        /// </summary>
        public System.String caliber { get; set; }

        /// <summary>
        /// 上期抄表异常
        /// </summary>
        public System.String lastmeterstatus { get; set; }
    }
}
