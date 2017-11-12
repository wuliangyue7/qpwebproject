using Game.Entity.Platform;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Kernel;
using Game.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Module.AppManager
{
    public partial class LotteryItemSet : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindConfig();
            }
        }

        private void BindConfig()
        {
            //控件绑定
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(LotteryItem.Tablename, 1, 10000, "", "ORDER BY ItemIndex ASC");
            rpData.DataSource = pagerSet.PageSet;
            rpData.DataBind();
        }
    }
}