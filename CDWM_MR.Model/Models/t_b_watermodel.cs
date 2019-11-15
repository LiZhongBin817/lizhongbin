using SqlSugar;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class t_b_watermodel
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int16 bmlid { get; set; }
        /// <summary>
        /// 型号名称
        /// </summary>
        public System.String modelname { get; set; }
        /// <summary>
        /// 口径
        /// </summary>
        public System.Int16 dn { get; set; }
        /// <summary>
        /// 分类类别
        /// </summary>
        public System.Int16 bmttype { get; set; }
        /// <summary>
        /// 用水类型
        /// </summary>
        public System.Int16 watertype { get; set; }
        /// <summary>
        /// 最小计量
        /// </summary>
        public System.Int16 minjl { get; set; }
        /// <summary>
        /// 最大计量
        /// </summary>
        public System.Int16 maxjl { get; set; }
        /// <summary>
        /// 最大量程
        /// </summary>
        public System.Int32 maxrange { get; set; }
        /// <summary>
        /// 是否带阀门
        /// </summary>
        public System.Int16 valve { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public System.Int16 bwmstate { get; set; }
    }
}
