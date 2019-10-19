using CDWM_MR.IRepository.Content;
using CDWM_MR.IServices.Content;
using CDWM_MR.Model.Models;
using CDWM_MR.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Services.Content
{
    public partial class v_mr_book_reader_lqServices:BaseServices<v_mr_book_reader_lq>, Iv_mr_book_reader_lqServices
    {
        private readonly Iv_mr_book_reader_lqRepository Dal;
        public v_mr_book_reader_lqServices(Iv_mr_book_reader_lqRepository Dal)
        {
            this.Dal = Dal;
            base.BaseDal = Dal;
        }
    }
}
