using AutoMapper;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using CDWM_MR.Model.ViewModels;

namespace CDWM_MR.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<v_bookexcel, bookExcel>();
            CreateMap<UploadPhotoModel, rt_b_photoattachment>();
        }
    }
}
