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
using System.Drawing;

namespace Game.Web.Module.DataAnalysis
{
    public partial class UserRegStat : AdminPage
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
                txtStartDate.Text = DateTime.Now.AddMonths( -1 ).ToString( "yyyy-MM" ) + "-01";
                txtEndDate.Text = DateTime.Now.ToString( "yyyy-MM-dd" );

                //日统计
                DayDateBind();
                //月统计
                MatchDateBind();
                UsersStatBind();
            }
        }

        protected void btnQuery_Click( object sender, EventArgs e )
        {
            DayDateBind();
            MatchDateBind();
        }

        protected void btnToday_Click( object sender, EventArgs e )
        {
            txtStartDate.Text = DateTime.Now.ToString( "yyyy-MM-dd" );
            txtEndDate.Text = DateTime.Now.ToString( "yyyy-MM-dd" );
            DayDateBind();
            MatchDateBind();
        }

        protected void btnTomorrow_Click( object sender, EventArgs e )
        {
            txtStartDate.Text = DateTime.Now.AddDays( -1 ).ToString( "yyyy-MM-dd" );
            txtEndDate.Text = DateTime.Now.AddDays( -1 ).ToString( "yyyy-MM-dd" );
            DayDateBind();
            MatchDateBind();
        }

        protected void ddlSiteList_SelectedIndexChanged( object sender, EventArgs e )
        {
            DayDateBind();
            MatchDateBind();
        }
        #endregion

        #region 绑定数据绘图
        /// <summary>
        /// 按日统计
        /// </summary>
        private void DayDateBind()
        {
            //属性定义
            int type = Convert.ToInt32( ddlType.SelectedValue );
            if( type == 0 )
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

            //时间
            string start = txtStartDate.Text.Trim();
            string end = txtEndDate.Text.Trim();
            if( string.IsNullOrEmpty( start ) || string.IsNullOrEmpty( end ) )
            {
                ShowError( "请输入统计的开始日期及结束日期" );
                return;
            }
            DateTime startDT = Convert.ToDateTime( start );
            DateTime endDT = Convert.ToDateTime( end );
            TimeSpan ts = endDT - startDT;
            if( endDT.AddMonths( -6 ) > startDT )
            {
                ShowError( "注册日统计查询跨度不建议超过6个月" );
                return;
            }
            int days = Convert.ToInt32( ts.TotalDays );
            Dictionary<string, object> dic = new Dictionary<string, object>();

            //绘图
            if( days > 0 )
            {
                //X轴为日期
                DataSet ds = FacadeManage.aideAccountsFacade.GetRegUserByDays( start, end );
                DataRow[] dr;
                int y = 0;
                for( int i = 0; i <= days; i++ )
                {
                    string date = startDT.AddDays( i ).ToString( "yyyyMMdd" );
                    string dateID = Fetch.GetDateID( startDT.AddDays( i ) );
                    dr = ds.Tables[0].Select( "DateID=" + dateID );
                    if( dr.Length != 0 )
                    {
                        y = Convert.ToInt32( dr[0]["RegisterCount"] );
                    }
                    else
                    {
                        y = 0;
                    }
                    dic.Add( date, y );
                    Chart1.Series[0].Points.AddXY( date, y );
                    Chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                    Chart1.Series[0].Points[i].ToolTip = "注册数：" + y + " 日期：" + date;
                }
            }
            else
            {
                //X轴为小时
                DataSet ds = FacadeManage.aideAccountsFacade.GetRegUserByHours( Fetch.GetStartTime( startDT ), Fetch.GetEndTime( startDT ) );
                DataRow[] dr;
                int y = 0;
                string x = string.Empty;
                for( int i = 0; i <= 23; i++ )
                {
                    x = i.ToString() + ":00";
                    dr = ds.Tables[0].Select( "StatDate='" + i + "'" );
                    if( dr.Length != 0 )
                    {
                        y = Convert.ToInt32( dr[0]["RegisterCount"] );
                    }
                    else
                    {
                        y = 0;
                    }
                    dic.Add( x, y );
                    Chart1.Series[0].Points.AddXY( x, y );
                    Chart1.Series[0].Points[i].ToolTip = "注册数：" + y + " 时间段：" + i + ":00-" + ( i + 1 ) + ":00";
                    Chart1.ChartAreas[0].AxisX.Interval = 1;
                    Chart1.Series[0].Points[i].Label = y.ToString();
                }
            }

            //绑定列表数据
            rptData.DataSource = dic;
            rptData.DataBind();
        }

        /// <summary>
        /// 按月统计
        /// </summary>
        private void MatchDateBind()
        {
            //查询数据
            DataSet ds = FacadeManage.aideAccountsFacade.GetRegUserByMonth();
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

            //定义图属性
            Chart2.Series[0].ChartType = SeriesChartType.Line;
            Chart2.Series[0].MarkerStyle = MarkerStyle.Circle;
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
                    y = Convert.ToInt32( dr[0]["RegisterCount"] );
                }
                else
                {
                    y = 0;
                }
                Chart2.Series[0].Points.AddXY( minDate.ToString( "yyyyMM" ), y );
                Chart2.Series[0].Points[i].ToolTip = "注册数：" + y + " 月份：" + minDate.ToString( "yyyy-MM" );
                Chart2.Series[0].Points[i].Label = y.ToString();
                i++;
                minDate = minDate.AddMonths( 1 );
            }

            //定义X轴显示
            if( i > 20 )
            {
                Chart2.ChartAreas[0].AxisX.Interval = 2;
            }
            else
            {
                Chart2.ChartAreas[0].AxisX.Interval = 1;
            }
        }

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
                int currentMonthRegUserCounts = 0;  //当月注册用户数
                int maxUserRegCounts = 0;           //日注册最高值

                userTotal = Convert.ToInt32(dr["UserTotal"]);
                currentMonthRegUserCounts = Convert.ToInt32(dr["CurrentMonthRegUserCounts"]);
                maxUserRegCounts = Convert.ToInt32(dr["MaxUserRegCounts"]);

                ltUserCounts.Text = userTotal.ToString();
                ltCurrentMonthRegUserCounts.Text = currentMonthRegUserCounts.ToString();
                ltNewRate.Text = userTotal == 0 ? "0" : (Convert.ToDecimal(currentMonthRegUserCounts) / Convert.ToDecimal(userTotal) * 100M).ToString("0.0") + "%";
                ltRegMaxCounts.Text = maxUserRegCounts.ToString();
            }
        }
        #endregion
    }
}