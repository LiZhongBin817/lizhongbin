using CDWM_MR.IServices.BASE;
using CDWM_MR.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.IServices.Content
{
    public partial interface Imr_taskinfoServices:IBaseServices<mr_taskinfo>
    {
        Task<object> Gettaskinfo(int? mrid);
    }
}
