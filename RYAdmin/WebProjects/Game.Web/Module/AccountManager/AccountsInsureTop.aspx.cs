using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Kernel;
using System.Text;
using Game.Utils;
using System.Data;
using Game.Facade;

namespace Game.Web.Module.AccountManager
{
    public partial class AccountsInsureTop : AdminPage
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack ) 
            {
                BindData();
            }
        }

        //绑定数据
        private void BindData()
        {
            DataSet ds = FacadeManage.aideTreasureFacade.GetUserTransferTop100();
            if( ds.Tables[0].Rows.Count > 0 )
            {
                rptDataList.Visible = true;
                litNoData.Visible = false;
                rptDataList.DataSource = ds;
                rptDataList.DataBind();
            }
            else 
            {
                rptDataList.Visible = true;
                litNoData.Visible = false;
            }
        }
    }
}
