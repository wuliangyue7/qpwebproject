using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Utils;

namespace Game.Web
{
    public partial class LoginOut : System.Web.UI.Page
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            FacadeManage.aidePlatformManagerFacade.UserLogout();
        }       

        #endregion
    }
}
