using CDWM_MR.IServices.BASE;
using CDWM_MR.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.IServices.Content
{
   public interface Iv_taskinfoServices:IBaseServices<v_taskinfo>
    {
         Task<object> AutoCreat(int planid);
    }
}
