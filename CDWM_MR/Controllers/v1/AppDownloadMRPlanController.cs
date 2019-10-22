using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDWM_MR.Controllers.v1
{
    /// <summary>
    /// APP下载抄表计划
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AppDownloadMRPlanController : ControllerBase
    {
        #region 相关变量
        readonly Imr_taskinfoServices taskServices;
        readonly Iv_taskinfoServices vtaskinfo;
        private readonly Iv_bookexcelServices _v_bookexcelservices;
        private readonly Iv_downloaddatainfoServices _v_downloaddatainfoservices;
        readonly IHostingEnvironment env;
        #endregion

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="taskservices"></param>
        /// <param name="Taskinfo"></param>
        /// <param name="Env"></param>
        /// <param name="vbookexcel"></param>
        /// <param name="downloaddatainfoservices"></param>
        public AppDownloadMRPlanController(Imr_taskinfoServices taskservices, Iv_taskinfoServices Taskinfo, IHostingEnvironment Env, Iv_bookexcelServices vbookexcel,Iv_downloaddatainfoServices downloaddatainfoservices)
        {
            taskServices = taskservices;
            vtaskinfo = Taskinfo;
            env = Env;
            _v_bookexcelservices = vbookexcel;
            _v_downloaddatainfoservices = downloaddatainfoservices;
        }

        /// <summary>
        /// 下载抄表计划
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("{ID}")]
        [EnableCors("LimitRequests")]
        public async Task<object> DownLoadMR(int? ID)
        {
            var t = await vtaskinfo.Query(c => c.readerid == ID && c.dowloadstatus == 1);
            if (t == null || t?.Count <= 0)
            {
                return new {
                    code =1001,
                    msg = "无数据！",
                    data = 0
                };
            }
            
            return new {
                code = 0,
                msg = "成功",
                data = t
            };
        }

        /// <summary>
        /// 下载抄表信息
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        [HttpGet("{taskid}")]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<List<v_downloaddatainfo>>> downLoadMRinfo(int? taskid)
        {
            var data = new MessageModel<List<v_downloaddatainfo>>();
            var judedata =await vtaskinfo.Query(c => c.taskid == taskid);
            if (judedata == null) 
            {
                data.code = 1001;
                data.msg = "没有对应的任务单！";
                return data;
            }
            var temp = judedata.FirstOrDefault();
            if (DateTime.Now < temp.downloadstarttime) 
            {
                data.code = 1001;
                data.msg = "没有到对应的下载日期！";
                return data;
            }
            var rdata =await _v_downloaddatainfoservices.Query(c => c.bookno == temp.bookno);
            if (rdata.Count <= 0)
            {
                data.code = 1001;
                data.msg = "此抄表册内没有水表信息或该抄表册不存在！";
                return data;
            }
            data.code = 0;
            data.msg = "成功！";
            data.data = rdata;
            return data;
            //var path = Path.Combine(env.ContentRootPath, "wwwroot", "MR_bookinfo", $"{bookno}.xls");
            //if (!System.IO.File.Exists(path))
            //{
            //    return new JsonResult(new {
            //        code = 1001,
            //        msg = "文件不存在",
            //        data = bookno
            //    });
            //}

            ////获取文件的ContentType
            //var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            //var memi = provider.Mappings[".xls"];
            //return PhysicalFile(path, memi, bookno);
        }

        /// <summary>
        /// 下载回调
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("{taskid}/{status}")]
        [EnableCors("LimitRequests")]
        public async Task<object> JudeSuccess(int? taskid,int? status)
        {
            if (status == 0)
            {
                await taskServices.Update(c => new mr_taskinfo() { dowloadstatus = 0 },c => c.id == taskid);
                return new
                {
                    code = 0,
                    msg = "下载完成",
                    data = taskid
                };
            }

            return new
            {
                code = 1001,
                msg = "下载失败",
                data = taskid
            };

        }

        /// <summary>
        /// 接受上传图片(测试)
        /// </summary>
        /// <param name="Test"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpPost]
        [EnableCors("LimitRequests")]
        public async Task<MessageModel<string>> UploadImg([FromServices]IHostingEnvironment environment)
        {
            var data = new MessageModel<string>();
            string path = string.Empty;
            string foldername = "images";
            IFormFileCollection files = null;
            string test = "抱歉";
            try
            {
                files = Request.Form.Files;
                test = Request.Form["Test"].ObjToString();
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
            var allowType = new string[] { "image/jpg", "image/png", "image/jpeg" };

            string folderpath = Path.Combine(environment.WebRootPath, foldername);
            if (!System.IO.Directory.Exists(folderpath))
            {
                System.IO.Directory.CreateDirectory(folderpath);
            }

            if (files.Any(c => allowType.Contains(c.ContentType)))
            {
                if (files.Sum(c => c.Length) <= 1024 * 1024 * 4)
                {
                    foreach (var item in files)
                    {
                        string strpath = Path.Combine(foldername,item.FileName);
                        path = Path.Combine(environment.WebRootPath, strpath);

                        using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            await item.CopyToAsync(stream);
                        }
                    }

                    data = new MessageModel<string>()
                    {
                        code = 0,
                        msg = "上传成功",
                        data = $"文件个数:{files.Count()}{test}",
                    };
                    return data;
                }
                else
                {
                    data.msg = "图片过大";
                    return data;
                }
            }
            else

            {
                data.msg = "图片格式错误";
                return data;
            }
        }

    }
}