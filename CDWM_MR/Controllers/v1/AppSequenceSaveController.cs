using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDWM_MR.IRepository.Content;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CDWM_MR.Controllers.v1
{
    /// <summary>
    /// 抄表册用户顺序保存
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class AppSequenceSaveController : ControllerBase
    {
        readonly Imr_book_meterRepository _Book_MeterRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AppSequenceSaveController(Imr_book_meterRepository book_MeterRepository)
        {
            _Book_MeterRepository = book_MeterRepository;
        }

        /// <summary>
        /// 抄表册用户顺序保存
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        [HttpPost("{bookid}")]
        [AllowAnonymous]//允许所有都访问       
        public async Task<object> SequenceSave(int bookid,[FromBody] Dictionary<string, int> JsonData)
        {
            //Dictionary<string, int> keyValues = JsonConvert.DeserializeObject<Dictionary<string, int>>(JsonData);
            List<mr_book_meter> bookInfo = await _Book_MeterRepository.Query(c=>c.bookid==bookid);
            bookInfo.ForEach(c => {c.meterseq = JsonData.ToList().Find(s => s.Key == c.useraccount).Value;});
            bool b=await _Book_MeterRepository.Updateable(bookInfo);
            return new JsonResult(new
            {
                code = 0,
                msg = b==true?"保存成功":"保存失败",
                data = bookInfo
            });
        }
    }
}