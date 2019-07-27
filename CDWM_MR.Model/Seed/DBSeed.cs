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
        /// <param name="CreateDataBase">默认不更新数据库</param>
        public static void SeedAsync(MyContext myContext,bool CreateDataBase = false)
        {
            try
            {
                if (CreateDataBase)
                {
                    Console.WriteLine("未生成数据库！");
                    return;
                }
                // 注意！一定要先手动创建一个【空的数据库】
                // 如果生成过了，第二次，就不用再执行一遍了,注释掉该方法即可
                myContext.CreateTableByEntity(false,
                    typeof(Sys_Menu),
                    typeof(Sys_Interface_Info),
                    typeof(Sys_OperateLog),
                    typeof(Sys_Operation),
                    typeof(Sys_Role),
                    typeof(Sys_Role_Menu),
                    typeof(Sys_UserInfo),
                    typeof(Sys_User_Operation),
                    typeof(Sys_User_Role_Mapper));

                // 后期单独处理某些表
                //myContext.Db.CodeFirst.InitTables(typeof(sysUserInfo));
                //myContext.Db.CodeFirst.InitTables(typeof(Permission)); 
                //myContext.Db.CodeFirst.InitTables(typeof(Advertisement));

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
