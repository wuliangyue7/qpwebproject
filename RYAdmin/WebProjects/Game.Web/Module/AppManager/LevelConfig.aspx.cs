using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using Game.Kernel;
using Game.Entity.Platform;
using Game.Facade;

namespace Game.Web.Module.AppManager
{
    public partial class LevelAwardConfig : AdminPage
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                BindConfig();
            }
        }

        private void BindConfig()
        {
            //控件绑定
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetList( GrowLevelConfig.Tablename, 1, 10000, "", "ORDER BY LevelID ASC" );
            //litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rpData.DataSource = pagerSet.PageSet;
            rpData.DataBind();
        }
    }
}
