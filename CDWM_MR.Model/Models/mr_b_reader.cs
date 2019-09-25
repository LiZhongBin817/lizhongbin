using SqlSugar;
using System;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 抄表员
    /// </summary>
    public class mr_b_reader : BaseModel
    {
        /// <summary>
        /// 抄表员编号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50)]
        public string mrreadernumber { get; set; }

        /// <summary>
        /// 抄表员姓名
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string mrreadername { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 20)]
        public string telephone { get; set; }

        /// <summary>
        /// app用户
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string appcount { get; set; }

        /// <summary>
        /// app密码(MD5加密)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 80)]
        public string apppassword { get; set; }

        /// <summary>
        /// 最近抄表时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime nearnrtime { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public System.String address { get; set; }

        /// <summary>
        /// 性别(0：男；1：女)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public short sex { get; set; } = 0;

        /// <summary>
        /// 身份证
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 80)]
        public string idcard { get; set; }

        /// <summary>
        /// 删除状态（0：未删除；1：删除）
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public short deleteflag { get; set; } = 0;

        /// <summary>
        /// 备用字段(存储角色id组合)
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string roles { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime lastlogintime { get; set; }
        public System.Int32 ID { get; set; }

    }
}
