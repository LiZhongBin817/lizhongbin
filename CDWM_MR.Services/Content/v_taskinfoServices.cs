using AutoMapper;
using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Model.ViewModels;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Services.Content
{
   public class v_taskinfoServices:BaseServices<v_taskinfo>,Iv_taskinfoServices
    {
        private readonly Iv_taskinfoRepository dal;
        private readonly IMapper mapper;
        private readonly Imr_b_bookinfoRepository book_info;
        private readonly Imr_planinfoRepository planinfo;
        private readonly Imr_taskinfoRepository mr_taskinfoRepository;
        private readonly Iv_taskinfoRepository taskinfoRepository;
        public v_taskinfoServices(Iv_taskinfoRepository dal,IMapper map,Imr_b_bookinfoRepository imr_B_BookinfoRepository,Imr_planinfoRepository imr_PlaninfoRepository,Iv_taskinfoRepository iv_TaskinfoRepository,Imr_taskinfoRepository taskinfoRepository)
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
        /// 自动生成v_taskinfo表
        /// </summary>
        /// <returns></returns>
        public async Task<object> AutoCreat()
        {
            int i = 1;
            List<mr_b_bookinfo> booklist = await book_info.Query();
            List<mr_planinfo> planlist = await planinfo.Query();
            int ID = Convert.ToInt32(planlist[planlist.Count - 1].ID);//v_taskinfo中的planid,每次自动生成最新的
            foreach (var item in booklist)
            {
                mr_taskinfo taskinfo = new mr_taskinfo();
                taskinfo.bookid = item.ID;
                taskinfo.planid = ID;
                taskinfo.readerid = 1;
                taskinfo.taskstarttime = DateTime.Now;
                taskinfo.taskendtime = DateTime.Now.AddDays(30);
                taskinfo.createpeople =" 1";
                taskinfo.createtime = DateTime.Now;
                taskinfo.tasknumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()+i.ToString();
                taskinfo.taskname = "任务单00" + i;
                taskinfo.taskperiodname = "1";//任务账期
                taskinfo.taskstatus = 0;//
                taskinfo.dowloadstatus = 0;//下载状态
                taskinfo.downloadstarttime = DateTime.Now;
                taskinfo.downloadendtime = DateTime.Now.AddDays(30);

                i++;
                await mr_taskinfoRepository.Add(taskinfo);
            }
            List<v_taskinfo>tasklist=await taskinfoRepository.Query();
            return tasklist.Count;

        }
    }
}
