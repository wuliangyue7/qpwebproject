using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Entity.PlatformManager;
using Game.Facade;
using System.Text;
using System.Data;

namespace Game.Web.Tools
{
    public partial class UserFaceList : System.Web.UI.Page
    {
        protected string systemFaceList = string.Empty; //系统头像

        protected void Page_Load( object sender, EventArgs e )
        {
            //验证登陆
            if( !FacadeManage.aidePlatformManagerFacade.CheckedUserLogon() )
            {
                return;
            }

            FaceBind();
        }

        /// <summary>
        /// 绑定头像
        /// </summary>
        private void FaceBind()
        {
            StringBuilder sb = new StringBuilder();

            //系统头像
            for( int i = 0; i < 200; i++ )
            {
                sb.Append("<a id=\"lnkFaceID" + i + "\" href=\"javascript:void(0);\" onclick=\"javascript:PI(" + i + ",'/gamepic/Avatar" + i + ".png',1);\" hidefocus=\"true\"><img src=\"../gamepic/Avatar" + i + ".png\" alt='' /></a>");
            }
            systemFaceList = sb.ToString();

            //自定义头像
            int userID = Utils.GameRequest.GetQueryInt( "param", 0 );
            if( userID == 0 )
            {
                divCustomFace.Visible = false;
                return;
            }
            DataSet ds = new DataSet();
            ds = FacadeManage.aideAccountsFacade.GetAccountsFaceList( userID );
            if( ds.Tables[0].Rows.Count > 0 )
            {
                rptCustom.DataSource = ds;
                rptCustom.DataBind();
            }
            else
            {
                divCustomFace.Visible = false;
            }
        }
    }
}
