using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CDWM_MR.Common.Helper;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDWM_MR.Controllers.v1
{
    /// <summary>
    /// 图片问题
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AppPhotoController : ControllerBase
    {
        #region 相关变量
        private readonly IMapper _mapper;

        #endregion

        public AppPhotoController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// 上传图片接口
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> uploadphoto([FromServices]IHostingEnvironment environment)
        {
            var data = new MessageModel<string>();
            try
            {
                IFormFileCollection files = Request.Form.Files;
                UploadPhotoModel uploadmodel = JsonHelper.GetObject<UploadPhotoModel>(Request.Form["UploadPhotoModel"].ObjToString());
                if (files.Count <= 0)
                {
                    data.code = 1001;
                    data.msg = "图片上传个数为0";
                    return data;
                }
                
                foreach (var item in files)
                {
                    uploadmodel.photoext = Path.GetExtension(item.FileName);
                    uploadmodel.photourl = $@"{Path.Combine(environment.WebRootPath, "images")}/类型_{uploadmodel.phototype}/{uploadmodel.taskperiodname}/抄表员_{uploadmodel}/任务单id_{uploadmodel.taskid}";
                    uploadmodel.
                    string file = Path.Combine(uploadmodel.photourl,item.FileName);
                    if (!System.IO.Directory.Exists(uploadmodel.photourl))
                    {
                        System.IO.Directory.CreateDirectory(uploadmodel.photourl);
                    }
                    using (var stream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        await item.CopyToAsync(stream);
                    }

                }
                rt_b_photoattachment photomodel = _mapper.Map<rt_b_photoattachment>(uploadmodel);
                data.code = 0;
                data.msg = "成功！";
                return data;
            }
            catch (Exception ex) 
            {
                data.code = 1001;
                data.msg = ex.Message;
                return data;
            }
        }
    }
}