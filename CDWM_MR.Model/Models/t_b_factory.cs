using SqlSugar;

namespace CDWM_MR.Model.Models
{
    public class t_b_factory
    {
        /// <summary>
        /// 厂商代码
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int16 bftid { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public System.String factoryname { get; set; }
    }
}
