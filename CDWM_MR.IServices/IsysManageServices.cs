using CDWM_MR.IServices.BASE;
using CDWM_MR.Model;
using CDWM_MR.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CDWM_MR.IServices
{
    public interface IsysManageServices:IBaseServices<sys_userinfo>
    {
        Task<List<sys_role_menu>> GetRoleOperation();

        Task<string> GetuserRole(int userid);

        Task<List<sys_menu>> GetMenuTree();

        List<object> Childmenu(int Parentid, List<sys_menu> MenuList);
        Task<TableModel<object>> Modify(int ID);
        Task<int> ModifyInfo(string JsonDate, string roleid);
        Task<int> AddUserinfo(string JsonDate, string roleid);
        Task<TableModel<object>> GetMenu();
        Task<TableModel<object>> Jude(int RoleID, int MenuID);
        Task<TableModel<object>> GetMenuID(int id);
        Task<TableModel<object>> SaveOperation(int RoleID, string MenuID);
        Task<TableModel<object>> GetOperation(int RoleID, int menuID);
        Task<TableModel<sys_operation>> EditOperations(int RoleID, int MenuID, string OperationID);
    }
}
