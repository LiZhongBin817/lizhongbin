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
    /// <summary>
    /// 抄表员管理
    /// </summary>
    [Route("api/Mr_b_Reader")]
    [AllowAnonymous]
    [EnableCors("LimitRequests")]
    public class Mr_b_ReaderController : ControllerBase
    {
        #region 相关变量
        readonly Imr_b_readerServices _B_ReaderServices;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="b_ReaderServices"></param>
        public Mr_b_ReaderController(Imr_b_readerServices b_ReaderServices)
        {
            _B_ReaderServices = b_ReaderServices;
        }

        
        /// <summary>
        /// 展示抄表员数据
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="Name"></param>
        /// <param name="Telephone"></param>
        /// <param name="Appcount"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ShowMassage")]      
        public async Task<TableModel<object>> ShowMassage(string Number,string Name,string Telephone,string Appcount,int page=1,int limit=10)
        {
            //分页信息
            PageModel<object> pageModel = new PageModel<object>();
            #region lambda拼接式
            Expression<Func<mr_b_reader, bool>> wherelambda = c =>c.deleteflag!=1;
            if (!string.IsNullOrEmpty(Number))
            {
                wherelambda = PredicateExtensions.And<mr_b_reader>(wherelambda, c => c.mrreadernumber.Contains(Number));
            }
            if (!string.IsNullOrEmpty(Name))
            {
                wherelambda =PredicateExtensions.And<mr_b_reader>(wherelambda, c => c.mrreadername.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Telephone))
            {
                wherelambda = PredicateExtensions.And<mr_b_reader>(wherelambda, c => c.telephone .Contains(Telephone)) ;
            }
            if (!string.IsNullOrEmpty(Appcount))
            {
                wherelambda = PredicateExtensions.And<mr_b_reader>(wherelambda, c => c.appcount .Contains(Appcount));
            }
            #endregion
            Expression<Func<mr_b_reader, object>> expression = c => new
            {
                ID = c.id,
                mrreadernumber = c.mrreadernumber,
                mrreadername = c.mrreadername,
                telephone = c.telephone,
                appcount = c.appcount,
                apppassword = c.apppassword,
                nearnrtime = c.nearnrtime,
                address = c.address,
                sex = c.sex,
                idcard = c.idcard,
                roles = c.roles,
                lastlogintime = c.lastlogintime,
                Remark = c.remark,
               
            };
            pageModel = await _B_ReaderServices.QueryPage(wherelambda,expression,page,limit,"");
            return new TableModel<object>
            {
                code=0,
                count=pageModel.dataCount,
                msg="OK",
                data=pageModel.data
            };
        }


        /// <summary>
        /// 添加抄表员信息
        /// </summary>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add_Mr_B_ReaderData")]      
        public async Task<TableModel<object>> Add_Mr_B_ReaderData(string JsonData)
        {
            //转换Json数据
            mr_b_reader jsonData = Common.Helper.JsonHelper.GetObject<mr_b_reader>(JsonData);
            //查询抄表员表信息
            var MrData =await  _B_ReaderServices.Query();
            #region 编号自动生成
            int MrrreaderNUmber = Convert.ToInt32( MrData[MrData.Count - 1].mrreadernumber.Substring(MrData[MrData.Count - 1].mrreadernumber.Length - 1, 1));          
            jsonData.mrreadernumber = "CB00" + (MrrreaderNUmber + 1);
            #endregion
            jsonData.nearnrtime = DateTime.Now;
            jsonData.deleteflag = 0;
            jsonData.lastlogintime = DateTime.Now;
            jsonData.appcount = jsonData.mrreadernumber;
            jsonData.apppassword = MD5Helper.MD5Encrypt32(jsonData.apppassword);
            jsonData.createpeople = Permissions.UersName;
            jsonData.createtime = DateTime.Now;

            //根据一个账号对应一个用户判重，但是一个用户可以对应多个账号
            var data = MrData.FindAll(c => c.mrreadername == jsonData.mrreadername && c.appcount == jsonData.appcount);
            if (data.Count() > 0)
            {
                return new TableModel<object>
                {
                    code = 1001,
                    msg = "重复",
                    count = 0,
                    data = "",
                };
            }
             //添加数据
            var msg=await _B_ReaderServices.Add(jsonData)>0?"OK":"Error";
            return new TableModel<object>
            {
                code = msg == "OK" ? 0 : 1001,
                msg = msg,
                data = "",
            };
        }

        /// <summary>
        /// 编辑抄表员信息
        /// </summary>
        /// <param name="JsonData"></param>
        /// <param name="ID"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("Edit_Mr_B_ReaderData")]       
        public async Task<TableModel<object>> Edit_Mr_B_ReaderData(string JsonData,int ID)
        {
            mr_b_reader jsonData=Common.Helper.JsonHelper.GetObject<mr_b_reader>(JsonData);      
           string Msg = await _B_ReaderServices.Update(c=> new mr_b_reader
           {
               mrreadername = jsonData.mrreadername,
               telephone = jsonData.telephone,
               nearnrtime=c.nearnrtime,
               address= jsonData.address,
               sex =jsonData.sex,
               idcard=jsonData.idcard,
               updatepeople=c.createpeople,
               updatetime=DateTime.Now,
               roles=jsonData.roles,
               remark=jsonData.remark
           },c=>c.id==ID)?"OK":"Error";
            return new TableModel<object>
            {
                code = Msg == "OK" ? 0 : 1001,
                msg = Msg,
                data = "",
            };
        }


        /// <summary>
        ///逻辑删除抄表员信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Del_Mr_B_ReaderData")]      
        public async Task<TableModel<object>> Del_Mr_B_ReaderData(int ID)
        {
            string Msg = await _B_ReaderServices.Update(c => new mr_b_reader
            {
             //0---- 未删除    1-----删除
             deleteflag=1,
            },c=>c.id==ID) ? "OK" : "Error";
            return new TableModel<object>
            {
                code = Msg == "OK" ? 0 : 1001,
                msg = Msg,
                data = "",
            };
        }


        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReSetPwd")]  
        public async Task<TableModel<object>> ReSetPwd(int ID)
        {
            string Msg = await _B_ReaderServices.Update(c=>new mr_b_reader
            {
                apppassword=MD5Helper.MD5Encrypt32("123456")
            },c=>c.id==ID) ? "OK" : "Error";
            return new TableModel<object>
            {
                code = Msg == "OK" ? 0 : 1001,
                msg = Msg,
                data = "",
            };
        }
    }
}