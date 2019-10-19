using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDWM_MR.Controllers.v1
{
    /// <summary>
    /// 抄表员数据统计
    /// </summary>
    [Route("api/[controller]")]
    public class AppMeterReaderData : Controller
    {
        #region  相关变量
        readonly Imr_b_readerServices _mr_b_readerServices;
        readonly Iv_mr_book_reader_lqServices _v_mr_book_reader_lqServices;
        readonly Iv_rt_b_recheckServices _v_rt_b_recheckServices;
        #endregion

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="mr_b_readerServices"></param>
        /// <param name="v_mr_book_reader_lqServices"></param>
        /// <param name="v_rt_b_recheckServices"></param>
        public AppMeterReaderData(Imr_b_readerServices mr_b_readerServices, Iv_mr_book_reader_lqServices v_mr_book_reader_lqServices, Iv_rt_b_recheckServices v_rt_b_recheckServices)
        {
            _mr_b_readerServices = mr_b_readerServices;
            _v_mr_book_reader_lqServices = v_mr_book_reader_lqServices;
            _v_rt_b_recheckServices = v_rt_b_recheckServices;
        }

        #region 抄表员数据统计接口
        /// <summary>
        /// 抄表员数据统计接口
        /// </summary>
        /// <param name="readerid">抄表员id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMeterReaderDateSum")]
        [AllowAnonymous]//允许所有都访问
        public async Task<object> GetMeterReaderDateSum(int readerid)
        {
            //查出抄表员上传的所有数据
            List<v_mr_book_reader_lq> bookreaderlist = await _v_mr_book_reader_lqServices.Query(c => c.readerid == readerid);
            //查出审核表中的数据
            List<v_rt_b_recheck> checklist = await _v_rt_b_recheckServices.Query(c=>c.readerid== readerid);
            //应抄用户
            int AllShouldReading = 0;
            for (int i=0;i< bookreaderlist.Count();i++)
            {
                AllShouldReading += Convert.ToInt32(bookreaderlist[i].contectusernum);
            }
            //已抄用户
            int AlreadyReading = checklist.Count();
            //抄见水量
            double CopySeeWater = 0;
            for(int i=0;i< checklist.Count();i++)
            {
                CopySeeWater += Convert.ToDouble(checklist[i].recheckdata);
            }
            var data = new
            {
                AllShouldReading= AllShouldReading,
                AlreadyReading= AlreadyReading,
                CopySeeWater= CopySeeWater,
            };
            return data;
        }
        #endregion

        #region  修改抄表员联系电话接口
        /// <summary>
        /// 修改抄表员联系电话接口
        /// </summary>
        /// <param name="count">登录账号</param>
        /// <param name="newphone">新号码</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyMeterReaderPhone")]
        [AllowAnonymous]//允许所有都访问
        public async Task<int> ModifyMeterReaderPhone(string count, string newphone)
        {
            int Status = 0;
            await _mr_b_readerServices.Update(c => new mr_b_reader
            {
                telephone = newphone
            }, c => c.appcount == count);
            Status = 1;
            return Status;
        }
        #endregion
       
    }
}
