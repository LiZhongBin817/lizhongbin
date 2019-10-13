	//----------v_mr_datainfo开始----------
    

using System;
using CDWM_MR.Model.Models;
using CDWM_MR.IServices.BASE;
using CDWM_MR.Model;
using System.Threading.Tasks;

namespace CDWM_MR.IServices.Content
{	
	/// <summary>
	/// v_mr_datainfoServices
	/// </summary>	
    public partial interface Iv_mr_datainfoServices :IBaseServices<v_mr_datainfo>
	{
       Task<TableModel<object>> ShowMRPath(string month, string date, string name, int page = 1, int limit = 20);


    }
}

	//----------v_mr_datainfo结束----------
	