using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 用户管理(档案管理)
    /// </summary>
    public class WatermeterUserManageController : Controller
    {
        #region  相关变量
        private readonly Iv_wateruserinfoServices _v_wateruserinfoServices;
        private readonly It_b_regionsServices _t_b_regionsServices;
        private readonly It_b_areasServices _t_b_areasServices;
        private readonly It_b_usersServices _t_b_usersServices;
        private readonly It_b_watermetersServices _t_b_watermetersServices;
        private readonly Iv_watermeterinfoServices _v_watermeterinfoServices;
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v_wateruserinfoServices"></param>
        /// <param name="t_b_regionsServices"></param>
        /// <param name="t_b_areasServices"></param>
        /// <param name="t_b_usersServices"></param>
        /// <param name="t_b_watermetersServices"></param>
        /// <param name="v_watermeterinfoServices"></param>
        public WatermeterUserManageController(Iv_wateruserinfoServices v_wateruserinfoServices, It_b_regionsServices t_b_regionsServices, It_b_areasServices t_b_areasServices, It_b_usersServices t_b_usersServices, It_b_watermetersServices t_b_watermetersServices, Iv_watermeterinfoServices v_watermeterinfoServices)
        {
            _v_wateruserinfoServices = v_wateruserinfoServices;
            _t_b_regionsServices = t_b_regionsServices;
            _t_b_areasServices = t_b_areasServices;
            _t_b_usersServices = t_b_usersServices;
            _t_b_watermetersServices = t_b_watermetersServices;
            _v_watermeterinfoServices = v_watermeterinfoServices;
        }
        #region  用户管理

        #region  显示
        /// <summary>
        /// 显示用户管理
        /// </summary>
        /// <param name="account">用户编号</param>
        /// <param name="username">用户名称</param>
        /// <param name="meternum">表号</param>
        /// <param name="optname">抄表员</param>
        /// <param name="bookno">抄表册号</param>
        /// <param name="regionplace">所属区域</param>
        /// <param name="areaname">所属小区</param>
        /// <param name="page">页号</param>
        /// <param name="limit">页面大小</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowWaterUserinfo")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<List<v_wateruserinfo>>> ShowWaterUserinfo(string account, string username, string meternum, string optname, string bookno, string regionplace, string areaname, int page = 1, int limit = 5)
        {
            PageModel<v_wateruserinfo> showdate = new PageModel<v_wateruserinfo>();
            #region  lambda拼接式
            Expression<Func<v_wateruserinfo, bool>> wherelambda = c => true;
            if (!string.IsNullOrEmpty(account))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.account.Contains(account));
            }
            if (!string.IsNullOrEmpty(username))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.username.Contains(username));
            }
            if (!string.IsNullOrEmpty(meternum))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.meternum.Contains(meternum));
            }
            if (!string.IsNullOrEmpty(optname))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.optname.Contains(optname));
            }
            if (!string.IsNullOrEmpty(bookno))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.bookno.Contains(bookno));
            }
            if (!string.IsNullOrEmpty(regionplace))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.regionplace.Contains(regionplace));
            }
            if (!string.IsNullOrEmpty(areaname))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.areaname.Contains(areaname));
            }
            #endregion
            showdate = await _v_wateruserinfoServices.QueryPage(wherelambda, page, limit, "");
            return new TableModel<List<v_wateruserinfo>>()
            {
                code = 0,
                msg = "ok",
                count = showdate.dataCount,
                data = showdate.data
            };
        }
        #endregion

        #region  编辑

        #region  编辑用户信息
        /// <summary>
        /// 给编辑界面传区域信息和历史信息
        /// </summary>
        /// <param name="account">用户编号</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ShowEditRegionDate")]
        [AllowAnonymous]
        public async Task<TableModel<object>> ShowEditRegionDate(string account)
        {
            List<object> list = new List<object>();
            //显示所有区域
            List<t_b_regions> regionlist = await _t_b_regionsServices.OQuery(c => true);
            //显示用户历史表单记录
            List<v_watermeterinfo> watermeterlist = await _v_watermeterinfoServices.Query(c=>c.account== account);
            list.Add(regionlist);
            list.Add(watermeterlist);
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = watermeterlist.Count(),//若为0，则不显示历史表，并且显示新增按钮，不为0，不显示新增按钮
                data = list
            };
        }
        /// <summary>
        /// 给编辑界面传小区信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ShowEditAreasDate")]
        [AllowAnonymous]
        public async Task<TableModel<object>> ShowEditAreasDate(string regionno)
        {
            List<t_b_areas> areas = await _t_b_areasServices.OQuery(c=>c.regionno==regionno);
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = 2,
                data = areas
            };
        }
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        ///  <param name="JsonDate">户名+电话+小区编号+家庭住址</param>
        [HttpPost]
        [Route("EditUserInfo")]
        [AllowAnonymous]
        public async Task<TableModel<object>> EditUserInfo(string JsonDate)
        {
            t_b_users Edit = Common.Helper.JsonHelper.GetObject<t_b_users>(JsonDate);
            await _t_b_usersServices.OUpdate(Edit) ;
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = 0,
                data = null
            };
        }
        #endregion

        #region  编辑水表信息
        /// <summary>
        /// 编辑水表信息
        /// </summary>
        /// <param name="JsonDate">水表信息JsonDate</param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditWaterMater")]
        [AllowAnonymous]
        public async Task<TableModel<object>> EditWaterMater(string JsonDate)
        {
            t_b_watermeters Edit = Common.Helper.JsonHelper.GetObject<t_b_watermeters>(JsonDate);
            await _t_b_watermetersServices.OUpdate(Edit);
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = 0,
                data = null
            };
        }
        #endregion
        #endregion

        #region  新增

        #region  新增水表

        /// <summary>
        /// 在新增水表界面打开时给水表序列号赋值
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("showmeternum")]
        [AllowAnonymous]
        public async Task<TableModel<object>> showmeternum()
        {
            //取到水表最后一条数据
            Expression<Func<t_b_watermeters, object>> expression = c => new
            {
                c.account
            };
            List<t_b_watermeters> last = await _t_b_watermetersServices.OQuery(c => true);
            //生成自动表号
            string lastnumber = last[last.Count() - 1].meternum;
            string meternum = Convert.ToString(Convert.ToDouble(lastnumber) + 1);
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = 1,
                data = meternum
            };
        }

        /// <summary>
        /// 新增水表（只有该用户没有历史水表时才显示该功能）
        /// </summary>
        /// <param name="JsonDate">传来的用户信息</param>
        /// <param name="autoaccount">用户自动编号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddWaterMeter")]
        [AllowAnonymous]
        public async Task<TableModel<object>> AddWaterMeter(string JsonDate, string autoaccount)
        {
            //转换对象
            t_b_watermeters adduwatermeter = Common.Helper.JsonHelper.GetObject<t_b_watermeters>(JsonDate);
            adduwatermeter.meterstate = 0;//状态为未使用
            //添加
            await _t_b_watermetersServices.OAdd(adduwatermeter);
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = 1,
                data = null
            };
        }

        #endregion

        #region  新增用户
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="JsonDate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Adduser")]
        [AllowAnonymous]
        public async Task<TableModel<object>> Adduser(string JsonDate)
        {
            t_b_users adduser = Common.Helper.JsonHelper.GetObject<t_b_users>(JsonDate);
            await _t_b_usersServices.OAdd(adduser);
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = 1,
                data = null
            };
        }

        #endregion

        #endregion

        #region 换表
        /// <summary>
        /// 给换表界面显示数据
        /// </summary>
        /// <param name="account">用户编号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowChangemeter")]
        [AllowAnonymous]
        public async Task<TableModel<object>> ShowChangemeter(string account)
        {
            List<v_wateruserinfo> waterinfolist = await _v_wateruserinfoServices.Query(c => c.account == account);
            //取到所有编号
            List<string> NumberList = new List<string>();
            for (int i=0;i< waterinfolist.Count();i++)
            {
                NumberList.Add(waterinfolist[i].meternum);
            }
            List<t_b_watermeters> watermaterlist =await _t_b_watermetersServices.OQuery(c => NumberList.Contains(c.meternum));
            List<t_b_watermeters> showwatermater = new List<t_b_watermeters>();
            //添加旧表
            showwatermater.Add(watermaterlist.Find(c => c.meterstate == 1));//刚显示 的时候表还没换，所以旧表的状态显示正常
            showwatermater.Add(watermaterlist[watermaterlist.Count() - 1]);//新表为最后一个表
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = showwatermater.Count(),
                data = showwatermater
            };
        }
        /// <summary>
        /// 换表
        /// </summary>
        /// <param name="Oldmeternum">旧表编号</param>
        /// <param name="Newmeternum">新表编号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Changemeter")]
        [AllowAnonymous]
        public async Task<TableModel<object>> Changemeter(string Oldmeternum, string Newmeternum)
        {
            //旧表状态变成 未使用
            await _t_b_watermetersServices.OUpdate(c => new t_b_watermeters
            {
                meterstate = 0//未使用
            }, c => c.meternum == Oldmeternum);
            //新表状态变成正常
            await _t_b_watermetersServices.OUpdate(c => new t_b_watermeters
            {
                meterstate = 1//正常
            }, c => c.meternum == Newmeternum);
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = 1,
                data = null
            };
        }
        #endregion

        #endregion
    }
}
