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

        Task<List<sys_role_menu>> GetbtninfoData();

        Task<string> GetuserRole(int userid);

        Task<List<sys_menu>> GetMenuTree();

        List<object> Childmenu(int Parentid, List<sys_menu> MenuList);
        Task<TableModel<object>> Modify(int ID);

        Task<TableModel<object>> GetMenu();

        Task<TableModel<object>> Jude(int RoleID, int MenuID);
        Task<TableModel<object>> GetMenuID(int id);

        Task<TableModel<object>> SaveOperation(int RoleID, string MenuID);

        Task<TableModel<object>> GetOperation(int RoleID, int menuID);

        Task<TableModel<sys_operation>> EditOperations(int RoleID, int MenuID, string OperationID);
        Task<int> ModifyInfo(string JsonDate, int[] roleid);
        Task<int> AddUserinfo(string JsonDate, int [] roleid);
        Task<List<object>> GetTree(int id);
        Task<object> GetMenuInfo(int id);
        Task<bool> AddMenu(string json);
        Task<bool> DelMenu(int id);
        Task<bool> Power(string adddata, string deldata, string modifdata, string seedata, int id);
    }
}
