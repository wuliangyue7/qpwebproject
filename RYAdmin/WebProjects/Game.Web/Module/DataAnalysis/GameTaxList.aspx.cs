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
    public partial class GameTaxList : AdminPage
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
            int startDateID = GameRequest.GetQueryInt( "startDateID", 0 );
            int endDateID = GameRequest.GetQueryInt( "endDateID", 0 );
            if( startDateID == 0 || endDateID == 0 ) 
            {
                litNoData.Visible = true;
                rptData.Visible = false;
            }

            string where = string.Format( " WHERE DateID>={0} AND DateID<={1}", startDateID, endDateID );
            PagerSet pagerSet = FacadeManage.aideRecordFacade.GetList( RecordEveryDayData.Tablename, anpPage.CurrentPageIndex, anpPage.PageSize, where, " ORDER BY DateID DESC" );
            anpPage.RecordCount = pagerSet.RecordCount;

            if( pagerSet.PageSet.Tables[0].Rows.Count > 0 )
            {
                rptData.DataSource = pagerSet.PageSet;
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
