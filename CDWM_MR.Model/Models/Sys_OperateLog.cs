using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class Sys_OperateLog : BaseModel
    {

        /// <summary>
        /// 获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 区域控制器名
        /// </summary>
        [SugarColumn(Length = int.MaxValue, IsNullable = true)]
        public string Controller { get; set; }
        /// <summary>
        /// Action名称
        /// </summary>
        [SugarColumn(Length = int.MaxValue, IsNullable = true)]
        public string Action { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        [SugarColumn(Length = int.MaxValue, IsNullable = true)]
        public string IPAddress { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(Length = int.MaxValue, IsNullable = true)]
        public string Description { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? LogTime { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string LoginName { get; set; }

        /// <summary>
        /// 操作的菜单名称
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string MenuName { get; set; }

        /// <summary>
        /// 操作类型(0--登陆;1--退出;2--添加;3--修改;4--删除)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int OperateType { get; set; }
    }
}
