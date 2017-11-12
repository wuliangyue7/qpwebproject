using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Entity.PlatformManager;
using Game.Entity;
using Game.Facade;
using Game.Utils;
using Game.Utils.Cache;

namespace Game.Web
{
    public partial class Right : System.Web.UI.Page
    {
        protected string preLogonDate = string.Empty;
        protected string preLogonip = string.Empty;
        protected string preLogonAddress = string.Empty;
        protected int preLogonTimes = 0;
        protected string roleName = string.Empty;
        protected string userName = string.Empty;

        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                //登录判断
              
                Base_Users userExt = new Base_Users();
                userExt = FacadeManage.aidePlatformManagerFacade.GetUserInfoFromCache();
                if( userExt == null || userExt.UserID <= 0 || ( userExt.UserID != ApplicationConfig.SUPER_ADMINISTRATOR_ID && userExt.RoleID <= 0 ) )
                {
                    Response.Redirect( "/Login.aspx?errtype=overtime" );
                    return;
                }

                userExt = FacadeManage.aidePlatformManagerFacade.GetUserByUserID( userExt.UserID );
                userName = userExt.Username;
                preLogonDate = userExt.PreLogintime.ToString();
                preLogonip = userExt.LastLoginIP;
                preLogonTimes = userExt.LoginTimes;
                preLogonAddress = IPQuery.GetAddressWithIP( preLogonip );
                if( userExt.UserID == ApplicationConfig.SUPER_ADMINISTRATOR_ID || userExt.RoleID == 1 )
                {
                    roleName = "超级管理员";
                }
                else
                {
                    roleName = FacadeManage.aidePlatformManagerFacade.GetRolenameByRoleID( userExt.RoleID );
                }
            }
        }
    }
}
