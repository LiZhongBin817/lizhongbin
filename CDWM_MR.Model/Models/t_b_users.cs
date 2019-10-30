using SqlSugar;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 水户信息表 t_b_users(从marketing_sw数据库中生成,相当于)
    /// </summary>
    public class t_b_users
    {
        /// <summary>
        /// 35. 水户信息表 t_b_users
        /// </summary>
        public t_b_users()
        {
        }

        /// <summary>
        /// 自动帐号(系统自动生成)
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String autoaccount { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public System.String account { get; set; }

        /// <summary>
        /// 用水用户名
        /// </summary>
        public System.String username { get; set; }

        /// <summary>
        /// 性别(0：男；1：女)
        /// </summary>
        public System.Int16? sex { get; set; }

        /// <summary>
        /// 标识符
        /// </summary>
        public System.String identification { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public System.String telephone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String telephone1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? peoplies { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public System.String workplace { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        public System.String address { get; set; }

        /// <summary>
        /// 所属小区(t_b_areas::areano)
        /// </summary>
        public System.String areano { get; set; }

        /// <summary>
        /// 楼栋号
        /// </summary>
        public System.String buildno { get; set; }

        /// <summary>
        /// 单元号
        /// </summary>
        public System.String unitno { get; set; }

        /// <summary>
        /// 房间号
        /// </summary>
        public System.String roomno { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public System.String email { get; set; }

        /// <summary>
        /// 帐户余额
        /// </summary>
        public System.Decimal? balance { get; set; }

        /// <summary>
        /// 状态(1：使用中；2：注销)
        /// </summary>
        public System.Int16? accstate { get; set; }

        /// <summary>
        /// 代收机构（0:不代收）(t_b_thirdreceiver:: btrid)
        /// </summary>
        public System.Int32? btrid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? createtime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public System.String createby { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public System.DateTime? lastmodifytime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public System.String lastmodifyby { get; set; }

        /// <summary>
        /// 报装批号
        /// </summary>
        public System.String batchno { get; set; }
        /// <summary>
        /// 抄表员
        /// </summary>
        public System.String mrreader { get; set; }
        /// <summary>
        /// 用水类型
        /// </summary>
        public System.String usemetertype { get; set; }
    }
}
