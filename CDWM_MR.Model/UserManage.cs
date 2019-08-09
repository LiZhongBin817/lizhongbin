using CDWM_MR.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDWM_MR.Model
{
    public class EditUser
    {
        public int ID { get; set; }
        public string FUserNumber { get; set; }
        public string FUserName { get; set; }
        public string LoginName { get; set; }
        public string RealName { get; set; }
        public short Sex { get; set; }
        public string MobilePhone { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public short UserType { get; set; }
    }
    public class AddUser
    {
        public string FUserName { get; set; }
        public string LoginName { get; set; }
        public string LoginPassWord { get; set; }
        public string RealName { get; set; }
        public short Sex { get; set; }
        public string MobilePhone { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public short UserType { get; set; }
    }
    public class EditDate
    {
        public string FUserName { get; set; }
        public string LoginName { get; set; }
        public string RealName { get; set; }
        public short Sex { get; set; }
        public string MobilePhone { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public short UserType { get; set; }
        public List<sys_role> roles { get; set; }//用户角色表
        public List<sys_user_role_mapper> role_mapper { get; set; }//用户选中角色表
    }
    public class ShowUser
    {
        public int ID { get; set; }
        public string FUserNumber { get; set; }
        public string FUserName { get; set; }
        public string LoginName { get; set; }
        public string LoginPassWord { get; set; }
        public string RealName { get; set; }
        public string Sex { get; set; }
        public string MobilePhone { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public int page { get; set; } // 当前页标
        public int dataCount { get; set; } // 数据总数
        public int PageSize { set; get; }// 每页大小
        public List<sys_role> roles { get; set; }//用户角色表
    }
}
