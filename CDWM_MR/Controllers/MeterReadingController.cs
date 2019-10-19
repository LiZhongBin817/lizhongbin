using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 抄表管理(档案管理)
    /// </summary>
    public class MeterReadingController : Controller
    {
        #region  相关变量
        readonly It_b_areasServices _t_b_areasServices;
        readonly It_b_regionsServices _t_b_regionsServices;
        #endregion

        /// <summary>
        /// 构造函数(依赖注入)
        /// </summary>
        /// <param name="t_b_areasServices"></param>
        /// <param name="t_b_regionsServices"></param>
        public MeterReadingController(It_b_areasServices t_b_areasServices, It_b_regionsServices t_b_regionsServices)
        {
            _t_b_areasServices = t_b_areasServices;
            _t_b_regionsServices = t_b_regionsServices;
        }

        #region  区域管理

        #region 显示
        /// <summary>
        /// 显示区域管理
        /// </summary>
        /// <param name="regionname">片区编号</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ShowregionDate")]
        [AllowAnonymous]//允许所有都访问
        public async Task<TableModel<List<t_b_regions>>> ShowregionDate(string regionname, int page = 1, int limit = 5)
        {
            Expression<Func<t_b_regions, bool>> wherelambda = c => c.regionstate == 1;
            #region lambda拼接式
            if (!string.IsNullOrEmpty(regionname))
            {
                wherelambda = PredicateExtensions.And<t_b_regions>(wherelambda, c => c.regionname == regionname);
            }
            #endregion
            PageModel<t_b_regions> regionlist = await _t_b_regionsServices.OQueryPage(wherelambda, page, limit, "");
            return new TableModel<List<t_b_regions>>
            {
                code = 0,
                msg = "ok",
                count = regionlist.dataCount,
                data = regionlist.data
            };
        }
        /// <summary>
        /// 显示小区管理
        /// </summary>
        /// <param name="regionno">片区编号</param>
        /// <param name="page">当前页面号</param>
        /// <param name="limit">页面大小</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ShowareasDate")]
        [AllowAnonymous]//允许所有都访问
        public async Task<TableModel<PageModel<t_b_areas>>> ShowareasDate(string regionno, int page = 1, int limit = 5)
        {
            Expression<Func<t_b_areas, bool>> wherelambda = c => c.areastate == 1 && c.regionno == regionno;
            PageModel<t_b_areas> areaslist = await _t_b_areasServices.OQueryPage(wherelambda, page, limit, "");
            return new TableModel<PageModel<t_b_areas>>
            {
                code = 0,
                msg = "ok",
                count = areaslist.dataCount,
                data = areaslist
            };
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除区域管理
        /// </summary>
        /// <param name="regionno">所属片区编号</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Deleteregion")]
        [AllowAnonymous]//允许所有都访问
        public async Task<TableModel<object>> Deleteregion(string regionno)
        {
            await _t_b_regionsServices.OUpdate(c => new t_b_regions
            {
                regionstate = 2
            }, c => c.regionno == regionno);
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                count = 0,
                data = null
            };
        }

        /// <summary>
        /// 删除小区管理
        /// </summary>
        /// <param name="regionno">所属片区编号</param>
        /// <param name="areano">小区编号</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Deleteareas")]
        [AllowAnonymous]//允许所有都访问
        public async Task<TableModel<object>> Deleteareas(string regionno, string areano)
        {
            await _t_b_areasServices.OUpdate(c => new t_b_areas
            {
                areastate = 2
            }, c => c.regionno == regionno && c.areano == areano);
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                count = 0,
                data = null
            };
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑区域管理
        /// </summary>
        /// <param name="regionno">所属片区编号</param>
        /// <param name="regionname">片区名称</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Editregion")]
        [AllowAnonymous]//允许所有都访问
        public async Task<TableModel<object>> Editregion(string regionno, string regionname)
        {
            await _t_b_regionsServices.OUpdate(c => new t_b_regions
            {
                regionname = regionname
            }, c => c.regionno == regionno);
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                count = 0,
                data = null
            };
        }

        /// <summary>
        /// 编辑小区管理
        /// </summary>
        /// <param name="regionno">所属片区编号</param>
        /// <param name="areano">小区编号</param>
        /// <param name="areaname">小区名称</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Editareas")]
        [AllowAnonymous]//允许所有都访问
        public async Task<TableModel<object>> Editareas(string regionno, string areano, string areaname)
        {
            await _t_b_areasServices.OUpdate(c => new t_b_areas
            {
                areaname = areaname
            }, c => c.regionno == regionno && c.areano == areano);
            return new TableModel<object>
            {
                code = 0,
                msg = "ok",
                count = 0,
                data = null
            };
        }
        #endregion

        #endregion

    }
}
