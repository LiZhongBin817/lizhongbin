using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 区域管理
    /// </summary>
    [Route("api/RegionManage")]
    [EnableCors("LimitRequests")]
    [AllowAnonymous]
    public class RegionManageController : Controller
    {
        private readonly It_b_regionsServices _t_b_regionsServices;
        private readonly It_b_areasServices _t_b_areasServices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="It_b_regionsService"></param>
        /// <param name="It_b_areasService"></param>
        public RegionManageController(It_b_regionsServices It_b_regionsService, It_b_areasServices It_b_areasService)
        {
            _t_b_regionsServices = It_b_regionsService;
            _t_b_areasServices = It_b_areasService;
        }
        /// <summary>
        /// 显示区域信息
        /// </summary>
        /// <param name="regionname"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("regionsShow")]
        public async Task<TableModel<object>> regionsShow(string regionname, int page = 1, int limit = 5)
        {
            PageModel<t_b_regions> showdate = new PageModel<t_b_regions>();
            Expression<Func<t_b_regions, bool>> wherelambda = c => c.regionstate == 1;
            if (!string.IsNullOrEmpty(regionname))
            {
                wherelambda = PredicateExtensions.And<t_b_regions>(wherelambda, c => c.regionname.Contains(regionname));
            }
            showdate = await _t_b_regionsServices.OQueryPage(wherelambda, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                data = showdate.data,
                count = showdate.dataCount
            };
        }
        /// <summary>
        /// 点击查看时展示的小区信息
        /// </summary>
        /// <param name="regionno"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("areaShow")]
        public async Task<TableModel<object>> areaShow(string regionno, int page = 1, int limit = 5)
        {
            PageModel<t_b_areas> showdata = new PageModel<t_b_areas>();
            Expression<Func<t_b_areas, bool>> wherelambda = c => c.areastate == 1;
            if (!string.IsNullOrEmpty(regionno))
            {
                wherelambda = PredicateExtensions.And<t_b_areas>(wherelambda, c => c.regionno == regionno);
            }
            showdata = await _t_b_areasServices.OQueryPage(wherelambda, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                data = showdata.data,
                count = showdata.dataCount
            };
        }
        /// <summary>
        /// 编辑区域信息
        /// </summary>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editRegion")]
        public async Task<TableModel<object>> editRegion(string JsonData)
        {
            t_b_regions EditObj = Common.Helper.JsonHelper.GetObject<t_b_regions>(JsonData);
            await _t_b_regionsServices.OUpdate(EditObj);
            return new TableModel<object>()
            {
                msg = "ok",
                code = 0,
                data = null,
                count = 0
            };
        }
        /// <summary>
        /// 修改小区信息
        /// </summary>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editArea")]
        public async Task<MessageModel<object>> editArea(string JsonData)
        {
            t_b_areas EditObj = Common.Helper.JsonHelper.GetObject<t_b_areas>(JsonData);
            await _t_b_areasServices.OUpdate(EditObj);
            return new MessageModel<object>()
            {
                msg = "ok",
                code = 0,
                data = null
            };
        }
        /// <summary>
        /// 删除一条区域信息
        /// </summary>
        /// <param name="regionno"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deleteRegion")]
        public async Task<TableModel<object>> deleteRegion(string regionno)
        {
            await _t_b_regionsServices.OUpdate(c => new t_b_regions
            {
                regionstate = 2
            }, c => c.regionno == regionno);
            return new TableModel<object>()
            {
                msg = "ok",
                code = 0,
                data = null,
                count = 0
            };
        }
        /// <summary>
        /// 删除一条小区信息
        /// </summary>
        /// <param name="areano"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deleteArea")]
        public async Task<TableModel<object>> deleteArea(string areano)
        {
            await _t_b_areasServices.OUpdate(c => new t_b_areas
            {
                areastate = 2
            }, c => c.areano == areano);
            return new TableModel<object>()
            {
                msg = "ok",
                code = 0,
                data = null,
                count = 0
            };
        }
        /// <summary>
        /// 添加一条区域信息
        /// </summary>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addRegion")]
        public async Task<MessageModel<object>> addRegion(string JsonData)
        {
            t_b_regions AddObj = Common.Helper.JsonHelper.GetObject<t_b_regions>(JsonData);
            var addid = await _t_b_regionsServices.OQuery(c => true,s => new t_b_regions() { 
            }, "regionno desc",1);
            AddObj.createtime = DateTime.Now;
            if (addid == null || addid.Count <= 0)
            {
                AddObj.regionno = "1000001";
            }
            else 
            {
                var tempnum = Convert.ToInt32(addid[0].regionno);
                tempnum += 1;
                AddObj.regionno = tempnum.ObjToString();
            }
            await _t_b_regionsServices.OAdd(AddObj);
            return new MessageModel<object>()
            {
                msg = "ok",
                code = 0,
                data = null
            };
        }
        /// <summary>
        /// 添加一条小区信息    
        /// </summary>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addArea")]
        public async Task<TableModel<object>> addArea(string JsonData)
        {
            t_b_areas AddObj = Common.Helper.JsonHelper.GetObject<t_b_areas>(JsonData);
            AddObj.createtime = DateTime.Now;
            await _t_b_areasServices.OAdd(AddObj);
            return new TableModel<object>()
            {
                msg = "ok",
                code = 0,
                data = null,
                count = 0
            };
        }
    }
}
