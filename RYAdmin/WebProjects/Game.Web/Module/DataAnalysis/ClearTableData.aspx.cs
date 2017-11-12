using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Kernel;
using Game.Web.UI;
using System.Data;
using Game.Facade;

namespace Game.Web.Module.DataAnalysis
{
    public partial class ClearTableData : AdminPage
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                BindData();
            }
        }

        private void BindData()
        {
            DataSet ds = new DataSet();
            ds = FacadeManage.aideTreasureFacade.StatRecordTable();
            rpData.DataSource = ds;
            rpData.DataBind();
        }
    }
}
