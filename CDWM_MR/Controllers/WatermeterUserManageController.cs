using CDWM_MR.Common.Helper;
using CDWM_MR.Common.HttpContextUser;
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
        private readonly IUser _users;
        #endregion
        /// <summary>
        /// 构造函数注入
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
        /// <param name="users"></param>
        public WatermeterUserManageController(Iv_wateruserinfoServices v_wateruserinfoServices, It_b_regionsServices t_b_regionsServices, It_b_areasServices t_b_areasServices, It_b_usersServices t_b_usersServices, It_b_watermetersServices t_b_watermetersServices, Iv_watermeterinfoServices v_watermeterinfoServices, Imr_b_bookinfoServices mr_b_bookinfoService, Imr_b_readerServices mr_b_readerService, It_b_watermetertypeServices t_b_watermetertypeService, It_b_factoryServices t_b_factoryService, It_b_installposServices t_b_installposService, It_b_watermodelServices t_b_watermodelService, It_b_natureServices t_b_natureService, IUser users)
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
            _users = users;
        }
        #region  用户管理

        #region 下拉框的值
        /// <summary>
        /// 拿到下拉框的值
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ListData")]
        public async Task<MessageModel<object>> ListData(string meternum)
        {
            List<v_watermeterinfo> meterinfo = await _v_watermeterinfoServices.Query(c => c.meternum == meternum);
            List<t_b_installpos> installposlist = new List<t_b_installpos>();//存储安装位置的集合 
            List<t_b_watermodel> watermodellist = new List<t_b_watermodel>();//存储用户型号的集合拿最大量程
            List<object> DataList = new List<object>();//存储返回的数据
            installposlist = await _t_b_installposServices.OQuery(c => true);
            watermodellist = await _t_b_watermodelServices.OQuery(c => c.bwmstate == 1);
            DataList.Add(meterinfo);
            DataList.Add(installposlist);
            DataList.Add(watermodellist);
            return new MessageModel<object>()
            {
                code = 0,
                data = DataList,
                msg = "ok"
            };
        }
        #endregion

        #region  显示
        /// <summary>
        /// 显示用户管理
        /// </summary>
        /// <param name="account"></param>
        /// <param name="meternum"></param>
        /// <param name="username"></param>
        /// <param name="readername"></param>
        /// <param name="bookno"></param>
        /// <param name="regionplace"></param>
        /// <param name="areaname"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ShowWaterUserinfo")]
        public async Task<TableModel<List<v_wateruserinfo>>> ShowWaterUserinfo(string account,string meternum,string username, string readername, string bookno, string regionplace, string areaname, int page = 1, int limit = 5)
        {
            PageModel<v_wateruserinfo> showdate = new PageModel<v_wateruserinfo>();
            #region  lambda拼接式
            Expression<Func<v_wateruserinfo, bool>> wherelambda = c => true;
            if (!string.IsNullOrEmpty(account))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.autoaccount.Contains(account));
            }
            if (!string.IsNullOrEmpty(meternum))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.meternum.Contains(meternum));
            }
            if (!string.IsNullOrEmpty(username))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.username.Contains(username));
            }
            if (!string.IsNullOrEmpty(readername))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.readername.Contains(readername));
            }
            if (!string.IsNullOrEmpty(bookno))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.bookno.Contains(bookno));
            }
            if (!string.IsNullOrEmpty(regionplace))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.regionno == regionplace);
            }
            if (!string.IsNullOrEmpty(areaname))
            {
                wherelambda = PredicateExtensions.And<v_wateruserinfo>(wherelambda, c => c.areano == areaname);
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
        /// 展示页面下拉框所需数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("SelectValue")]
        public async Task<MessageModel<object>> SelectValue()
        {
            List<mr_b_bookinfo> bookinfo = await _mr_b_bookinfoServices.Query();
            List<mr_b_reader> readerinfo = await _mr_b_readerServices.Query(c => c.deleteflag == 0);
            //显示所有区域
            List<t_b_regions> regionlist = await _t_b_regionsServices.OQuery(c => c.regionstate == 1);
            //List<t_b_areas> arealist = await _t_b_areasServices.OQuery(c => c.areastate == 1);
            List<object> datalist = new List<object>();
            datalist.Add(bookinfo);
            datalist.Add(readerinfo);
            datalist.Add(regionlist);
            //datalist.Add(arealist);
            return new MessageModel<object>
            {
                data = datalist,
                msg = "ok",
                code = 0
            };
        }

        /// <summary>
        /// 区域选择触发
        /// </summary>
        /// <param name="regionno"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegionSelShow")]
        public async Task<MessageModel<List<t_b_areas>>> RegionSelShow(string regionno) 
        {
            List<t_b_areas> arealist = await _t_b_areasServices.OQuery(c => c.areastate == 1 && c.regionno == regionno);
            return new MessageModel<List<t_b_areas>>()
            {
                code = 0,
                msg = "成功",
                data = arealist
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
            List<v_wateruserinfo> watermeteruserlist = await _v_wateruserinfoServices.Query(c => c.autoaccount == account);
            //显示用户历史表单记录
            List<v_watermeterinfo> watermeterlist = await _v_watermeterinfoServices.Query(c => c.autoaccount == account);
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
            Edit.lastmodifytime = DateTime.Now;
            Edit.lastmodifyby = _users.Name;
            await _t_b_usersServices.OUpdate(c => new t_b_users()
            {
                username = Edit.username,
                telephone = Edit.telephone,
                areano = Edit.areano,
                address = Edit.address,
                lastmodifytime = DateTime.Now,
                lastmodifyby = _users.Name
            }, c => c.autoaccount == Edit.autoaccount);
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
            t_b_watermeters modifyobj = null;
            if (Edit.meterstate == 3)
            {
                modifyobj = new t_b_watermeters()
                {
                    metermodel = Edit.metermodel,
                    bwcode = Edit.bwcode,
                    installpos = Edit.installpos,
                    lastwaternum = Edit.lastwaternum,
                    meterstate = Edit.meterstate,
                    updatemetertime = Edit.updatemetertime,
                    GISPlace = Edit.GISPlace,
                    bookno = null
                };
            }
            else 
            {
                modifyobj = new t_b_watermeters()
                {
                    metermodel = Edit.metermodel,
                    bwcode = Edit.bwcode,
                    installpos = Edit.installpos,
                    lastwaternum = Edit.lastwaternum,
                    meterstate = Edit.meterstate,
                    updatemetertime = Edit.updatemetertime,
                    GISPlace = Edit.GISPlace
                };
            }
            await _t_b_watermetersServices.OUpdate(c => modifyobj, c=> c.meternum == Edit.meternum);
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
        /// <returns></returns>
        [HttpPost]
        [Route("showaddmeterinfo")]
        public async Task<TableModel<object>> showaddmeterinfo()
        {
            List<t_b_factory> factorylist = new List<t_b_factory>();//存储生产厂商的集合
            List<t_b_installpos> installposlist = new List<t_b_installpos>();//存储安装位置的集合          
            List<t_b_watermetertype> watermetertypelist = new List<t_b_watermetertype>();//存储水表类型
            List<t_b_watermodel> watermodellist = new List<t_b_watermodel>();//存储用户型号的集合
            List<object> DataList = new List<object>();//存储返回的数据
            factorylist = await _t_b_factoryServices.OQuery(c => true);
            installposlist = await _t_b_installposServices.OQuery(c => true);
            watermetertypelist = await _t_b_watermetertypeServices.OQuery(c => true);
            watermodellist = await _t_b_watermodelServices.OQuery(c => c.bwmstate == 1);
            DataList.Add(factorylist);
            DataList.Add(installposlist);
            DataList.Add(watermetertypelist);
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
        /// 口径下拉框触发
        /// </summary>
        /// <param name="bmlid"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CarialSeltigger")]
        public async Task<MessageModel<object>> CarialSeltigger(int bmlid)
        {
            var data = await _t_b_watermodelServices.OQuery(c => c.bmlid == bmlid);
            if (data.Count <= 0) 
            {
                return new MessageModel<object>() {
                    code = 0,
                    msg = "OK",
                    data = 9999
                };
            }
            return new MessageModel<object>() { 
                code = 0,
                msg = "成功",
                data = data[0].maxrange
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
        public async Task<MessageModel<object>> AddWaterMeter(string JsonDate, string autoaccount)
        {
            //转换对象
            t_b_watermeters adduwatermeter = Common.Helper.JsonHelper.GetObject<t_b_watermeters>(JsonDate);
            adduwatermeter.meterstate = 1;//状态为使用
            adduwatermeter.createby = _users.Name;
            adduwatermeter.createtime = DateTime.Now;
            adduwatermeter.delflag = 1;//使用标记
            var addid = await _t_b_watermetersServices.OQuery(c => true, s => new t_b_watermeters()
            {
                meternum = s.meternum
            }, "meternum desc", 1);
            if (addid == null || addid.Count <= 0)
            {
                adduwatermeter.meternum = "1000000001";
            }
            else
            {
                var tempnum = Convert.ToInt64(addid[0].meternum);
                tempnum += 1;
                adduwatermeter.meternum = tempnum.ObjToString();
            }
            //添加
            await _t_b_watermetersServices.OAdd(adduwatermeter);
            return new MessageModel<object>()
            {
                code = 0,
                msg = "ok",
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
            t_b_users adduser = Common.Helper.JsonHelper.GetObject<t_b_users>(JsonDate);
            adduser.createby = _users.Name;
            adduser.createtime = DateTime.Now;
            var addid = await _t_b_usersServices.OQuery(c => true, s => new t_b_users()
            {
                autoaccount = s.autoaccount,
                account = s.account
            }, "autoaccount desc", 1);
            if (addid == null || addid.Count <= 0)
            {
                adduser.autoaccount = "100000000001";
                adduser.account = "160001";
            }
            else
            {
                var tempnum = Convert.ToInt64(addid[0].autoaccount);
                var tempnum2 = Convert.ToInt32(addid[0].account);
                tempnum += 1;
                tempnum2 += 1;
                adduser.autoaccount = tempnum.ObjToString();
                adduser.account = tempnum2.ObjToString();
            }
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
        /// 添加弹窗后台所需下拉框数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AdduserDataShow")]
        public async Task<MessageModel<object>> AdduserDataShow()
        {
            List<t_b_nature> natureslist = new List<t_b_nature>();
            List<t_b_areas> arealist = new List<t_b_areas>();
            List<object> Datalist = new List<object>();
            natureslist = await _t_b_natureServices.OQuery(c => c.bntstate == 1);
            arealist = await _t_b_areasServices.OQuery(c => c.areastate == 1);
            Datalist.Add(natureslist);
            Datalist.Add(arealist);
            return new MessageModel<object>()
            {
                code = 0,
                data = Datalist,
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
        public async Task<MessageModel<object>> ShowChangemeter(string account)
        {
            List<v_watermeterinfo> waterinfolist = await _v_watermeterinfoServices.Query(c => c.autoaccount == account && c.meterstate == 1);//用户对应的正常水表
            List<string> NumberList = new List<string>();
            List<t_b_factory> factorylist = new List<t_b_factory>();//存储生产厂商的集合
            List<t_b_installpos> installposlist = new List<t_b_installpos>();//存储安装位置的集合          
            List<t_b_watermetertype> watermetertypelist = new List<t_b_watermetertype>();//存储水表类型
            List<t_b_watermodel> watermodellist = new List<t_b_watermodel>();//存储用户型号的集合
            factorylist = await _t_b_factoryServices.OQuery(c => true);
            installposlist = await _t_b_installposServices.OQuery(c => true);
            watermetertypelist = await _t_b_watermetertypeServices.OQuery(c => true);
            watermodellist = await _t_b_watermodelServices.OQuery(c => c.bwmstate == 1);
            List<object> showwatermater = new List<object>();
            showwatermater.Add(factorylist);
            showwatermater.Add(installposlist);
            showwatermater.Add(watermetertypelist);
            showwatermater.Add(watermodellist);
            showwatermater.Add(waterinfolist);
            return new MessageModel<object>()
            {
                code = 0,
                msg = "ok",
                data = showwatermater
            };
        }
        
        /// <summary>
        /// 换表操作
        /// </summary>
        /// <param name="Oldmeterinfo">旧表信息</param>
        /// <param name="Newmeterinfo">新表信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Changemeter")]
        public async Task<MessageModel<object>> Changemeter(string Oldmeterinfo, string Newmeterinfo)
        {
            var oldmeterobject = JsonHelper.GetObject<t_b_watermeters>(Oldmeterinfo);
            var newmeterobject = JsonHelper.GetObject<t_b_watermeters>(Newmeterinfo);
            var judeboolen = await _t_b_watermetersServices.OExistModel(c => c.meternum == newmeterobject.meternum);
            if (judeboolen)
            {
                return new MessageModel<object>()
                {
                    code = 0,
                    msg = "repeat",
                    data = "存在重复数据"
                };
            }
            //将旧水表状态置于未使用
            await _t_b_watermetersServices.OUpdate(c => new t_b_watermeters() {
                updatemetertime = oldmeterobject.updatemetertime,
                remark = oldmeterobject.remark,
                lastwaternum = oldmeterobject.lastwaternum,
                GISPlace = oldmeterobject.GISPlace,
                meterstate = 3,
                bookno = null
            },c => c.meternum == oldmeterobject.meternum);
            
            //添加新水表信息
            newmeterobject.meterstate = 1;//状态为使用
            newmeterobject.createby = _users.Name;
            newmeterobject.createtime = DateTime.Now;
            newmeterobject.delflag = 1;//使用标记
            if (!judeboolen) 
            {
                var addid = await _t_b_watermetersServices.OQuery(c => true, s => new t_b_watermeters()
                {
                    meternum = s.meternum
                }, "meternum desc", 1);
                var tempnum = Convert.ToInt64(addid[0].meternum);
                tempnum += 1;
                newmeterobject.meternum = tempnum.ObjToString();
            }
            await _t_b_watermetersServices.OAdd(newmeterobject);
            return new MessageModel<object>()
            {
                code = 0,
                msg = "ok",
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
            string a = System.Environment.CurrentDirectory;   //当前路径
            string filePath = a + "\\" + "wwwroot\\WatermeterManage\\userinfo.xls";//路径
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
                long userautoaccount = 0;
                var addid = await _t_b_usersServices.OQuery(c => true, s => new t_b_users()
                {
                    autoaccount = s.autoaccount,
                    account = s.account
                }, "autoaccount desc", 1);
                if (addid == null || addid.Count <= 0)
                {
                    userautoaccount = 100000000000;
                }
                else
                {
                    userautoaccount = Convert.ToInt64(addid[0].autoaccount);
                }
                Hashtable tb = new Hashtable();
                tb.Add("account", "用户编号");
                tb.Add("username", "用户名称");
                tb.Add("sex", "性别");
                tb.Add("address", "地址");
                tb.Add("areano", "所属小区");
                tb.Add("usemetertype", "用水类型");
                tb.Add("telephone", "电话");
                tb.Add("workplace","工作单位");
                tb.Add("identification","身份证号");
                List<UserFromExcel> dataExcel = OfficeHelper.fromExcel<UserFromExcel>(tb, path);
                t_b_users userobj = null;
                List<t_b_users> userlist = new List<t_b_users>();
                var typelist = await _t_b_natureServices.OQuery(c => c.bntstate == 1);//所有的用水类型
                foreach (var item in dataExcel)
                {
                    userautoaccount++;
                    userobj = new t_b_users();
                    userobj.username = item.username;
                    userobj.account = item.account;
                    userobj.address = item.address;
                    userobj.usemetertype = typelist.Find(c => c.naturename == item.usemetertype).bntid;
                    userobj.telephone = item.telephone;
                    userobj.areano = item.areano;
                    userobj.accstate = 1;
                    userobj.createby = _users.Name;
                    userobj.createtime = DateTime.Now;
                    userobj.sex = item.sex == "男" ? Convert.ToInt16(0) : Convert.ToInt16(1);
                    userobj.autoaccount = userautoaccount.ObjToString();
                    userlist.Add(userobj);
                }
                await _t_b_usersServices.OAdd(userlist);
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
