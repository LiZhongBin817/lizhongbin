using SqlSugar;

namespace CDWM_MR.Model.Models
{
    public class t_b_watermetertype
    {
        /// <summary>
        /// 分类编码
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int16 bmtid { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public System.String metertypename { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public System.Int16 bmttype { get; set; }
    }
}
