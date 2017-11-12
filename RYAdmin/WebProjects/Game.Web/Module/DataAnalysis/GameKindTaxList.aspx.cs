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
using Game.Entity.Record;
using Game.Facade;


namespace Game.Web.Module.DataAnalysis
{
    public partial class GameKindTaxList : AdminPage
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                BindData();
            }
        }

        protected void anpNews_PageChanged( object sender, EventArgs e )
        {
            BindData();
        }

        //绑定数据
        private void BindData()
        {
            int dateID = GameRequest.GetQueryInt( "dateID", 0 );

            if( dateID == 0 )
            {
                litNoData.Visible = true;
                rptData.Visible = false;
            }

            DataSet ds = FacadeManage.aideRecordFacade.GetGameTaxListByDateID( dateID );

            if( ds.Tables[0].Rows.Count > 0 )
            {
                rptData.DataSource = ds;
                rptData.DataBind();
                litNoData.Visible = false;
                rptData.Visible = true;
            }
            else
            {
                litNoData.Visible = true;
                rptData.Visible = false;
            }
        }
    }
}
