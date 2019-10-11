using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Tasks.Job;
using CDWM_MR_Common.Redis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CDWM_MR.Controllers
{
    /// <summary>
    /// 抄表册管理
    /// </summary>
    public class BookManageController : ControllerBase
    {
        public readonly Iv_b_bookinfoServices _Iv_b_bookinfoServices;
        public readonly It_b_regionsServices _It_b_regionsServices;
        public readonly Imr_b_bookinfoServices _Imr_b_bookinfoServices;
        public readonly Imr_book_meterServices _Imr_book_meterServices;
        public readonly Iv_wateruserinfoServices _Iv_wateruserinfoServices;
        public readonly Imr_book_readerServices _Imr_book_readerServices;
        public readonly Imr_b_readerServices _Imr_b_readerServices;
        public readonly Iv_b_bookuserinfoServices _Iv_b_bookuserinfoServices;
        public readonly It_b_watermetersServices _It_b_watermetersServices;
        private readonly IRedisHelper _redishelpr;
        private readonly IBuildBookServices _buildservices;

        #region 构造函数注入
        public BookManageController(IRedisHelper redis, Iv_b_bookinfoServices Iv_b_bookinfoService, Imr_book_meterServices Imr_book_meterService, Iv_wateruserinfoServices Iv_wateruserinfoService, Imr_book_readerServices Imr_book_readerService, Imr_b_readerServices Imr_b_readerService, Iv_b_bookuserinfoServices v_b_bookuserinfoService, Imr_b_bookinfoServices Imr_b_bookinfoService, It_b_watermetersServices It_b_watermetersService, It_b_regionsServices It_b_regionsService, IBuildBookServices buildservices)
        {
            _Iv_b_bookinfoServices = Iv_b_bookinfoService;
            _Imr_book_meterServices = Imr_book_meterService;
            _Iv_wateruserinfoServices = Iv_wateruserinfoService;
            _Imr_book_readerServices = Imr_book_readerService;
            _Imr_b_readerServices = Imr_b_readerService;
            _Iv_b_bookuserinfoServices = v_b_bookuserinfoService;
            _Imr_b_bookinfoServices = Imr_b_bookinfoService;
            _It_b_watermetersServices = It_b_watermetersService;
            _It_b_regionsServices = It_b_regionsService;
            _redishelpr = redis;
            _buildservices = buildservices;
        }
        #endregion

        #region 显示所有抄表册
        /// <summary>
        /// 显示所有抄表册信息
        /// </summary>
        /// <param name="bookno">抄表册编号</param>
        /// <param name="bookname">抄表册名称</param>
        /// <param name="page">分页参数</param>
        /// <param name="limit">分页参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("t_b_bookinfoShow")]
        [AllowAnonymous]//允许所有都访问
        public async Task<TableModel<object>> t_b_bookinfoShow(string bookno, string bookname, int page = 1, int limit = 10)
        {
            PageModel<v_b_bookinfo> bookinfo = new PageModel<v_b_bookinfo>();
            Expression<Func<v_b_bookinfo, bool>> wherelamda = c => true;
            #region lamda表达式拼接
            if (!string.IsNullOrEmpty(bookname))
            {
                wherelamda = PredicateExtensions.And<v_b_bookinfo>(wherelamda, c => c.bookname.Contains(bookname));
            }
            if (!string.IsNullOrEmpty(bookno))
            {
                wherelamda = PredicateExtensions.And<v_b_bookinfo>(wherelamda, c => c.bookno.Contains(bookno));
            }
            #endregion
            bookinfo = await _Iv_b_bookinfoServices.QueryPage(wherelamda, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                data = bookinfo.data,
                count = bookinfo.dataCount,
                msg = "ok"
            };
        }
        #endregion

        #region 分配用户
        /// <summary>
        /// 分配用户
        /// </summary>
        /// <param name="meternum">水表编号</param>
        /// <param name="bookid">抄表册ID</param>
        /// <param name="account">用户编号</param>
        /// <param name="bookno">抄表册编号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SelectUser")]
        [AllowAnonymous]//允许所有都访问
        public async Task<MessageModel<object>> SelectUser(List<string> meternum, int bookid, string[] account, string bookno)
        {
            List<mr_book_meter> list = new List<mr_book_meter>();
            for (int i = 0; i < meternum.Count; i++)
            {
                mr_book_meter selectuser = new mr_book_meter();
                selectuser.meterseq = 1;
                selectuser.bookid = bookid;
                selectuser.watermeternumber = meternum[i];
                selectuser.useraccount = account[i];
                selectuser.createpeople = "李忠斌";
                selectuser.createtime = DateTime.Now;
                list.Add(selectuser);
            }
            await _Imr_book_meterServices.Add(list);
            int length = account.Length;
            await _Imr_b_bookinfoServices.Update(c => new mr_b_bookinfo
            {
                contectusernum = length
            }, c => c.id == bookid);
            await _It_b_watermetersServices.OUpdate(c => new t_b_watermeters
            {
                bookno = bookno
            }, c => meternum.Contains(c.meternum));
            return new MessageModel<object>()
            {
                code = 0,
                data = null,
                msg = "ok",

            };
        }
        #endregion

        #region 添加抄表册
        /// <summary>
        /// 添加一条抄表册信息
        /// </summary>
        /// <param name="bookno">抄表册编号</param>
        /// <param name="bookname">抄表册名称</param>
        /// <param name="regionno">区域编号</param>
        /// <param name="readperiod">抄表周期</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddBook")]
        [AllowAnonymous]//允许所有都访问
        public async Task<MessageModel<object>> AddBook(string bookno, string bookname, string regionno, int readperiod)
        {
            int bookid;
            mr_b_bookinfo bookinfo = new mr_b_bookinfo();
            List<mr_b_bookinfo> LastID = new List<mr_b_bookinfo>();
            bookinfo.bookname = bookname;
            bookinfo.bookno = bookno;
            bookinfo.allotstatus = 0;
            bookinfo.createpeople = "李忠斌";
            bookinfo.createtime = DateTime.Now;
            bookinfo.contectusernum = 0;
            bookinfo.regionno = regionno;
            bookinfo.readperiod = readperiod;
            #region 在mr_book_reader这个中间表中添加数据
            await _Imr_b_bookinfoServices.Add(bookinfo);
            LastID = await _Imr_b_bookinfoServices.Query();
            bookid = LastID[LastID.Count - 1].id;
            mr_book_reader book_reader = new mr_book_reader();
            book_reader.bookid = bookid;
            book_reader.readerid = 0;
            book_reader.createpeople = "李忠斌";
            book_reader.createtime = DateTime.Now;
            await _Imr_book_readerServices.Add(book_reader);

            await _redishelpr.ListLeftPushAsync<string>("CDWM_BuildExcel", bookno);//添加一个任务进入队列

            #endregion
            return new MessageModel<object>()
            {
                data = null,
                code = 0,
                msg = "ok"
            };
        }
        #endregion

        #region 显示所有的区域信息
        /// <summary>
        /// 显示所有的区域信息
        /// </summary>
        /// <param name="page">分页参数</param>
        /// <param name="limit">分页参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegionShow")]
        [AllowAnonymous]//允许所有都访问
        public async Task<TableModel<object>> RegionShow(int page = 1, int limit = 10)
        {
            PageModel<t_b_regions> regions = new PageModel<t_b_regions>();
            regions = await _It_b_regionsServices.OQueryPage(c => true, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                data = regions.data,
                msg = "ok",
                count = regions.dataCount
            };
        }
        #endregion

        #region 分配抄表员
        [HttpPost]
        [Route("SelectReader")]
        [AllowAnonymous]//允许所有都访问
        public async Task<MessageModel<object>> SelectReader(int bookid, int readerid)
        {
            await _Imr_book_readerServices.Update(c => new mr_book_reader
            {
                readerid = readerid
            }, c => c.bookid == bookid);
            await _Imr_b_bookinfoServices.Update(c => new mr_b_bookinfo
            {
                readmanid = readerid.ToString()
            }, c => c.id == bookid); ;
            return new MessageModel<object>()
            {
                msg = "ok",
                code = 0,
                data = null
            };
        }
        #endregion

        #region 显示所有的抄表员
        /// <summary>
        /// 显示所有的抄表员
        /// </summary>
        /// <param name="readmanid">抄表员id</param>
        /// <param name="mrreadernumber">抄表员编号</param>
        /// <param name="mrreadername">抄表员名称</param>
        /// <param name="page">分页参数</param>
        /// <param name="limit">分页参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("waterrederinfo")]
        [AllowAnonymous]//允许所有都访问
        public async Task<TableModel<object>> waterrederinfo(string readmanid, string mrreadernumber, string mrreadername, int page = 1, int limit = 10)
        {
            PageModel<mr_b_reader> readerinfo = new PageModel<mr_b_reader>();//分页的对象
            Expression<Func<mr_b_reader, bool>> wherelamda = c => true;
            if (!string.IsNullOrEmpty(readmanid))
            {
                wherelamda = c => c.id.ToString() == readmanid;
            }
            if (!string.IsNullOrEmpty(mrreadernumber))
            {
                wherelamda = PredicateExtensions.And<mr_b_reader>(wherelamda, c => c.mrreadernumber == mrreadernumber);
            }
            if (!string.IsNullOrEmpty(mrreadername))
            {
                wherelamda = PredicateExtensions.And<mr_b_reader>(wherelamda, c => c.mrreadername == mrreadername);
            }
            readerinfo = await _Imr_b_readerServices.QueryPage(wherelamda, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                data = readerinfo.data,
                count = readerinfo.dataCount,
                msg = "ok"

            };
        }
        #endregion

        #region 显示所有的用户信息
        /// <summary>
        /// 显示所有的用户信息
        /// </summary>
        /// <param name="regionplace">区域名称</param>
        /// <param name="areaname">小区名称</param>
        /// <param name="disstatus">分配状态</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AllUserInfoShow")]
        [AllowAnonymous]//允许所有都访问
        public async Task<TableModel<object>> AllUserInfoShow(string regionplace, string areaname, int disstatus = 3, int page = 1, int limit = 10)
        {
            PageModel<v_wateruserinfo> userinfo = new PageModel<v_wateruserinfo>();//分页的对象
            Expression<Func<v_wateruserinfo, bool>> wherelamda = c => true;
            if (!string.IsNullOrEmpty(regionplace))
            {
                wherelamda = PredicateExtensions.And<v_wateruserinfo>(wherelamda, c => c.regionplace == regionplace);
            }
            if (!string.IsNullOrEmpty(areaname))
            {
                wherelamda = PredicateExtensions.And<v_wateruserinfo>(wherelamda, c => c.areaname == areaname);
            }
            if (disstatus == 0)
            {
                wherelamda = PredicateExtensions.And<v_wateruserinfo>(wherelamda, c => c.bookno == null || c.bookno == "");
            }
            if (disstatus == 1)
            {
                wherelamda = PredicateExtensions.And<v_wateruserinfo>(wherelamda, c => c.bookno != null && c.bookno != "");
            }
            userinfo = await _Iv_wateruserinfoServices.QueryPage(wherelamda, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                data = userinfo.data,
                count = userinfo.dataCount,
                msg = "ok"

            };
        }
        #endregion

        #region 显示关联的用户
        /// <summary>
        /// 显示关联的用户
        /// </summary>
        /// <param name="bookid">抄表册ID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AssoUserShow")]
        [AllowAnonymous]//允许所有都访问
        public async Task<TableModel<object>> AssoUserShow(int bookid, int page = 1, int limit = 10)
        {
            PageModel<v_b_bookuserinfo> assouser = new PageModel<v_b_bookuserinfo>();
            Expression<Func<v_b_bookuserinfo, bool>> wherelamda = c => true;
            #region lamda表达式拼接
            if (bookid > 0)
            {
                wherelamda = PredicateExtensions.And<v_b_bookuserinfo>(wherelamda, c => c.bookid == bookid);
            }
            #endregion
            assouser = await _Iv_b_bookuserinfoServices.QueryPage(wherelamda, page, limit, "");
            return new TableModel<object>()
            {
                code = 0,
                data = assouser.data,
                count = assouser.dataCount,
                msg = "ok"
            };
        }
        #endregion

        #region 修改抄表册信息
        /// <summary>
        /// 修改抄表册信息
        /// </summary>
        /// <param name="ID">抄表册ID</param>
        /// <param name="bookname">抄表册名称</param>
        /// <param name="regionno">区域</param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditBook")]
        [AllowAnonymous]//允许所有都访问
        public async Task<MessageModel<object>> EditBook(int ID, string bookname, string regionno)
        {
            await _Imr_b_bookinfoServices.Update(c => new mr_b_bookinfo
            {
                bookname = bookname,
                regionno = regionno
            }, c => c.id == ID);
            return new MessageModel<object>()
            {
                code = 0,
                data = null,
                msg = "ok"
            };
        }
        #endregion

        #region  删除抄表册信息
        [HttpPost]
        [Route("DeleteBook")]
        [AllowAnonymous]//允许所有都访问
        public async Task<MessageModel<object>> DeleteBook(int ID, string bookno)
        {
            Expression<Func<mr_book_meter, bool>> lambda = c => c.bookid == ID;
            Expression<Func<mr_book_reader, bool>> lambda1 = c => c.bookid == ID;
            await _Imr_b_bookinfoServices.DeleteById(ID);//删除mr_b_bookinfo中的信息
            object[] deleteID = { };
            List<mr_book_meter> bookobj1 = await _Imr_book_meterServices.Query(lambda);
            List<mr_book_reader> bookobj2 = await _Imr_book_readerServices.Query(lambda1);
            for (int i = 0; i < bookobj1.Count; i++)
            {
                await _Imr_book_meterServices.Delete(bookobj1[i]);//删除mr_book_meter中的信息              
            }
            for (int i = 0; i < bookobj2.Count; i++)
            {
                await _Imr_book_readerServices.Delete(bookobj2[i]);//删除mr_book_meter中的信息              
            }
            await _It_b_watermetersServices.OUpdate(c => new t_b_watermeters
            {
                bookno = null
            }, c => c.bookno == bookno);//修改t_b_watermeters中的信息
            return new MessageModel<object>()
            {
                code = 0,
                data = null,
                msg = "ok"
            };
        }
        #endregion

        /// <summary>
        /// 生成EXCEL文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("BuildExcel")]
        [AllowAnonymous]//允许所有都访问
        public async Task<string> BuildExcel()
        {

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();//启动单元
            #region 任务一 创建任务单Excel文件
            //创建作业
            IJobDetail buildexcel = JobBuilder.Create<BuildBookExcel>()
                .WithIdentity("BuildBookExcel", "task1")
                .WithDescription("创建抄表册Excel文件")
                .Build();
            buildexcel.JobDataMap.Add("buildService", _buildservices);//为方法传入参数
            //创建时间策略
            ITrigger triggerbuildexcel = TriggerBuilder.Create()
                              .WithIdentity("BuildBookExceltigger", "task1")
                              .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(10)))
                             .WithCronSchedule("0 0/2 * * * ?")
                             .WithDescription("生成抄表册EXCEL文件！")
                             .Build();
            await scheduler.ScheduleJob(buildexcel, triggerbuildexcel);
            return "OK";
        }
    }
    #endregion
}
