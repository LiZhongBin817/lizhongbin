using SqlSugar;

namespace CDWM_MR.Model.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_mr_datainfo
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_mr_datainfo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ID { get; set; }

        /// <summary>
        /// 自动帐号(系统自动生成)(关联水表用户信息)
        /// </summary>
        public System.String autoaccount { get; set; }

        /// <summary>
        /// 水表编号,（t_b_watermeters::meternum）
        /// </summary>
        public System.String meternum { get; set; }

        /// <summary>
        /// 抄表员id（mr_b_reader：:id）
        /// </summary>
        public System.Int32 readerid { get; set; }

        /// <summary>
        /// 人为抄表数据
        /// </summary>
        public System.Decimal? inputdata { get; set; }

        /// <summary>
        /// 图像识别抄表数据Optical Choractor Recognittion光学字符识别
        /// </summary>
        public System.Decimal? ocrdata { get; set; }

        /// <summary>
        /// 上传数据时间
        /// </summary>
        public System.DateTime? uploadtime { get; set; }

        /// <summary>
        /// 上传的GIS信息
        /// </summary>
        public System.String uploadgisplace { get; set; }

        /// <summary>
        /// 抄表状态(0--默认;1--实抄;2--估抄;3--异常),根据gis位置进行判断
        /// </summary>
        public System.Int32 readtype { get; set; }

        /// <summary>
        /// 任务单id(来源于mr_taskinfo)
        /// </summary>
        public System.Int32 taskid { get; set; }

        /// <summary>
        /// 水表状态,从数据字典中读取字符型
        /// </summary>
        public System.Int32? meterstatus { get; set; }

        /// <summary>
        /// 状态0--未审;1--已审;2--异常
        /// </summary>
        public System.Int32 recheckstatus { get; set; }

        /// <summary>
        /// 复审读数(抄表数据审核数据)
        /// </summary>
        public System.Decimal? readcheckdata { get; set; }

        /// <summary>
        /// 抄表数据审核异常原因，冗余
        /// </summary>
        public System.String recheckresult { get; set; }

        /// <summary>
        /// 任务账期201909冗余
        /// </summary>
        public System.String taskperiodname { get; set; }

        /// <summary>
        /// 0--未抄;1--已抄回;2--已识别;3--已复审;4--已结转;5--已归档;6--其余未定义
        /// </summary>
        public System.Int32 readstatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String telephone { get; set; }

        /// <summary>
        /// 家庭住址
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
        /// 水表名称
        /// </summary>
        public System.String metername { get; set; }

        /// <summary>
        /// 抄表册编号（t_c_readmeterbook::bookno）
        /// </summary>
        public System.String bookno { get; set; }

        /// <summary>
        /// 抄表册名称
        /// </summary>
        public System.String bookname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String mrreadername { get; set; }

        /// <summary>
        /// 小区名称
        /// </summary>
        public System.String areaname { get; set; }

        /// <summary>
        /// 片区名称
        /// </summary>
        public System.String regionname { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public System.DateTime? checksuccesstime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? checktime { get; set; }

        /// <summary>
        /// 审核人（sys_userinfo::ID,0为系统自动）
        /// </summary>
        public System.String checkor { get; set; }

        /// <summary>
        /// 状态0--通过;1--不通过
        /// </summary>
        public System.Int32? rtrecheckstatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String FUserName { get; set; }

        /// <summary>
        /// 状态0--未结转;1--正常;2--异常
        /// </summary>
        public System.Int32? carrystatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? carryime { get; set; }
    }
}
