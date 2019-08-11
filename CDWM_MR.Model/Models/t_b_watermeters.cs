using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// 38. 水表基本信息表 t_b_watermeters（档案管理模块核心表）
    /// </summary>
    public class t_b_watermeters
    {
        /// <summary>
        /// 38. 水表基本信息表 t_b_watermeters
        /// </summary>
        public t_b_watermeters()
        {
        }

        /// <summary>
        /// 水表自动表号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String meternum { get; set; }

        /// <summary>
        /// 水表名称123
        /// </summary>
        public System.String metername { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        public System.String bwmid { get; set; }

        /// <summary>
        /// 电子表号（机械水表的电子表号与自动表号相同）
        /// </summary>
        public System.String electronno { get; set; }

        /// <summary>
        /// 抄表册编号（t_c_readmeterbook::bookno）
        /// </summary>
        public System.String bookno { get; set; }

        /// <summary>
        /// 抄表顺序号
        /// </summary>
        public System.Int32? cbseq { get; set; }

        /// <summary>
        /// 铅封号
        /// </summary>
        public System.String sealnum { get; set; }

        /// <summary>
        /// 子表号(卡表使用)
        /// </summary>
        public System.Int32? subseq { get; set; }

        /// <summary>
        /// 安装地址
        /// </summary>
        public System.String address { get; set; }

        /// <summary>
        /// 所属小区(t_b_areas::areano)
        /// </summary>
        public System.String areano { get; set; }

        /// <summary>
        /// 楼栋号
        /// </summary>
        public System.String buildno { get; set; }

        /// <summary>
        /// 单元号
        /// </summary>
        public System.String unitno { get; set; }

        /// <summary>
        /// 房间号
        /// </summary>
        public System.String roomno { get; set; }

        /// <summary>
        /// 水表型号(t_b_watermodel::bmlid)
        /// </summary>
        public System.Int16? metermodel { get; set; }

        /// <summary>
        /// 水表类型（t_b_watermetertype::bmtid）
        /// </summary>
        public System.Int16? metertype { get; set; }

        /// <summary>
        /// 生产厂商(t_b_factory::bftid)
        /// </summary>
        public System.Int16? factory { get; set; }

        /// <summary>
        /// 安装位置(t_b_installpos::bipid)
        /// </summary>
        public System.Int16? installpos { get; set; }

        /// <summary>
        /// 初始读数（初始表码）
        /// </summary>
        public System.Int32? bwcode { get; set; }

        /// <summary>
        /// 预装金额
        /// </summary>
        public System.Decimal? prewaters { get; set; }

        /// <summary>
        /// 囤积限额值（卡表有效）
        /// </summary>
        public System.Int32? maxwaters { get; set; }

        /// <summary>
        /// 报警值（卡表有效）
        /// </summary>
        public System.Int32? warnwaters { get; set; }

        /// <summary>
        /// 赊欠限额值（卡表有效）
        /// </summary>
        public System.Int32? creditlimit { get; set; }

        /// <summary>
        /// 累计购水次数（卡表有效）
        /// </summary>
        public System.Int32? totaltime { get; set; }

        /// <summary>
        /// 累计购水金额（卡表有效）
        /// </summary>
        public System.Decimal? totalmoney { get; set; }

        /// <summary>
        /// 本次购水次数（卡表有效）
        /// </summary>
        public System.Int32? chargetime { get; set; }

        /// <summary>
        /// 本次购水金额（卡表有效）
        /// </summary>
        public System.Decimal? chargemoney { get; set; }

        /// <summary>
        /// simm卡号（t_c_simcard:: cscid）（物联网水表有效）
        /// </summary>
        public System.String simm { get; set; }

        /// <summary>
        /// 水价方案类型(1：定额;2：阶梯;3：混合；4：二部制水价)
        /// </summary>
        public System.Int16? priceplantype { get; set; }

        /// <summary>
        /// 水价方案(t_b_ladderpice,t_b_mixpriceplan,t_morestepplan)
        /// </summary>
        public System.String priceplan { get; set; }

        /// <summary>
        /// 特殊计量规则号（0:按实际用量）（t_b_measurerule::bmrid）
        /// </summary>
        public System.Int32? measurerule { get; set; }

        /// <summary>
        /// 特殊计费规则号（0:按实际金额）(t_b_chargingrule::bcrid)
        /// </summary>
        public System.Int32? chargingrule { get; set; }

        /// <summary>
        /// 初装时间
        /// </summary>
        public System.DateTime? installtime { get; set; }

        /// <summary>
        /// 审核状态（0：废除；1：可编辑；2：发起待审；3：审核通过；4：批准生效）
        /// </summary>
        public System.Int16? approvestate { get; set; }

        /// <summary>
        /// 状态(0:未使用1:正常2:暂停用水3:注销)
        /// </summary>
        public System.Int16? meterstate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? createtime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public System.String createby { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public System.DateTime? lastmodifytime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public System.String lastmodifyby { get; set; }

        /// <summary>
        /// 水户自动帐号（t_b_users:: autoaccount）
        /// </summary>
        public System.String autoaccount { get; set; }

        /// <summary>
        /// 帐号(水司分配的帐号)
        /// </summary>
        public System.String account { get; set; }

        /// <summary>
        /// 收费标志（0：不需要收费1：需要收费）
        /// </summary>
        public System.Int32 feestate { get; set; }

        /// <summary>
        /// 上级水表信息（总表）(0：表示无上级水表信息)
        /// </summary>
        public System.String fathermeternum { get; set; }

        /// <summary>
        /// 虚拟总表标识(0:实际水表1：虚拟水表)
        /// </summary>
        public System.Int32? fictitousmeteridt { get; set; }

        /// <summary>
        /// 报装批号
        /// </summary>
        public System.String batchno { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public System.String remark { get; set; }

        /// <summary>
        /// 使用标记 (0:否；1:是)
        /// </summary>
        public System.SByte? delflag { get; set; }

        /// <summary>
        /// NB表是否注册到iot平台(1:是；2：否)
        /// </summary>
        public System.Int32? isreg { get; set; }

        /// <summary>
        /// 更换水表时间
        /// </summary>
        public System.DateTime? updatemetertime { get; set; }
    }
}
