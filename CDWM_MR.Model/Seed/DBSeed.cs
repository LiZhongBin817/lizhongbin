using CDWM_MR.Model.Models;
using CDWM_MR.Model.Seed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.Model.Seed
{
    /// <summary>
    /// 生成数据库
    /// </summary>
    public class DBSeed
    {
        /// <summary>
        /// 异步添加种子数据
        /// </summary>
        /// <param name="myContext"></param>
        public static void SeedAsync(MyContext myContext)
        {
            try
            {
                // 注意！一定要先手动创建一个【空的数据库】
                // 如果生成过了，第二次，就不用再执行一遍了,注释掉该方法即可
                //myContext.CreateTableByEntity(false,
                //    typeof(dispatch_fault_check),
                //    typeof(dispatch_fault_handleinfo),
                //    typeof(dispatch_faultinfo),
                //    typeof(dispatch_faultinfo_history),
                //    typeof(dispatchsheet_info),
                //    typeof(finishturn_check),
                //    typeof(finishturn_datainfo),
                //    typeof(finishturn_datainfo_history),
                //    typeof(mr_b_bookinfo),
                //    typeof(mr_b_reader),
                //    typeof(mr_data_check),
                //    typeof(mr_datainfo),
                //    typeof(mr_datainfo_history),
                //    typeof(mr_planinfo),
                //    typeof(mr_planinfo_history),
                //    typeof(mr_taskinfo),
                //    typeof(mr_taskinfo_history),
                //    typeof(sys_parameter));

                // 后期单独处理某些表
                myContext.Db.CodeFirst.InitTables(typeof(dispatch_fault_check));
                myContext.Db.CodeFirst.InitTables(typeof(dispatch_fault_handleinfo));
                myContext.Db.CodeFirst.InitTables(typeof(dispatch_faultinfo));
                myContext.Db.CodeFirst.InitTables(typeof(dispatch_faultinfo_history));
                myContext.Db.CodeFirst.InitTables(typeof(dispatchsheet_info));
                myContext.Db.CodeFirst.InitTables(typeof(finishturn_check));
                myContext.Db.CodeFirst.InitTables(typeof(finishturn_datainfo));
                myContext.Db.CodeFirst.InitTables(typeof(finishturn_datainfo_history));
                myContext.Db.CodeFirst.InitTables(typeof(mr_b_bookinfo));
                myContext.Db.CodeFirst.InitTables(typeof(mr_b_reader));
                myContext.Db.CodeFirst.InitTables(typeof(mr_data_check));
                myContext.Db.CodeFirst.InitTables(typeof(mr_datainfo));
                myContext.Db.CodeFirst.InitTables(typeof(mr_datainfo_history));
                myContext.Db.CodeFirst.InitTables(typeof(mr_planinfo));
                myContext.Db.CodeFirst.InitTables(typeof(mr_planinfo_history));
                myContext.Db.CodeFirst.InitTables(typeof(mr_taskinfo));
                myContext.Db.CodeFirst.InitTables(typeof(mr_taskinfo_history));
                myContext.Db.CodeFirst.InitTables(typeof(sys_parameter));

                Console.WriteLine("正在生成数据库 ...");

                #region 判断数据库中是否存在该数据库
                //myContext.Db.Queryable<Sys_UserInfo>().Any()
                #endregion


                Console.WriteLine("生成完毕.");
                Console.WriteLine();

            }
            catch (Exception ex)
            {
                throw new Exception("1、注意要先创建空的数据库\n2、" + ex.Message);
            }
        }
    }
}
