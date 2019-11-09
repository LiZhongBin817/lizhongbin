using CDWM_MR.Common.Helper;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 用户管理(档案管理)
    /// </summary>
    [Route("api/WatermeterUserManage")]
    [EnableCors("LimitRequests")]
    [AllowAnonymous]
    public class WatermeterUserManageController : Controller
    {
        #region  相关变量
        private readonly Iv_wateruserinfoServices _v_wateruserinfoServices;
        private readonly It_b_watermodelServices _t_b_watermodelServices;
        private readonly It_b_regionsServices _t_b_regionsServices;
        private readonly It_b_areasServices _t_b_areasServices;
        private readonly It_b_usersServices _t_b_usersServices;
        private readonly It_b_watermetersServices _t_b_watermetersServices;
        private readonly Iv_watermeterinfoServices _v_watermeterinfoServices;
        private readonly Imr_b_bookinfoServices _mr_b_bookinfoServices;
        private readonly Imr_b_readerServices _mr_b_readerServices;
        private readonly It_b_watermetertypeServices _t_b_watermetertypeServices;
        private readonly It_b_factoryServices _t_b_factoryServices;
        private readonly It_b_installposServices _t_b_installposServices;
        private readonly It_b_natureServices _t_b_natureServices;
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
        /// <param name="mr_b_bookinfoService"></param>
        /// <param name="mr_b_readerService"></param>
        /// <param name="t_b_watermetertypeService"></param>
        /// <param name="t_b_factoryService"></param>
        /// <param name="t_b_installposService"></param>
        /// <param name="t_b_watermodelService"></param>
        /// <param name="t_b_natureService"></param>
        public WatermeterUserManageController(Iv_wateruserinfoServices v_wateruserinfoServices, It_b_regionsServices t_b_regionsServices, It_b_areasServices t_b_areasServices, It_b_usersServices t_b_usersServices, It_b_watermetersServices t_b_watermetersServices, Iv_watermeterinfoServices v_watermeterinfoServices, Imr_b_bookinfoServices mr_b_bookinfoService, Imr_b_readerServices mr_b_readerService, It_b_watermetertypeServices t_b_watermetertypeService, It_b_factoryServices t_b_factoryService, It_b_installposServices t_b_installposService, It_b_watermodelServices t_b_watermodelService, It_b_natureServices t_b_natureService)
        {
            _v_wateruserinfoServices = v_wateruserinfoServices;
            _t_b_regionsServices = t_b_regionsServices;
            _t_b_areasServices = t_b_areasServices;
            _t_b_usersServices = t_b_usersServices;
            _t_b_watermetersServices = t_b_watermetersServices;
            _v_watermeterinfoServices = v_watermeterinfoServices;
            _mr_b_bookinfoServices = mr_b_bookinfoService;
            _mr_b_readerServices = mr_b_readerService;
            _t_b_watermetertypeServices = t_b_watermetertypeService;
            _t_b_installposServices = t_b_installposService;
            _t_b_factoryServices = t_b_factoryService;
            _t_b_watermodelServices = t_b_watermodelService;
            _t_b_natureServices = t_b_natureService;
        }
        #region  用户管理

        #region 下拉框的值
        /// <summary>
        /// 拿到下拉框的值
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ListData")]       
        public async Task<TableModel<object>> ListData()
        {
            List<t_b_factory> factorylist = new List<t_b_factory>();//存储生产厂商的集合
            List<t_b_installpos> installposlist = new List<t_b_installpos>();//存储安装位置的集合          
            List<t_b_watermetertype> watermetertypelist = new List<t_b_watermetertype>();//存储水表类型
            List<t_b_watermodel> watermodellist = new List<t_b_watermodel>();//存储用户型号的集合拿最大量程
            List<object> DataList = new List<object>();//存储返回的数据
            factorylist = await _t_b_factoryServices.OQuery(c => true);
            watermetertypelist = await _t_b_watermetertypeServices.OQuery(c => true);
            installposlist = await _t_b_installposServices.OQuery(c => true);           
            watermodellist = await _t_b_watermodelServices.OQuery(c => true);
            DataList.Add(factorylist);
            DataList.Add(installposlist);
            DataList.Add(watermetertypelist);
            DataList.Add(watermodellist);
            return new TableModel<object>()
            {
                code = 0,
                data = DataList,
                count = DataList.Count,
                msg = "ok"
            };
        }
        #endregion

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
        [HttpGet]
        [Route("ShowWaterUserinfo")]        
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("SelectValue")]       
        public async Task<TableModel<object>> SelectValue()
        {
            List<mr_b_bookinfo> bookinfo = await _mr_b_bookinfoServices.Query();
            List<mr_b_reader> readerinfo = await _mr_b_readerServices.Query();
            //显示所有区域
            List<t_b_regions> regionlist = await _t_b_regionsServices.OQuery(c => true);
            List<t_b_areas> arealist = await _t_b_areasServices.OQuery(c => true);
            List<object> datalist = new List<object>();
            datalist.Add(bookinfo);
            datalist.Add(readerinfo);
            datalist.Add(regionlist);
            datalist.Add(arealist);
            return new TableModel<object>
            {
                data = datalist,
                msg = "ok",
                code = 0,
                count = datalist.Count
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
        public async Task<TableModel<object>> ShowEditRegionDate(string account)
        {
            List<object> list = new List<object>();
            //显示所有区域
            List<v_wateruserinfo> watermeteruserlist = await _v_wateruserinfoServices.Query(c => c.account == account);
            //显示用户历史表单记录
            List<v_watermeterinfo> watermeterlist = await _v_watermeterinfoServices.Query(c => c.account == account);
            list.Add(watermeteruserlist);
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
        public async Task<TableModel<object>> ShowEditAreasDate(string regionno)
        {
            List<t_b_areas> areas = await _t_b_areasServices.OQuery(c => c.regionno == regionno);
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
        public async Task<TableModel<object>> EditUserInfo(string JsonDate)
        {
            t_b_users Edit = Common.Helper.JsonHelper.GetObject<t_b_users>(JsonDate);
            Edit.accstate = 1;
            Edit.lastmodifytime = DateTime.Now;
            Edit.lastmodifyby = Permissions.UersName;
            await _t_b_usersServices.OUpdate(Edit);
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
        /// 新增水表时返回的数据
        /// </summary>
        /// <param name="meternum1">水表编号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("showaddmeterinfo")]      
        public async Task<TableModel<object>> showaddmeterinfo(string meternum1)
        {
            string GISPlace = "";
            List<t_b_factory> factorylist = new List<t_b_factory>();//存储生产厂商的集合
            List<t_b_installpos> installposlist = new List<t_b_installpos>();//存储安装位置的集合          
            List<t_b_watermetertype> watermetertypelist = new List<t_b_watermetertype>();//存储水表类型
            List<t_b_watermodel> watermodellist = new List<t_b_watermodel>();//存储用户型号的集合
            List<object> DataList = new List<object>();//存储返回的数据
            factorylist = await _t_b_factoryServices.OQuery(c => true);
            installposlist = await _t_b_installposServices.OQuery(c => true);
            watermetertypelist = await _t_b_watermetertypeServices.OQuery(c => true);
            watermodellist = await _t_b_watermodelServices.OQuery(c => true);
            //取到水表最后一条数据
            Expression<Func<t_b_watermeters, object>> expression = c => new
            {
                c.account
            };
            List<t_b_watermeters> last = await _t_b_watermetersServices.OQuery(c => true);
            if(meternum1==null)
            {

            }
            else
            {
                List<t_b_watermeters> GISPlacelist = await _t_b_watermetersServices.OQuery(c => c.meternum == meternum1);
                GISPlace = GISPlacelist[0].GISPlace;
            }          
            //生成自动表号
            string lastnumber = last[last.Count() - 1].meternum;
            string SNumber = (last.Count).ToString();
            string meternum = Convert.ToString(Convert.ToDouble(lastnumber) + 1);           
            List<string> Data = new List<string>();
            Data.Add(meternum);
            Data.Add(SNumber);
            Data.Add(GISPlace);
            DataList.Add(factorylist);
            DataList.Add(installposlist);
            DataList.Add(watermetertypelist);
            DataList.Add(Data);
            DataList.Add(watermodellist);
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = DataList.Count,
                data = DataList
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
        public async Task<TableModel<object>> AddWaterMeter(string JsonDate, string autoaccount)
        {
            //转换对象
            t_b_watermeters adduwatermeter = Common.Helper.JsonHelper.GetObject<t_b_watermeters>(JsonDate);
            adduwatermeter.autoaccount = autoaccount;
            List<t_b_watermeters>judge=await _t_b_watermetersServices.OQuery(c => c.autoaccount == autoaccount);
            if(judge.Count!=0)
            {
                adduwatermeter.meterstate = 0;//状态为未使用
            }
            else
            {
                adduwatermeter.meterstate = 1;//状态为使用
            }     
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
        [HttpGet]
        [Route("Adduser")]        
        public async Task<TableModel<object>> Adduser(string JsonDate)
        {
            long autoaccount = 0;
            List<t_b_users> User = await _t_b_usersServices.OQuery(c => true);
            if (User.Count == 0)
            {
                autoaccount = 1;
            }
            else
            {
                autoaccount = Convert.ToInt64((User[User.Count - 1].autoaccount)) + 1;
            }
            t_b_users adduser = Common.Helper.JsonHelper.GetObject<t_b_users>(JsonDate);
            adduser.autoaccount = autoaccount.ToString();
            await _t_b_usersServices.OAdd(adduser);
            return new TableModel<object>()
            {
                code = 0,
                msg = "ok",
                count = 1,
                data = null
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AdduserDataShow")]      
        public async Task<TableModel<object>> AdduserDataShow()
        {
            List<t_b_nature> natureslist = new List<t_b_nature>();
            List<t_b_areas> arealist = new List<t_b_areas>();
            List<object> Datalist = new List<object>();
            natureslist = await _t_b_natureServices.OQuery(c => c.bntstate == 1);
            arealist = await _t_b_areasServices.OQuery(c => c.areastate == 1);
            Datalist.Add(natureslist);
            Datalist.Add(arealist);
            return new TableModel<object>()
            {
                code = 0,
                data = Datalist,
                count = Datalist.Count,
                msg = "ok"
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
        public async Task<TableModel<object>> ShowChangemeter(string account)
        {
            List<v_wateruserinfo> waterinfolist = await _v_wateruserinfoServices.Query(c => c.account == account);
            //取到所有编号
            List<string> NumberList = new List<string>();
            List<t_b_watermodel> watermodellist = new List<t_b_watermodel>();//存储用户型号的集合拿最大量程
            List<t_b_installpos> installposlist = new List<t_b_installpos>();//存储安装位置的集合   
            installposlist = await _t_b_installposServices.OQuery(c => true);
            watermodellist = await _t_b_watermodelServices.OQuery(c => true);
            for (int i = 0; i < waterinfolist.Count(); i++)
            {
                NumberList.Add(waterinfolist[i].meternum);
            }
            List<t_b_watermeters> watermaterlist = await _t_b_watermetersServices.OQuery(c => NumberList.Contains(c.meternum));
            List<object> showwatermater = new List<object>();
            //添加旧表
            showwatermater.Add(watermaterlist.Find(c => c.meterstate == 1));//刚显示 的时候表还没换，所以旧表的状态显示正常
            showwatermater.Add(watermaterlist[watermaterlist.Count() - 1]);//新表为最后一个表
            showwatermater.Add(installposlist);
            showwatermater.Add(watermodellist);
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
        /// <param name="lastmodifyby">修改人</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Changemeter")]      
        public async Task<TableModel<object>> Changemeter(string Oldmeternum, string Newmeternum,string lastmodifyby)
        {
            //旧表状态变成 未使用
            await _t_b_watermetersServices.OUpdate(c => new t_b_watermeters
            {
                meterstate = 0//未使用
            }, c => c.meternum == Oldmeternum);
            //新表状态变成正常
            await _t_b_watermetersServices.OUpdate(c => new t_b_watermeters
            {
                meterstate = 1,               
                lastmodifyby=lastmodifyby//正常
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

        #region 删除
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="autoaccount"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DelUser")]       
        public async Task<TableModel<object>> DelUser(string autoaccount)
        {
            await _t_b_usersServices.OUpdate(c => new t_b_users { accstate = 2 }, c => c.autoaccount == autoaccount);
            return new TableModel<object>()
            {
                code = 0,
                data = null,
                count = 0,
                msg = "ok"
            };
        }
        #endregion

        #region 导入Excel
        /// <summary>
        /// 下载模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DownloadOrderTemplet")]        
        public IActionResult DownloadOrderTemplet()
        {
            string a=System.Environment.CurrentDirectory;   //当前路径
            string filePath = a+"\\"+"wwwroot\\WatermeterManage\\userinfo.xls";//路径
            //获取文件的ContentType
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            var memi = provider.Mappings[".xls"];
            return PhysicalFile(filePath, memi, "userinfo.xls"); //welcome.txt是客户端保存的名字
        }

        /// <summary>
        /// 导入EXCEL
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PutExcel")]
        public async Task<MessageModel<string>> PutExcel([FromServices]IHostingEnvironment environment)
        {
            var data = new MessageModel<string>();
            string path = string.Empty;
            string foldername = "File_WatermeterUser";
            IFormFileCollection files = null;
            try
            {
                files = Request.Form.Files;
            }
            catch (Exception)
            {
                files = null;
            }
            if (files == null || !files.Any())
            {
                if (files == null)
                {
                    data.msg = "异常了"; return data;
                }
                data.msg = $"请选择上传的文件。{files.Count()}"; return data;
            }
            //格式限制
            var allowType = new string[] { "XLS 工作表/xls", "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };
            string folderpath = Path.Combine(environment.WebRootPath, foldername);
            if (!System.IO.Directory.Exists(folderpath))
            {
                System.IO.Directory.CreateDirectory(folderpath);
            }
            if (files.Any(c => allowType.Contains(c.ContentType)))
            {
                foreach (var item in files)
                {
                    string strpath = Path.Combine(foldername, item.FileName);
                    path = Path.Combine(environment.WebRootPath, strpath);

                    using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        await item.CopyToAsync(stream);
                    }
                }
                Hashtable tb = new Hashtable();
                tb.Add("account", "用户编号");
                tb.Add("username", "用户名称");
                tb.Add("sex", "性别");
                tb.Add("address", "地址");
                tb.Add("areano", "所属小区");
                tb.Add("usermetertype", "用水类型");
                tb.Add("telephone", "电话");
                List<UserFromExcel> dataExcel = OfficeHelper.fromExcel<UserFromExcel>(tb, path);
                List<t_b_areas> areas = new List<t_b_areas>();
                for (int i = 0; i < dataExcel.Count; i++)
                {
                    long autoaccount = 0;
                    List<t_b_users> User = await _t_b_usersServices.OQuery(c => true);
                    if (User.Count == 0)
                    {
                        autoaccount = 1;
                    }
                    else
                    {
                        autoaccount = Convert.ToInt64((User[User.Count - 1].autoaccount))+ 1;
                    }
                    t_b_users userobj = new t_b_users();
                    for (int j = 0; j < areas.Count; j++)
                    {
                        if (areas[j].areaname == dataExcel[i].areano)
                        {
                            userobj.areano = areas[j].areano;
                        }
                    }
                    userobj.autoaccount = autoaccount.ToString();
                    userobj.account = dataExcel[i].account;
                    userobj.username = dataExcel[i].username;
                    userobj.address = dataExcel[i].address;
                    userobj.usemetertype = dataExcel[i].usemetertype;
                    userobj.telephone = dataExcel[i].telephone;
                    userobj.accstate = 1;
                    userobj.createby = "李忠斌";
                    userobj.createtime = DateTime.Now;
                    userobj.sex = dataExcel[i].sex == "男" ? Convert.ToInt16(0) : Convert.ToInt16(1);
                    await _t_b_usersServices.OAdd(userobj);
                }
                return new MessageModel<string>()
                {
                    msg = "ok",
                    data = null,
                    code = 0
                };
            }
            else
            {
                data.msg = "图片格式错误";
                return data;
            }


        }

        #endregion
        #endregion
    }
}
