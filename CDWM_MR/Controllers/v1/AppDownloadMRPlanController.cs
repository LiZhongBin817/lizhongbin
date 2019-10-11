using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AppDownloadMRPlanController : ControllerBase
    {
        #region 相关变量
        readonly Imr_taskinfoServices taskServices;
        readonly Iv_taskinfoServices vtaskinfo;
        readonly IHostingEnvironment env;
        #endregion

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="taskservices"></param>
        /// <param name="Taskinfo"></param>
        /// <param name="Env"></param>
        public AppDownloadMRPlanController(Imr_taskinfoServices taskservices, Iv_taskinfoServices Taskinfo, IHostingEnvironment Env)
        {
            taskServices = taskservices;
            vtaskinfo = Taskinfo;
            env = Env;
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
            var t = await vtaskinfo.Query(c => c.readerid == ID);

            return new {
                code = 0,
                msg = "成功",
                data = t
            };
        }

        /// <summary>
        /// 抄表册EXCEL文件下载
        /// </summary>
        /// <param name="bookno">抄表册编号</param>
        /// <returns></returns>
        [HttpGet("{bookno}")]
        [EnableCors("LimitRequests")]
        public IActionResult downLoadMRinfo(string bookno)
        {
            var path = Path.Combine(env.ContentRootPath, "wwwroot", "MR_bookinfo", $"{bookno}.xls");
            if (!System.IO.File.Exists(path))
            {
                return new JsonResult(new {
                    code = 1001,
                    msg = "文件不存在",
                    data = bookno
                });
            }

            //获取文件的ContentType
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            var memi = provider.Mappings[".xls"];
            return PhysicalFile(path, memi, bookno);
        }

        /// <summary>
        /// 下载回调
        /// </summary>
        /// <param name="bookno"></param>
        /// <param name="status">下载状态</param>
        /// <returns></returns>
        [HttpGet("{bookno}/{status}")]
        [EnableCors("LimitRequests")]
        public async Task<object> JudeSuccess(int? bookno,int? status)
        {
            if (status == 0)
            {
                await taskServices.Update(c => new mr_taskinfo() { dowloadstatus = 0 },c => c.bookid == bookno);
                return new
                {
                    code = 0,
                    msg = "下载完成",
                    data = bookno
                };
            }

            return new
            {
                code = 1001,
                msg = "下载失败",
                data = bookno
            };

        }

        /// <summary>
        /// 接受上传图片
        /// </summary>
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
                        data = $"文件个数:{files.Count()}",
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