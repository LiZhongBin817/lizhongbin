using AutoMapper;
using CDWM_MR.Model.Models;

namespace CDWM_MR.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            //CreateMap<BlogArticle, BlogViewModels>();
        }
    }
}
