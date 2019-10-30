using CDWM_MR.IServices.BASE;
using CDWM_MR.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.IServices.Content
{
   public partial interface Iv_taskinfoServices:IBaseServices<v_taskinfo>
    {
         Task<object> AutoCreat(int planid);
    }
}
