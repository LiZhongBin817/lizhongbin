using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_carryoverdatainfoServices:BaseServices<v_carryoverdatainfo>, Iv_carryoverdatainfoServices
    {
        readonly Iv_carryoverdatainfoRepository dal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dal"></param>
        public v_carryoverdatainfoServices(Iv_carryoverdatainfoRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
    }
}
