using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Facade;
using Game.Entity;
using Game.Utils;
using Game.Entity.PlatformManager;
using Game.Entity.NativeWeb;

namespace Game.Web
{
    public partial class Index : Page
    {
        #region Fields

        protected Base_Users userExt = new Base_Users();

        protected string roleName;

        //标题
        protected string SiteName
        {
            get
            {
                NativeWebFacade nativeWebFacade = new NativeWebFacade();
                ConfigInfo configInfo = nativeWebFacade.GetConfigInfo( EnumerationList.SiteConfigKey.SiteConfig.ToString() );
                if( configInfo == null )
                    return "网站管理后台";
                else
                    return configInfo.Field1+"棋牌后台管理系统";
            }
        }
        #endregion

        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            //登录判断
            PlatformManagerFacade aidePlatformManagerFacade = new PlatformManagerFacade();
            userExt = aidePlatformManagerFacade.GetUserInfoFromCache();
            if( userExt == null || userExt.UserID <= 0 || ( userExt.UserID != ApplicationConfig.SUPER_ADMINISTRATOR_ID && userExt.RoleID <= 0 ) )
            {
                Response.Redirect( "Login.aspx" );
            }
            if( userExt.UserID == ApplicationConfig.SUPER_ADMINISTRATOR_ID || userExt.RoleID == 1 )
            {
                roleName = "超级管理员";
            }
            else
            {
                roleName = aidePlatformManagerFacade.GetRolenameByRoleID( userExt.RoleID );
            }

            Utility.ClearPageClientCache();
        }
        #endregion
    }
}
