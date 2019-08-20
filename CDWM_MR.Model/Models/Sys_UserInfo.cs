using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class sys_userinfo : BaseModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [SugarColumn(Length = 80, IsNullable = false)]
        public string FUserNumber { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string FUserName { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [SugarColumn(Length = 60, IsNullable = true)]
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码,默认密码为12345
        /// </summary>
        [SugarColumn(Length = 60, IsNullable = true)]
        public string LoginPassWord { get; set; } = "12345";

        /// <summary>
        /// 真实姓名
        /// </summary>
        [SugarColumn(Length = 60, IsNullable = true)]
        public string RealName { get; set; }

        /// <summary>
        /// 性别，默认为男
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short Sex { get; set; } = 0;

        /// <summary>
        /// 手机号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 32)]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 200)]
        public string Adress { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [SugarColumn(IsNullable = true,Length = 50)]
        public string Email { get; set; }

        /// <summary>
        /// 状态0为正常，1为作废
        /// </summary>
        [SugarColumn(Length = 60, IsNullable = true)]
        public int UseStatus { get; set; } = 0;

        /// <summary>
        /// 删除标记,默认值为0--正常
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short DeleteFlag { get; set; } = 0;

        /// <summary>
        /// 登陆用户类型0--超级管理员;1--管理员;2--普通用户(默认)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short UserType { get; set; } = 2;

        /// <summary>
        ///最后登录时间 
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime LastLoginTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 分配的角色
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<string> RoleNames { get; set; }
    }
}
