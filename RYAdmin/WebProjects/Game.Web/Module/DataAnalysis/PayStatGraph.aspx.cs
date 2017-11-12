using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;

using Game.Web.UI;
using Game.Facade;
using Game.Utils;
using Game.Entity.Treasure;

namespace Game.Web.Module.DataAnalysis
{
    public partial class PayStatGraph : AdminPage
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

                BindPageData();
                PayStatBind();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click( object sender, EventArgs e )
        {
            BindPageData();
        }

        /// <summary>
        /// 最近30天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMonth_Click( object sender, EventArgs e )
        {
            txtStartDate.Text = DateTime.Now.AddMonths( -1 ).ToString( "yyyy-MM-dd" );
            txtEndDate.Text = DateTime.Now.ToString( "yyyy-MM-dd" );
            BindPageData();
        }

        /// <summary>
        /// 站点选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSiteList_SelectedIndexChanged( object sender, EventArgs e )
        {
            BindPageData();
            PayStatBind();
        }
        #endregion

        #region 绑定数据
        protected void BindPageData()
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
            Chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            //日期计算
            string startDate = CtrlHelper.GetText( txtStartDate );
            string endDate = CtrlHelper.GetText( txtEndDate );
            if( string.IsNullOrEmpty( startDate ) || string.IsNullOrEmpty( endDate ) )
            {
                ShowError( "请输入统计的开始日期及结束日期" );
                return;
            }
            int startID = TextUtility.GetDaysDate( Convert.ToDateTime( startDate ) );
            int endID = TextUtility.GetDaysDate( Convert.ToDateTime( endDate ) );
            int days = endID - startID;
            if( days < 1 )
            {
                ShowError( "查询区间必须大于1天" );
                return;
            }
            if( Convert.ToDateTime( endDate ).AddMonths( -6 ) > Convert.ToDateTime( startDate ) )
            {
                ShowError( "查询时间跨度不建议超过6个月" );
            }

            //数据
            DataSet ds = FacadeManage.aideTreasureFacade.GetPayStatByDay( startDate, Fetch.GetEndTime( Convert.ToDateTime( endDate ) ) );
            DataTable dt = ds.Tables[0];
            DataRow[] dr;

            //绘图
            string toolTipInfo = string.Empty;
            int y = 0;
            string x = string.Empty;
            for( int i = startID; i <= endID; i++ )
            {
                x = TextUtility.GetDateTimeByDays( i ).ToString( "yyyy-MM-dd" );
                dr = dt.Select( "ApplyDate='" + x + "'" );
                if( dr.Length != 0 )
                {
                    y = Convert.ToInt32( dr[0]["PayAmount"] );
                    toolTipInfo += "统计时间：" + x + "\n";
                    toolTipInfo += "充值：" + dr[0]["PayAmount"].ToString() + "\n";
                }
                else
                {
                    y = 0;
                    toolTipInfo += "统计时间：" + x + "\n";
                    toolTipInfo += "充值：0\n";
                }
                Chart1.Series[0].Points.AddXY( x.Replace( "-", "" ), y );
                Chart1.Series[0].Points[i - startID].ToolTip = toolTipInfo;
                toolTipInfo = string.Empty;
            }
        }

        /// <summary>
        /// 绑定充值信息
        /// </summary>
        private void PayStatBind()
        {

            DataSet ds = FacadeManage.aideTreasureFacade.GetPayStat();
            DataTable dt = ds.Tables[0];
            if( dt.Rows.Count > 0 )
            {
                DataRow dr = dt.Rows[0];
                int payUserCounts = 0;                      //充值总人数
                int payTwoUserCounts = 0;                   //充值大于1次人数
                int payMaxAmount = 0;                       //最大充值金额
                UInt64 payTotalAmount = 0;                  //充值总金额
                int maxShareID = 0;                         //充值最多渠道ID
                int currentDateMaxAmount = 0;               //今天充值最多金额
                string payMaxDate = string.Empty;           //最多充值日期
                int userTotal = 0;                          //用户总数
                int payUserOutflowTotal = 0;                //用户流失数
                int VIPpayUserTotal = 0;                    //充值金额大于2000RMB玩家数

                payUserCounts = Convert.ToInt32( dr["PayUserCounts"] );
                payTwoUserCounts = Convert.ToInt32( dr["PayTwoUserCounts"] );
                payMaxAmount = Convert.ToInt32( dr["PayMaxAmount"] );
                payTotalAmount = Convert.ToUInt32( dr["PayTotalAmount"] );
                maxShareID = Convert.ToInt32( dr["maxShareID"] );
                currentDateMaxAmount = Convert.ToInt32( dr["CurrentDateMaxAmount"] );
                payMaxDate = string.IsNullOrEmpty( dr["PayMaxDate"].ToString() ) ? "无充值" : dr["PayMaxDate"].ToString();
                userTotal = Convert.ToInt32( dr["UserTotal"] );
                VIPpayUserTotal = Convert.ToInt32( dr["VIPpayUserTotal"] );

                ltPayUserCounts.Text = payUserCounts.ToString();
                ltPayTwoUserCounts.Text = payTwoUserCounts.ToString();
                ltPayMaxAmount.Text = payMaxAmount.ToString();
                ltPayTotalAmount.Text = payTotalAmount.ToString();
                ltMaxShareName.Text = maxShareID == 0 ? "无充值" : GetShareName( maxShareID ).ToString();
                ltCurrentDateMaxAmount.Text = currentDateMaxAmount.ToString();
                ltPayMaxDate.Text = payMaxDate;
                ltPayUserRateWill.Text = "0";
                ltPayUserOutflowTotal.Text = payUserOutflowTotal.ToString();

                ltAPRUPay.Text = payUserCounts == 0 ? "0" : ( Convert.ToDouble( payTotalAmount ) / Convert.ToDouble( payUserCounts ) ).ToString( "0.00" );
                ltAPRUReg.Text = userTotal == 0 ? "0" : ( Convert.ToDouble( payTotalAmount ) / Convert.ToDouble( userTotal ) ).ToString( "0.00" );
                ltPayUserRate.Text = userTotal == 0 ? "0" : ( ( Convert.ToDouble( payUserCounts ) / Convert.ToDouble( userTotal ) ) * 100 ).ToString( "0.00" ) + "%";
                ltPayUserOutflowRate.Text = payUserCounts == 0 ? "0" : ( ( Convert.ToDouble( payUserOutflowTotal ) / Convert.ToDouble( payUserCounts ) ) * 100 ).ToString( "0.00" ) + "%";
                ltVIPPayUserRate.Text = VIPpayUserTotal.ToString();
            }
        }
        #endregion
    }
}