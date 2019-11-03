//用户管理界面导入用户数据时创建
//李黎东
//2019/10/24
namespace CDWM_MR.Model
{
    public class UserFromExcel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public System.String account { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public System.String username { get; set; }
        /// <summary>
        /// 用户地址
        /// </summary>
        public System.String address { get; set; }
        /// <summary>
        /// 所属小区
        /// </summary>
        public System.String areano { get; set; }
        /// <summary>
        /// 用水类型
        /// </summary>
        public System.String usemetertype { get; set; }
        /// <summary>
        /// 用户电话
        /// </summary>
        public System.String telephone { get; set; }
        /// <summary>
        /// GIS位置
        /// </summary>
        public System.String GISPlace { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public System.String sex { get; set; }
    }
}
