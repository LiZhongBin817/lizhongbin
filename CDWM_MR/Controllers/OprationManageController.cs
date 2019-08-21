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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDWM_MR.Controllers
{
    
    public class OprationManageController : ControllerBase
    {
        #region 相关变量
        readonly Isys_operationServices _sys_OperationServices;
        readonly Isys_menuServices isys_MenuServices;
        readonly Isys_interface_infoServices isys_Interface_InfoServices;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isys_OperationServices"></param>
        /// <param name="_MenuServices"></param>
        /// <param name="_Interface_InfoServices"></param>
        public OprationManageController(Isys_operationServices isys_OperationServices,Isys_menuServices _MenuServices,Isys_interface_infoServices _Interface_InfoServices)
        {
            _sys_OperationServices = isys_OperationServices;
            isys_MenuServices = _MenuServices;
            isys_Interface_InfoServices = _Interface_InfoServices;
        }
        #endregion

        #region 权限管理

        #region 显示数据
        /// <summary>
        /// 显示权限信息
        /// </summary>
        /// <param name="OperationName"></param>
        /// <param name="InterfaceID"></param>
        /// <param name="LinkUrl"></param>
        /// <param name="MenuID"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowInfor")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<TableModel<object>> ShowInfor(string OperationName, int InterfaceID, string LinkUrl, int MenuID, int page = 1, int limit = 10)
        {
            PageModel<object> datainfor = new PageModel<object>();
            #region Lambda拼接式
            Expression<Func<sys_operation, bool>> wherelambda = c => true;
            if (!string.IsNullOrEmpty(OperationName))
            {
                wherelambda = PredicateExtensions.And<sys_operation>(wherelambda, c => c.OperationName == OperationName);
            }
            if (InterfaceID!=0)
            {
                wherelambda = PredicateExtensions.And<sys_operation>(wherelambda, c => c.InterfaceID == InterfaceID);
            }
            if (!string.IsNullOrEmpty(LinkUrl))
            {
                wherelambda = PredicateExtensions.And<sys_operation>(wherelambda, c => c.LinkUrl == LinkUrl);
            }
            if (MenuID!=0)
            {
                wherelambda = PredicateExtensions.And<sys_operation>(wherelambda, c => c.MenuID == MenuID);
            }
            #endregion
            Expression<Func<sys_operation, object>> expression = c => new
            {
                ID = c.ID,
                AuthorityName = c.OperationName,
                MenuID = c.MenuID,
                InterfaceID = c.InterfaceID,
                URL = c.LinkUrl,
                Type = c.OperationType,
                UseStatus = c.OperationStatus == 0 ? "使用" : "作废"
            };
            datainfor = await _sys_OperationServices.QueryPage(wherelambda, expression, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = datainfor.dataCount,
                data = datainfor.data
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
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<object>> ModifyInfor(string data, int ID)
        {
            sys_operation Data = Common.Helper.JsonHelper.GetObject<sys_operation>(data);          
            Data.UpdatePeople = "1";
            Data.UpdateTime = DateTime.Now;
            Data.ID = ID;
            #region 判重
            Expression<Func<sys_operation, bool>> wherelambda = c =>c.ID!=ID&&(c.OperationName == Data.OperationName||c.LinkUrl==Data.LinkUrl);
            var listQuery = await _sys_OperationServices.Query(wherelambda);
            string message = "";
            if (listQuery.Count != 0)
            {
                return new MessageModel<object>()
                {
                    data = null,
                    code = 1,
                    msg = "该权限名称或者URL地址存在"
                };
            }
            #endregion
            message = await _sys_OperationServices.Update(c => new sys_operation
            {
                LinkUrl = Data.LinkUrl,
                OperationName = Data.OperationName,
                UpdatePeople = "1",
                UpdateTime = DateTime.Now,
                OperationType = Data.OperationType
            }, c => c.OperationName == Data.OperationName) == true ? "ok" : "error";
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
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<object>> AddData(string JsonDate)
        {
            string[] jsondata = JsonDate.Split('"',';','{',':',',');
            int MenuID=Convert.ToInt32(jsondata[11].Substring(0, jsondata[11].IndexOf('-')));
            int InterfaceID=Convert.ToInt32(jsondata[17].Substring(0,jsondata[17].IndexOf('-')));
            var alllist= await _sys_OperationServices.Query();
            int ID = alllist[alllist.Count-1].ID+1;
            sys_operation data = Common.Helper.JsonHelper.GetObject<sys_operation>(JsonDate);
            data.CreatePeople = "1";
            data.CreateTime = DateTime.Now;
            data.OperationNumber = "ON-000" + ID;
            data.MenuID = MenuID;
            data.InterfaceID = InterfaceID;
            #region 判重
            string operationname = data.OperationName;
            string linkurl = data.LinkUrl;
            var listQuery = alllist.FindAll(c => c.OperationName == operationname || c.LinkUrl == linkurl);
            string message = "";
            if (listQuery.Count!=0)
            {
                return new MessageModel<object>()
                {
                    data = null,
                    code = 1,
                    msg ="该权限存在"
                };
            }
            #endregion
            message = await _sys_OperationServices.Add(data) > 0 ? "ok" : "error";
            return new MessageModel<object>()
            {
                data = null,
                code = 0,
                msg = message
            };
        }
        #endregion

        #region 菜单下拉框数据
        [HttpPost]
        [Route("ShowSelectInfor")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<object>> ShowSelectInfor()
        {
            var allist=await isys_MenuServices.Query();
            List<object> list = new List<object>();
            foreach (var item in allist)
            {
                var datalist = new { Name = item.MenuName, ID = item.ID };
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
        [HttpPost]
        [Route("ShowInterInfor")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
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
        [HttpPost]
        [Route("GetStatus")]
        [AllowAnonymous]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<object>> GetStatus(int ID, string status)
        {
            int s= status == "使用" ? 0 : 1;
            var msg=await _sys_OperationServices.Update(c => new sys_operation
            {
                OperationStatus = (short)s
            }, c => c.ID == ID) == true ? "ok" : "error";
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