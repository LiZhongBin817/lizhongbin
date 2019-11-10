using SqlSugar;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 23. 片区信息表 t_b_regions（相当于本系统中的区域表）
    /// </summary>
    public class t_b_regions
    {
        /// <summary>
        /// 23. 片区信息表 t_b_regions
        /// </summary>
        public t_b_regions()
        {
        }

        /// <summary>
        /// 片区编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String regionno { get; set; }

        /// <summary>
        /// 片区名称
        /// </summary>
        public System.String regionname { get; set; }

        /// <summary>
        /// 状态(1:可用;2:注销)
        /// </summary>
        public System.Int16? regionstate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime createtime { get; set; }
    }
}
