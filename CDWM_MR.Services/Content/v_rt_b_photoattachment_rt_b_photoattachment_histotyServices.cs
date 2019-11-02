using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_rt_b_photoattachment_rt_b_photoattachment_histotyServices:BaseServices<v_rt_b_photoattachment_rt_b_photoattachment_histoty>, Iv_rt_b_photoattachment_rt_b_photoattachment_histotyServices
    {
        readonly Iv_rt_b_photoattachment_rt_b_photoattachment_histotyRepository dal;
        public v_rt_b_photoattachment_rt_b_photoattachment_histotyServices(Iv_rt_b_photoattachment_rt_b_photoattachment_histotyRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
