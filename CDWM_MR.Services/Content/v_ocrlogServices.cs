using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public class v_ocrlogServices : BaseServices<v_ocrlog>, Iv_ocrlogServices
    {
        private readonly Iv_ocrlogRepository dal;
        public v_ocrlogServices(Iv_ocrlogRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
