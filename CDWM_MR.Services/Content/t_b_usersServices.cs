using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class t_b_usersServices:BaseServices<t_b_users>, It_b_usersServices
    {
        private readonly It_b_usersRepository Dal;
        public t_b_usersServices(It_b_usersRepository Dal)
        {
            this.Dal = Dal;
            base.BaseDal = Dal;
        }
    }
}
