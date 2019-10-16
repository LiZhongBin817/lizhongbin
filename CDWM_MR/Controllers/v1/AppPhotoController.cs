using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
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
        private readonly Imr_datainfoServices _mr_datainfoservices;
        private readonly Irt_b_faultinfoServices _faultinfoservices;
        private readonly Irb_b_faultprocessServices _faultprocess;
        private readonly Irt_b_photoattachmentServices _photoservices;
        #endregion

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="mr_datainfoservices"></param>
        /// <param name="faultinfo"></param>
        /// <param name="faultprocess"></param>
        /// <param name="photoservices"></param>
        public AppPhotoController(IMapper mapper,Imr_datainfoServices mr_datainfoservices, Irt_b_faultinfoServices faultinfo, Irb_b_faultprocessServices faultprocess, Irt_b_photoattachmentServices photoservices)
        {
            _mapper = mapper;
            _mr_datainfoservices = mr_datainfoservices;
            _faultinfoservices = faultinfo;
            _faultprocess = faultprocess;
            _photoservices = photoservices;
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
                List<UploadPhotoModel> uploadmodel = JsonHelper.GetObject<List<UploadPhotoModel>>(Request.Form["UploadPhotoModel"].ObjToString());
                if (files.Count <= 0)
                {
                    data.code = 1001;
                    data.msg = "图片上传个数为0";
                    return data;
                }
                List<rt_b_photoattachment> addlist = new List<rt_b_photoattachment>();
                for (int i = 0; i < files.Count; i++)
                {
                    uploadmodel[i].photoext = Path.GetExtension(files[i].FileName);
                    uploadmodel[i].photourl = $@"{Path.Combine(environment.WebRootPath, "images")}/类型_{uploadmodel[i].phototype}/{uploadmodel[i].taskperiodname}/抄表员_{uploadmodel[i].readercode}/任务单id_{uploadmodel[i].taskid}";
                    uploadmodel[i].createpeople = "抄表员";//暂时写
                    uploadmodel[i].createtime = DateTime.Now;
                    //uploadmodel.billid
                    //已上传
                    if (uploadmodel[i].isreadupdate == 1)
                    {
                        //表盘抄表
                        if (uploadmodel[i].phototype == 1 || uploadmodel[i].phototype == 2)
                        {
                            var temp = await _mr_datainfoservices.Query(c => c.taskperiodname == uploadmodel[i].taskperiodname && c.meternum == uploadmodel[i].metercode);
                            uploadmodel[i].billid = temp.FirstOrDefault().id;//抄表数据id/mr_datainfo
                        } //现场表况
                        else if (uploadmodel[i].phototype == 4)
                        {
                            var temp2 = await _faultinfoservices.Query(c => c.taskperiodname == uploadmodel[i].taskperiodname && c.meternum == uploadmodel[i].metercode);
                            uploadmodel[i].billid = temp2.FirstOrDefault().id;//故障信息id/rt_b_faultinfo
                        }
                        else
                        {
                            uploadmodel[i].billid = 0;
                        }
                    }
                    string file = Path.Combine(uploadmodel[i].photourl, files[i].FileName);
                    if (!System.IO.Directory.Exists(uploadmodel[i].photourl))
                    {
                        System.IO.Directory.CreateDirectory(uploadmodel[i].photourl);
                    }
                    using (var stream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        await files[i].CopyToAsync(stream);
                    }
                    rt_b_photoattachment photomodel = _mapper.Map<rt_b_photoattachment>(uploadmodel[i]);
                    addlist.Add(photomodel);
                }
                
                await _photoservices.Add(addlist);//添加到图片表
                data.code = 0;
                data.msg = "成功！";
                return data;
            }
            catch (Exception ex) 
            {
                data.code = 1001;
                data.msg = ex.ToString();
                return data;
            }
        }
    }
}