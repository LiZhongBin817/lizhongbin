using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CDWM_MR.Common.Helper;
using CDWM_MR.Common.HttpContextUser;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 权限管理
    /// </summary>
    [Route("api/OprationManage")]
    [AllowAnonymous]
    [EnableCors("LimitRequests")]
    public class OprationManageController : ControllerBase
    {
        #region 相关变量
        readonly Isys_operationServices _sys_OperationServices;
        readonly Isys_menuServices isys_MenuServices;
        readonly Isys_interface_infoServices isys_Interface_InfoServices;
        readonly IUser _user;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isys_OperationServices"></param>
        /// <param name="_MenuServices"></param>
        /// <param name="_Interface_InfoServices"></param>
        public OprationManageController(Isys_operationServices isys_OperationServices,Isys_menuServices _MenuServices,Isys_interface_infoServices _Interface_InfoServices, IUser user)
        {
            _sys_OperationServices = isys_OperationServices;
            isys_MenuServices = _MenuServices;
            isys_Interface_InfoServices = _Interface_InfoServices;
            _user = user;
        }
        #endregion

        #region 权限管理

        #region 显示数据
        /// <summary>
        /// 显示按钮权限信息
        /// </summary>
        /// <param name="OperationName"></param>
        /// <param name="menuid"></param>
        /// <param name="operationclass"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowInfor")]
        public async Task<TableModel<object>> ShowInfor(string OperationName,int menuid = 0,int operationclass = -1, int page = 1, int limit = 10)
        {
            PageModel<sys_operation> datainfor = new PageModel<sys_operation>();
            #region Lambda拼接式
            Expression<Func<sys_operation, bool>> wherelambda = c => true;
            if (!string.IsNullOrEmpty(OperationName))
            {
                wherelambda = PredicateExtensions.And<sys_operation>(wherelambda, c => c.OperationName.Contains(OperationName));
            }
            if (menuid != 0)
            {
                wherelambda = PredicateExtensions.And<sys_operation>(wherelambda, c => c.MenuID == menuid);
            }
            if (operationclass != -1)
            {
                wherelambda = PredicateExtensions.And<sys_operation>(wherelambda, c => c.OperationType == operationclass);
            }
            #endregion
            Expression<Func<sys_operation, object>> expression = null;
            datainfor = await _sys_OperationServices.Getoperationlist(wherelambda, expression, page, limit);
            var temp = datainfor.data.Select(c => new
            {
                ID = c.id,
                AuthorityName = c.OperationName,
                MenuID = c.MenuID,
                menuname = c.menumodel.MenuName,
                btnClassName = c.btnClassName,
                btneventName = c.btneventName,
                btnContainer = c.btnContainer,
                Type = c.OperationType,
                UseStatus = c.OperationStatus
            }).ToList();
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = datainfor.dataCount,
                data = temp
            };
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyInfor")]
        public async Task<MessageModel<object>> ModifyInfor(string data, int ID)
        {
            sys_operation Data = Common.Helper.JsonHelper.GetObject<sys_operation>(data);          
            Data.updatepeople =_user.Name;
            Data.id = ID;
            #region 判重
            #endregion
            var message = await _sys_OperationServices.Update(c => new sys_operation
            {
                MenuID = Data.MenuID,
                OperationName = Data.OperationName,
                updatepeople =_user.Name,
                updatetime = DateTime.Now,
                OperationType = Data.OperationType,
                btnClassName = Data.btnClassName,
                btneventName = Data.btneventName,
                btnContainer = Data.btnContainer
            }, c => c.id == Data.id) == true ? "ok" : "error";
            return new MessageModel<object>()
            {
                data = null,
                code = 0,
                msg = "ok"
            };
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="JsonDate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddData")]
        public async Task<MessageModel<object>> AddData(string JsonDate)
        {
            var alllist= await _sys_OperationServices.Query();
            int ID = alllist[alllist.Count-1].id+1;
            sys_operation data = Common.Helper.JsonHelper.GetObject<sys_operation>(JsonDate);
            data.createpeople = _user.Name;
            data.createtime = DateTime.Now;
            data.OperationNumber = "ON-000" + ID;
            var message = await _sys_OperationServices.Add(data) > 0 ? "ok" : "error";
            return new MessageModel<object>()
            {
                data = null,
                code = 0,
                msg = message
            };
        }
        #endregion

        #region 菜单下拉框数据
        /// <summary>
        /// 菜单下拉框数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowSelectInfor")]
        public async Task<MessageModel<object>> ShowSelectInfor()
        {
            var allist=await isys_MenuServices.Query(c => c.MenuType == 2);
            List<object> list = new List<object>();
            foreach (var item in allist)
            {
                var datalist = new { Name = item.MenuName, ID = item.id };
                list.Add(datalist);
            }
            return new MessageModel<object>() {
                data=list,
                code=0,
                msg="ok"
            };

        }
        #endregion

        #region 接口下拉框
        /// <summary>
        /// 接口下拉框
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowInterInfor")]
        public async Task<MessageModel<object>> ShowInterInfor()
        {
            var allist = await isys_Interface_InfoServices.Query();
            List<object> list = new List<object>();
            foreach (var item in allist)
            {
                var datalist = new { Name = item.InterfaceName, ID = item.ID };
                list.Add(datalist);
            }
            return new MessageModel<object>()
            {
                data = list,
                code = 0,
                msg = "ok"
            };
        }
        #endregion

        #region 判断状态
        /// <summary>
        /// 判断状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStatus")]
        public async Task<MessageModel<object>> GetStatus(int ID, int status = 0)
        {
            status = status == 0 ? 1 : 0;
            var msg=await _sys_OperationServices.Update(c => new sys_operation
            {
                OperationStatus = (short)status
            }, c => c.id == ID) == true ? "ok" : "error";
            return new MessageModel<object>()
            {
                data=null,
                code=0,
                msg=msg
            };
        }
        #endregion

        #endregion
    }
}