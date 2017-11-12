using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Facade;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using Game.Utils;

namespace Game.Web.Module.DataAnalysis
{
    public partial class UserActiveStat : AdminPage
    {
        #region 重写属性
        protected override bool IsCreateTmepDir
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region 页面事件
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                txtStartDate.Text = DateTime.Now.AddMonths( -1 ).ToString( "yyyy-MM-dd" );
                txtEndDate.Text = DateTime.Now.AddDays( -1 ).ToString( "yyyy-MM-dd" );

                DayDateBind();
                MatchDateBind();
                UsersStatBind();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click( object sender, EventArgs e )
        {
            DayDateBind();
            MatchDateBind();
        }
        #endregion

        #region 数据绘图

        #region 日统计
        private void DayDateBind()
        {
            //查询数据
            string startDate = CtrlHelper.GetText( txtStartDate );
            string endDate = CtrlHelper.GetText( txtEndDate );
            if( string.IsNullOrEmpty( startDate ) || string.IsNullOrEmpty( endDate ) )
            {
                ShowError( "请输入统计的开始日期及结束日期" );
                return;
            }
            DateTime dtStartDate = Convert.ToDateTime( startDate );
            DateTime dtEndDate = Convert.ToDateTime( endDate );
            int startDateID = Convert.ToInt32( Fetch.GetDateID( dtStartDate ) );
            int endDateID = Convert.ToInt32( Fetch.GetDateID( dtEndDate ) );
            int days = endDateID - startDateID;
            if( days < 1 )
            {
                ShowError( "查询区间必须大于1天" );
                return;
            }
            if( dtEndDate.AddMonths( -6 ) > dtStartDate )
            {
                ShowError( "注册日统计查询跨度不建议超过6个月" );
                return;
            }

            DataSet ds = FacadeManage.aideTreasureFacade.GetActiveUserByDay( startDateID, endDateID );
            DataTable dt = ds.Tables[0];
            Dictionary<string, object> dic = new Dictionary<string, object>();

            //属性定义
            if( ddlType.SelectedValue == "0" )
            {
                Chart1.Series[0].ChartType = SeriesChartType.Line;
                Chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
                Chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            }
            else
            {
                Chart1.Series[0].ChartType = SeriesChartType.Column;
            }
            Chart1.Series[0].BorderWidth = 2;
            Chart1.Series[0].ShadowOffset = 0;

            //绘图
            DataRow[] dr;
            int y = 0;
            int i = 0;
            while( startDateID <= endDateID )
            {
                dr = dt.Select( "DateID=" + startDateID + "" );
                if( dr.Length != 0 )
                {
                    y = dr.Count();
                }
                else
                {
                    y = 0;
                }
                Chart1.Series[0].Points.AddXY( Fetch.ShowDate( startDateID ), y );
                dic.Add( Fetch.ShowDate( startDateID ), y );
                Chart1.Series[0].Points[i].ToolTip = "活跃玩家数：" + y + " 日期：" + Fetch.ShowDate( startDateID );
                i++;
                startDateID = startDateID + 1;
            }

            //绑定列表
            rptData.DataSource = dic;
            rptData.DataBind();
        }
        #endregion

        #region 月统计
        private void MatchDateBind()
        {
            //查询数据

            DataSet ds = FacadeManage.aideTreasureFacade.GetActivieUserByMonth();
            DataTable dt = ds.Tables[0];
            DateTime maxDate = new DateTime();
            DateTime minDate = new DateTime();
            if( dt.Rows.Count > 0 )
            {
                DataRow drMax = dt.AsEnumerable().OrderByDescending( r => r.Field<string>( "StatDate" ) ).FirstOrDefault();
                DataRow drMin = dt.AsEnumerable().OrderBy( r => r.Field<string>( "StatDate" ) ).FirstOrDefault();
                maxDate = Convert.ToDateTime( drMax["StatDate"] );
                minDate = Convert.ToDateTime( drMin["StatDate"] );
            }
            else
            {
                return;
            }

            //定义图属性
            Chart2.Series[0].ChartType = SeriesChartType.Column;
            Chart2.ChartAreas[0].AxisX.Interval = 2;
            Chart2.Series[0].BorderWidth = 2;
            Chart2.Series[0].ShadowOffset = 0;

            //绘图
            DataRow[] dr;
            int y = 0;
            int i = 0;
            while( minDate <= maxDate )
            {
                dr = dt.Select( "StatDate='" + minDate.ToString( "yyyy-MM" ) + "'" );
                if( dr.Length != 0 )
                {
                    y = dr.Count();
                }
                else
                {
                    y = 0;
                }
                Chart2.Series[0].Points.AddXY( minDate.ToString( "yyyy-MM" ), y );
                Chart2.Series[0].Points[i].ToolTip = "玩家数：" + y + " 月份：" + minDate.ToString( "yyyy-MM" );
                Chart2.Series[0].Points[i].Label = y.ToString();
                i++;
                minDate = minDate.AddMonths( 1 );
            }
        }
        #endregion

        /// <summary>
        /// 用户汇总数据
        /// </summary>
        private void UsersStatBind()
        {
            DataSet ds = FacadeManage.aideAccountsFacade.GetUsersNumber();
            DataTable dt = ds.Tables[0];
            if(dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                int userTotal = 0;                  //总用户数
                int activeUserCounts = 0;           //活跃用户数
                int userAVGOnlineTime = 0;          //用户平均在线时长

                userTotal = Convert.ToInt32(dr["UserTotal"]);
                userAVGOnlineTime = Convert.ToInt32(dr["UserAVGOnlineTime"]);
                activeUserCounts = Convert.ToInt32(dr["ActiveUserCounts"]);

                ltActiveUserRate.Text = userTotal == 0 ? "0" : (Convert.ToDecimal(activeUserCounts) / Convert.ToDecimal(userTotal) * 100M).ToString("0.0") + "%";
                ltAVGOnlineTime.Text = userAVGOnlineTime.ToString();
                ltActiveUserCounts.Text = activeUserCounts.ToString();
            }
        }

        #endregion
    }
}