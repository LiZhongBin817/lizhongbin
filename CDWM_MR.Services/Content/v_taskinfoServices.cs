using AutoMapper;
using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Model.ViewModels;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDWM_MR.Services.Content
{
    public class v_taskinfoServices : BaseServices<v_taskinfo>, Iv_taskinfoServices
    {
        private readonly Iv_taskinfoRepository dal;
        private readonly IMapper mapper;
        private readonly Imr_b_bookinfoRepository book_info;
        private readonly Imr_planinfoRepository planinfo;
        private readonly Imr_taskinfoRepository mr_taskinfoRepository;
        private readonly Iv_taskinfoRepository taskinfoRepository;
        public v_taskinfoServices(Iv_taskinfoRepository dal, IMapper map, Imr_b_bookinfoRepository imr_B_BookinfoRepository, Imr_planinfoRepository imr_PlaninfoRepository, Iv_taskinfoRepository iv_TaskinfoRepository, Imr_taskinfoRepository taskinfoRepository)
        {
            this.dal = dal;
            this.mapper = map;
            this.book_info = imr_B_BookinfoRepository;
            this.planinfo = imr_PlaninfoRepository;
            this.taskinfoRepository = iv_TaskinfoRepository;
            this.mr_taskinfoRepository = taskinfoRepository;
            base.BaseDal = dal;
        }

        /// <summary>
        /// 根据id生成v_taskinfo表
        /// </summary>
        /// <returns></returns>
        public async Task<object> AutoCreat(int planid)
        {
            int i = 1;
            List<mr_b_bookinfo> booklist = await book_info.Query();
            List<mr_planinfo> planlist = await planinfo.Query(c => c.ID == planid);
            if (planlist == null || planlist?.Count <= 0)
            {
                return new
                {
                    code = 1001,
                    msg = "缺少计划单！",
                    data = 0
                };
            }
            var temp = planlist[0];
            mr_taskinfo taskinfo = null;
            foreach (var item in booklist)
            {
                taskinfo = new mr_taskinfo();
                taskinfo.bookid = item.id;
                taskinfo.planid = temp.ID;
                taskinfo.readerid = item.readmanid;
                taskinfo.taskstarttime = temp.planstarttime;
                taskinfo.taskendtime = temp.planendtime;
                taskinfo.createpeople = "0";
                taskinfo.createtime = DateTime.Now;
                taskinfo.tasknumber = temp.mplannumber + i.ToString("0000");
                taskinfo.taskname = "任务单00" + i;
                taskinfo.taskperiodname = $"{temp.mplanyear}{temp.mplanmonth}";//任务账期
                taskinfo.taskstatus = 0;
                taskinfo.dowloadstatus = 1;//下载状态
                taskinfo.downloadstarttime = temp.planstarttime;
                taskinfo.downloadendtime = temp.planendtime;
                int taskid = await mr_taskinfoRepository.Add(taskinfo);//添加进入taskid
                await mr_taskinfoRepository.ExecutePro("call Createdatainfo(@ptaskid)", new { ptaskid = taskid });
                i++;
            }
            return new {
                code = 0,
                msg = "成功",
                data = 0
            };
        }
    }
}
