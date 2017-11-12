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

namespace Game.Web.Module.DataAnalysis
{
    public partial class UserLossStat : AdminPage
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-") + "01";
                txtEndDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

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
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DayDateBind();
            MatchDateBind();
        }

        /// <summary>
        /// 查询最近30天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button4_Click(object sender, EventArgs e)
        {
            txtStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            DayDateBind();
            MatchDateBind();
        }

        /// <summary>
        /// 玩家类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DayDateBind();
            MatchDateBind();
        }
        #endregion

        #region 数据分析
        /// <summary>
        /// 按日统计
        /// </summary>
        private void DayDateBind()
        {
            //查询数据
            string startDate = txtStartDate.Text.Trim();
            string endDate = txtEndDate.Text.Trim();
            if(string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                ShowError("请输入统计的开始日期及结束日期");
                return;
            }
            DataSet ds = new DataSet();
            DateTime dtStartDate = Convert.ToDateTime(startDate);
            DateTime dtEndDate = Convert.ToDateTime(endDate);
            int startID = TextUtility.GetDaysDate(Convert.ToDateTime(startDate));
            int endID = TextUtility.GetDaysDate(Convert.ToDateTime(endDate));
            int days = endID - startID;
            if(days < 1)
            {
                ShowError("查询区间必须大于1天");
                return;
            }
            if(dtEndDate.AddMonths(-6) > dtStartDate)
            {
                ShowError("注册日统计查询跨度不建议超过6个月");
                return;
            }

            ds = FacadeManage.aideRecordFacade.GetLossUserByDay(startID, endID);
            DataTable dt = ds.Tables[0];
            Dictionary<string, object> dic = new Dictionary<string, object>();

            //属性定义
            int type = Convert.ToInt32(ddlType.SelectedValue);
            if(type == 0)
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
            string toolTipInfo = string.Empty;
            DataRow[] dr;
            int y = 0;
            string x = string.Empty;
            for(int i = startID; i <= endID; i++)
            {
                x = TextUtility.GetDateTimeByDays(i).ToString("yyyyMMdd");
                dr = dt.Select("DateID=" + i + "");
                if(dr.Length != 0)
                {
                    if(rblUserType.SelectedValue == "0")
                    {
                        y = Convert.ToInt32(dr[0]["LossUser"]);
                    }
                    else
                    {
                        y = Convert.ToInt32(dr[0]["LossPayUser"]);
                    }
                    toolTipInfo += "统计时间：" + x + "\n";
                    toolTipInfo += "流失用户数：" + y + "\n";
                }
                else
                {
                    y = 0;
                    toolTipInfo += "统计时间：" + x + "\n";
                    toolTipInfo += "流失用户数：0\n";
                }
                Chart1.Series[0].Points.AddXY(x, y);
                Chart1.Series[0].Points[i - startID].ToolTip = toolTipInfo;
                dic.Add(x, y);
                toolTipInfo = string.Empty;
            }

            //绑定列表
            rptData.DataSource = dic;
            rptData.DataBind();
            rptData.Visible = true;
        }

        /// <summary>
        /// 按月统计
        /// </summary>
        private void MatchDateBind()
        {
            //查询数据
            DataSet ds = FacadeManage.aideRecordFacade.GetLossUserByMonth();
            DataTable dt = ds.Tables[0];
            DateTime maxDate = new DateTime();
            DateTime minDate = new DateTime();
            if(dt.Rows.Count > 0)
            {
                DataRow drMax = dt.AsEnumerable().OrderByDescending(r => r.Field<string>("CollectDate")).FirstOrDefault();
                DataRow drMin = dt.AsEnumerable().OrderBy(r => r.Field<string>("CollectDate")).FirstOrDefault();
                maxDate = Convert.ToDateTime(drMax["CollectDate"]);
                minDate = Convert.ToDateTime(drMin["CollectDate"]);
            }
            else
            {
                return;
            }

            //定义图属性
            Chart2.Series[0].ChartType = SeriesChartType.Column;
            Chart2.ChartAreas[0].AxisX.Interval = 1;
            Chart2.Series[0].BorderWidth = 2;
            Chart2.Series[0].ShadowOffset = 0;
            Chart2.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;

            //绘图
            DataRow[] dr;
            string toolTipInfo = string.Empty;
            int y = 0;
            int i = 0;
            while(minDate <= maxDate)
            {
                dr = dt.Select("CollectDate='" + minDate.ToString("yyyy-MM") + "'");
                if(dr.Length != 0)
                {
                    if(rblUserType.SelectedValue == "0")
                    {
                        y = Convert.ToInt32(dr[0]["LossUserTotal"]);
                    }
                    else
                    {
                        y = Convert.ToInt32(dr[0]["LossPayUserTotal"]);
                    }
                }
                else
                {
                    y = 0;
                }
                toolTipInfo += "统计时间：" + minDate.AddMonths(-1).ToString("yyyyMM") + "\n";
                toolTipInfo += "流失数：" + y + "\n";
                Chart2.Series[0].Points.AddXY(minDate.AddMonths(-1).ToString("yyyyMM"), y);
                Chart2.Series[0].Points[i].ToolTip = toolTipInfo;
                toolTipInfo = string.Empty;
                Chart2.Series[0].Points[i].Label = y.ToString();
                i++;
                minDate = minDate.AddMonths(1);
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
                int lossUserCounts = 0;             //流失用户数

                userTotal = Convert.ToInt32(dr["UserTotal"]);
                lossUserCounts = Convert.ToInt32(dr["LossUserCounts"]);

                ltLossUserCounts.Text = lossUserCounts.ToString();
                ltLossUserRate.Text = userTotal == 0 ? "0" : (Convert.ToDecimal(lossUserCounts) / Convert.ToDecimal(userTotal) * 100M).ToString("0.0") + "%";
            }
        }
        #endregion
    }
}