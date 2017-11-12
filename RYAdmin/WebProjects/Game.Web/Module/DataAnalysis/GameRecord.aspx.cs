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
using Game.Entity.Accounts;
using System.Data;
using Game.Facade;

namespace Game.Web.Module.DataAnalysis
{
    public partial class GameRecord : AdminPage
    {
        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                BindServerList();
                txtStartDate.Text = DateTime.Now.ToString( "yyyy-MM-" ) + "01";
                txtEndDate.Text = DateTime.Now.ToString( "yyyy-MM-dd" );
            }
        }

        protected void anpPage_PageChanged( object sender, EventArgs e )
        {
            BindData();
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        private void SetCondition( string startDate, string endDate )
        {
            StringBuilder condition = new StringBuilder(" WHERE 1=1");
            int serverID = Convert.ToInt32( ddlServerID.SelectedValue );
            if( serverID != 0 )
                condition.AppendFormat( " AND ServerID = {0} ", serverID );
            if( !string.IsNullOrEmpty( startDate ) )
                condition.AppendFormat( " AND ConcludeTime >= '{0}' ", startDate );
            if( !string.IsNullOrEmpty( endDate ) )
                condition.AppendFormat( " AND ConcludeTime < '{0}'", Convert.ToDateTime( endDate ).AddDays( 1 ).ToString( "yyyy-MM-dd" ) );
            ViewState["SearchItems"] = condition.ToString();
        }

        //查询
        protected void btnQuery_Click( object sender, EventArgs e )
        {
            string startDate = CtrlHelper.GetText( txtStartDate );
            string endDate = CtrlHelper.GetText( txtEndDate );

            SetCondition( startDate, endDate );
            BindData();
        }

        //今天
        protected void btnQueryTD_Click( object sender, EventArgs e )
        {
            string startDate = Fetch.GetTodayTime().Split( '$' )[0].ToString();
            string endDate = Fetch.GetTodayTime().Split( '$' )[1].ToString();

            CtrlHelper.SetText( txtStartDate, Convert.ToDateTime( startDate ).ToString( "yyyy-MM-dd" ) );
            CtrlHelper.SetText( txtEndDate, Convert.ToDateTime( endDate ).ToString( "yyyy-MM-dd" ) );

            SetCondition( startDate, endDate );
            BindData();
        }

        //昨天
        protected void btnQueryYD_Click( object sender, EventArgs e )
        {
            string startDate = Fetch.GetYesterdayTime().Split( '$' )[0].ToString();
            string endDate = Fetch.GetYesterdayTime().Split( '$' )[1].ToString();

            CtrlHelper.SetText( txtStartDate, Convert.ToDateTime( startDate ).ToString( "yyyy-MM-dd" ) );
            CtrlHelper.SetText( txtEndDate, Convert.ToDateTime( endDate ).ToString( "yyyy-MM-dd" ) );

            SetCondition( startDate, endDate );
            BindData();
        }

        //本周
        protected void btnQueryTW_Click( object sender, EventArgs e )
        {
            string startDate = Fetch.GetWeekTime().Split( '$' )[0].ToString();
            string endDate = Fetch.GetWeekTime().Split( '$' )[1].ToString();

            CtrlHelper.SetText( txtStartDate, Convert.ToDateTime( startDate ).ToString( "yyyy-MM-dd" ) );
            CtrlHelper.SetText( txtEndDate, Convert.ToDateTime( endDate ).ToString( "yyyy-MM-dd" ) );

            SetCondition( startDate, endDate );
            BindData();
        }

        //上周
        protected void btnQueryYW_Click( object sender, EventArgs e )
        {
            string startDate = Fetch.GetLastWeekTime().Split( '$' )[0].ToString();
            string endDate = Fetch.GetLastWeekTime().Split( '$' )[1].ToString();

            CtrlHelper.SetText( txtStartDate, Convert.ToDateTime( startDate ).ToString( "yyyy-MM-dd" ) );
            CtrlHelper.SetText( txtEndDate, Convert.ToDateTime( endDate ).ToString( "yyyy-MM-dd" ) );

            SetCondition( startDate, endDate );
            BindData();
        }
        #endregion

        #region 数据绑定

        //绑定数据
        private void BindData()
        {
            AccountsInfo account = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID( IntParam );
            if( account == null )
                return;

            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetRecordDrawInfoList( anpPage.CurrentPageIndex, anpPage.PageSize, SearchItems, Orderby );
            anpPage.RecordCount = pagerSet.RecordCount;

            if( pagerSet.PageSet.Tables[0].Rows.Count > 0 )
            {
                rptDataList.DataSource = pagerSet.PageSet;
                rptDataList.DataBind();
                rptDataList.Visible = true;
                litNoData.Visible = false;
            }
            else
            {
                rptDataList.Visible = false;
                litNoData.Visible = true;
            }
        }

        //绑定房间
        private void BindServerList()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetGameRoomInfoList( 1, Int32.MaxValue, "WHERE ServerType=1", "ORDER BY GameID ASC" );

            if( pagerSet.PageSet.Tables[0].Rows.Count > 0 )
            {
                ddlServerID.DataSource = pagerSet.PageSet;
                ddlServerID.DataTextField = "ServerName";
                ddlServerID.DataValueField = "ServerID";
                ddlServerID.DataBind();
            }

            ddlServerID.Items.Insert( 0, new ListItem( "全部金币房间", "0" ) );
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchItems
        {
            get
            {
                if( ViewState["SearchItems"] == null )
                {
                    StringBuilder condition = new StringBuilder();
                    ViewState["SearchItems"] = condition.ToString();
                }
                return (string)ViewState["SearchItems"];
            }
            set
            {
                ViewState["SearchItems"] = value;
            }
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        public string Orderby
        {
            get
            {
                if( ViewState["Orderby"] == null )
                {
                    ViewState["Orderby"] = "ORDER BY DrawID DESC";
                }

                return (string)ViewState["Orderby"];
            }
            set
            {
                ViewState["Orderby"] = value;
            }
        }

        #endregion
    }
}
