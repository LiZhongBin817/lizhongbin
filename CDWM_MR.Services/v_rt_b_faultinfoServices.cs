using CDWM_MR.IRepository;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Model.ViewModels;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services
{
   public partial class v_rt_b_faultinfoServices:BaseServices<v_rt_b_faultinfo>,Iv_rt_b_faultinfoServices
    {
        private readonly Iv_rt_b_faultinfoRepository dal;
        public v_rt_b_faultinfoServices(Iv_rt_b_faultinfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
