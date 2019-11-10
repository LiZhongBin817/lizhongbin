using CDWM_MR.AuthHelper;
using CDWM_MR.Controllers;
using CDWM_MR.IServices;
using CDWM_MR.IServices.Content;
using CDWM_MR_Common.Redis;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CDWM_MR.Tests.Controller_Test
{
    public class LoginTest
    {
        Mock<PermissionRequirement> permissionRequire = new Mock<PermissionRequirement>();
        Mock<IRedisHelper> rtedishelper = new Mock<IRedisHelper>();
        Mock<IsysManageServices> sysmanage = new Mock<IsysManageServices>();
        Mock<Isys_userinfoServices> userinfo = new Mock<Isys_userinfoServices>();
        LoginController logincontroller;

        public LoginTest()
        {
            userinfo.Setup(r => r.Query());
            sysmanage.Setup(r => r.Query());
            //logincontroller = new LoginController(userinfo.Object, sysmanage.Object, rtedishelper.Object, permissionRequire.Object);
        }

        [Fact]
        public async Task loginuser()
        {
            var oVerCode = logincontroller.ShowCode();
            string UserName = "admin", PassWord = "12345", VerCode = oVerCode.ToString();
            var res = await logincontroller.UserLogin(UserName, PassWord, VerCode);
        }
    }
}
