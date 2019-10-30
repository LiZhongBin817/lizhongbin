using SqlSugar;

namespace CDWM_MR.Model.Models
{
    public class t_b_installpos
    {
        /// <summary>
        /// 位置编码
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int16 bipid { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public System.String posname { get; set; }
    }
}
