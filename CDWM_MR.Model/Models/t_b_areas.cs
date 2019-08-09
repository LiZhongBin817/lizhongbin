using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 24. 小区信息表 t_b_areas
    /// </summary>
    public class t_b_areas
    {
        /// <summary>
        /// 24. 小区信息表 t_b_areas
        /// </summary>
        public t_b_areas()
        {
        }

        /// <summary>
        /// 小区编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String areano { get; set; }

        /// <summary>
        /// 小区名称
        /// </summary>
        public System.String areaname { get; set; }

        /// <summary>
        /// 所属片区编号
        /// </summary>
        public System.String regionno { get; set; }

        /// <summary>
        /// 状态(1:可用;2:注销)
        /// </summary>
        public System.Int16? areastate { get; set; }
    }
}
