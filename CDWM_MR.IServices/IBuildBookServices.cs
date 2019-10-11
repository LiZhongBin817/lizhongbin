using CDWM_MR.IServices.BASE;
using CDWM_MR.Model.Models;

namespace CDWM_MR.IServices
{
    public partial interface IBuildBookServices : IBaseServices<mr_b_bookinfo>
    {
        void BuildEXCELMethodAsync(string bookno);
        void DoworkAsync();
    }
}
