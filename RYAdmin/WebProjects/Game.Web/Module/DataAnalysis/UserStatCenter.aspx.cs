using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Utils;
using System.Web.UI.DataVisualization.Charting;
using Game.Entity.Accounts;
using System.Data;
using Game.Entity.Record;
using Game.Kernel;
using Game.Facade;
using System.Text;

namespace Game.Web.Module.DataAnalysis
{
    public partial class UserStatCenter : AdminPage
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
                txtStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-01";
                txtEndDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

                //绘图
                DayDateBind();
                //用户汇总统计
                UsersStatBind();
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DayDateBind();
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
        }

        /// <summary>
        /// 站点选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSiteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DayDateBind();
            UsersStatBind();
        }
        #endregion

        #region 绘图/绑定数据

        /// <summary>
        /// 付费欲望图绘图
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

            DateTime dtStartDate = Convert.ToDateTime(startDate);
            DateTime dtEndDate = Convert.ToDateTime(endDate);
            int startID = TextUtility.GetDaysDate(Convert.ToDateTime(startDate));
            int endID = TextUtility.GetDaysDate(Convert.ToDateTime(endDate));
            int days = endID - startID;
            if(dtEndDate.AddMonths(-6) > dtStartDate)
            {
                ShowError("付费欲望统计查询跨度不建议超过6个月");
                return;
            }

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
            Chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            //查询数据
            int RMBRate = 1;        //RMB与平台币的汇率
            int CurrencyRate = 1;   //平台币与游戏币的汇率
            SystemStatusInfo systemStatusInfo = FacadeManage.aideAccountsFacade.GetSystemStatusInfo("");
            RMBRate = systemStatusInfo == null ? 1 : systemStatusInfo.StatusValue;
            systemStatusInfo = FacadeManage.aideAccountsFacade.GetSystemStatusInfo("");
            CurrencyRate = systemStatusInfo == null ? 1 : systemStatusInfo.StatusValue;


            string toolTipInfo = string.Empty;
            DataRow[] dr;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" WHERE DateID>{0} AND DateID<{1} ", startID, endID);
            DataSet ds;
            ds = FacadeManage.aideRecordFacade.GetPayDesireByDay(startID, endID);
            DataTable dt = ds.Tables[0];

            //绘图
            string y = string.Empty;
            string x = string.Empty;
            decimal AP = 0;                         //活跃用户数
            decimal CP = 0;                         //充值用户数
            decimal NP = 0;                         //普通用户数
            decimal RP = 0;                         //注册用户
            decimal CLP = 0;                        //充值用户流失用户数
            decimal CG = 0;                         //充值平台币的人民币总金额
            decimal AOH = 0;                        //所有用户平均在线时长
            decimal GT = 0;                         //游戏税收
            decimal TG = 0;                         //游戏币市场价值
            decimal PT = 0;                         //用户付费欲望
            decimal LU = 0;                         //流失用户总数
            decimal Rate = RMBRate * CurrencyRate;  //RMB与游戏币的兑换比例
            decimal Currency = 0;                   //货币总数
            decimal GoldTotal = 0;                  //游戏币总数
            for(int i = startID; i <= endID; i++)
            {
                x = TextUtility.GetDateTimeByDays(i).ToString("yyyyMMdd");
                dr = dt.Select("DateID=" + i + "");
                if(dr.Length != 0)
                {
                    AP = Convert.ToDecimal(dr[0]["ActiveUserTotal"]);
                    CP = Convert.ToDecimal(dr[0]["PayUserTotal"]);
                    RP = Convert.ToDecimal(dr[0]["UserTotal"]);
                    CLP = Convert.ToDecimal(dr[0]["LossPayUserTotal"]);
                    LU = Convert.ToDecimal(dr[0]["LossUserTotal"]);
                    CG = Convert.ToDecimal(dr[0]["PayAmountForCurrency"]);
                    AOH = Convert.ToDecimal(dr[0]["UserAVGOnlineTime"]) / 3600;   //平均在线时长，单位：小时
                    GT = Convert.ToDecimal(dr[0]["GameTaxTotal"]); //游戏税收
                    GoldTotal = Convert.ToDecimal(dr[0]["GoldTotal"]);
                    Currency = Convert.ToDecimal(dr[0]["CurrencyTotal"]);
                    toolTipInfo += "统计时间：" + x + "\n";
                    if (AP == 0 || RP == 0 || CP == 0 || AOH == 0 || CG == 0 || GT == 0 || TG * Rate == 0)
                    {
                        y = "0";
                        toolTipInfo += "用户付费欲望：0";
                    }
                    else
                    {
                        NP = RP - LU - CP + CLP;  //计算出普通用户数
                        TG = (CG - Currency / RMBRate) * RMBRate * CurrencyRate / GoldTotal;                           //计算游戏币的市场价值
                        PT = ((CP + NP * CP / AP * AP / RP * (1 - CLP / CP)) * AOH * GT) / (CG / TG * Rate);     //计算付费欲望
                        y = PT.ToString("0.000000");
                        toolTipInfo += "用户付费欲望：" + y + "\n";
                        toolTipInfo += "游戏币市场价值：" + TG.ToString("0.0000") + "";
                    }
                }
                else
                {
                    y = "0";
                    toolTipInfo += "统计时间：" + x + "\n";
                    toolTipInfo += "用户付费欲望：0";
                }
                Chart1.Series[0].Points.AddXY(x, y);
                Chart1.Series[0].Points[i - startID].ToolTip = toolTipInfo;
                toolTipInfo = string.Empty;
            }
        }

        /// <summary>
        /// 用户汇总数据
        /// </summary>
        private void UsersStatBind()
        {
            DataSet ds = FacadeManage.aideAccountsFacade.GetUsersStat();
            DataTable dt = ds.Tables[0];
            if(dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                int userTotal = 0;                  //总用户数
                int currentMonthRegUserCounts = 0;  //当月注册用户数
                int maxUserRegCounts = 0;           //日注册最高值
                int userAVGOnlineTime = 0;          //用户平均在线时长
                int payMaxAmount = 0;               //最高充值金额
                int currentDateMaxAmount = 0;       //今日最高充值金额
                int payCurrencyAmount = 0;          //充值货币的充值总额度
                int activeUserCounts = 0;           //活跃用户数
                int lossUserCounts = 0;             //流失用户数
                int payUserCounts = 0;              //充值总人数
                int payTwoUserCounts = 0;           //充值大于1次人数
                int payTotalAmount = 0;             //充值总金额
                int maxShareID = 0;                 //充值最多渠道
                int payUserOutflowTotal = 0;        //充值用户流失数
                int VIPPayUserTotal = 0;            //充值金额大于2000RMB玩家数
                long currencyTotal = 0;             //平台币总数
                Int64 goldTotal = 0;                //游戏币总数
                int RMBRate = 1;                    //RMB与平台币的兑换比例
                int currencyRate = 1;               //平台币与游戏币的比例
                decimal UserAVGOnlineTime = 0M;     //玩家平均在线时长
                decimal gameTax = 0M;               //游戏总税收

                userTotal = Convert.ToInt32(dr["UserTotal"]);
                currentMonthRegUserCounts = Convert.ToInt32(dr["CurrentMonthRegUserCounts"]);
                maxUserRegCounts = Convert.ToInt32(dr["MaxUserRegCounts"]);
                userAVGOnlineTime = Convert.ToInt32(dr["UserAVGOnlineTime"]);
                payMaxAmount = Convert.ToInt32(dr["PayMaxAmount"]);
                currentDateMaxAmount = Convert.ToInt32(dr["CurrentDateMaxAmount"]);
                payCurrencyAmount = Convert.ToInt32(dr["PayCurrencyAmount"]);
                activeUserCounts = Convert.ToInt32(dr["ActiveUserCounts"]);
                lossUserCounts = Convert.ToInt32(dr["LossUserCounts"]);
                payUserCounts = Convert.ToInt32(dr["PayUserCounts"]);
                payTwoUserCounts = Convert.ToInt32(dr["PayTwoUserCounts"]);
                payTotalAmount = Convert.ToInt32(dr["PayTotalAmount"]);
                maxShareID = Convert.ToInt32(dr["MaxShareID"]);
                payUserOutflowTotal = Convert.ToInt32(dr["PayUserOutflowTotal"]);
                VIPPayUserTotal = Convert.ToInt32(dr["VIPPayUserTotal"]);
                currencyTotal = Convert.ToInt32(dr["CurrencyTotal"]);
                goldTotal = Convert.ToInt64(dr["GoldTotal"]);
                RMBRate = Convert.ToInt32(dr["RMBRate"]);
                currencyRate = Convert.ToInt32(dr["CurrencyRate"]);
                UserAVGOnlineTime = Convert.ToDecimal(dr["UserAVGOnlineTime"]);
                gameTax = Convert.ToDecimal(dr["GameTax"]);

                ltUserCounts.Text = userTotal.ToString();
                ltCurrentMonthRegUserCounts.Text = currentMonthRegUserCounts.ToString();
                ltNewRate.Text = userTotal == 0 ? "0" : (Convert.ToDecimal(currentMonthRegUserCounts) / Convert.ToDecimal(userTotal) * 100M).ToString("0.0") + "%";
                ltActiveUserRate.Text = userTotal == 0 ? "0" : (Convert.ToDecimal(activeUserCounts) / Convert.ToDecimal(userTotal) * 100M).ToString("0.0") + "%";
                ltLossUserRate.Text = userTotal == 0 ? "0" : (Convert.ToDecimal(lossUserCounts) / Convert.ToDecimal(userTotal) * 100M).ToString("0.0") + "%";
                ltAPRUUser.Text = userTotal == 0 ? "0" : (Convert.ToDecimal(payTotalAmount) / Convert.ToDecimal(userTotal)).ToString("0.00");
                ltRegMaxCounts.Text = maxUserRegCounts.ToString();
                ltAVGOnlineTime.Text = userAVGOnlineTime.ToString();
                ltActiveUserCounts.Text = activeUserCounts.ToString();
                ltLossUserCounts.Text = lossUserCounts.ToString();
                ltPayUserOutflowTotal.Text = payMaxAmount.ToString();
                ltPayUserOutflowRate.Text = currentDateMaxAmount.ToString();
                ltAPRUPayUser.Text = payUserCounts == 0 ? "0" : (Convert.ToDecimal(payTotalAmount) / Convert.ToDecimal(payUserCounts)).ToString("0.00");
                ltPayUserCounts.Text = payUserCounts.ToString();
                ltPayTwoUserCounts.Text = payTwoUserCounts.ToString();
                ltPayRate.Text = userTotal == 0 ? "0" : (Convert.ToDecimal(payUserCounts) / Convert.ToDecimal(userTotal) * 100M).ToString("0.0") + "%";
                ltLossPayUserCounts.Text = payUserOutflowTotal.ToString();
                ltLossPayUserRate.Text = payUserCounts == 0 ? "0" : (Convert.ToDecimal(payUserOutflowTotal) / Convert.ToDecimal(payUserCounts) * 100M).ToString("0.0") + "%";
                ltVIPPayUserTotal.Text = VIPPayUserTotal.ToString();

                decimal AP = activeUserCounts;                  //活跃用户数
                decimal CP = payUserCounts;                     //充值用户数
                decimal RP = userTotal;                         //注册用户
                decimal CLP = payUserOutflowTotal;              //充值用户流失用户数
                decimal CG = payTotalAmount;                    //充值总金额
                decimal AOH = UserAVGOnlineTime / 3600;         //所有用户平均在线时长
                decimal GT = gameTax;                           //游戏税收
                decimal NP = RP - lossUserCounts - CP + CLP;    //普通用户数

                //除数出现0的情况返回
                if (AP == 0 || RP == 0 || CP == 0 || AOH == 0 || CG == 0 || GT == 0 || CLP == 0)
                {
                    return;
                }
                decimal TG = ((payTotalAmount - currencyTotal / currencyRate) * RMBRate * currencyRate) / goldTotal;    //游戏币市场价值
                decimal Rate = RMBRate * currencyRate;  //RMB与游戏币的兑换比例
                if (TG * Rate == 0)
                {
                    return;
                }
                decimal PT = ((CP + (NP * CP / AP) * (AP / RP * (1 - CLP / CP))) * AOH * GT) / (CG / TG * Rate);
                ltPayDesire.Text = PT.ToString();
            }
        }
        #endregion

        #region 公式说明
        /*
            公式：
            普通用户数公式：RP - LU - CP + CLP
            游戏币市场价值公式：TG = ( CG - currency / currencyRate ) / goldTotal;
            付费欲望公式：PT =( ( CP + ( NP * CP / AP ) * ( AP / RP * ( 1 - CLP / CP ) ) ) * AOH * GT ) / ( CG / TG * Rate );
            参数：
            活跃用户数 AP
            充值用户数 CP
            普通用户数 NP
            注册用户 RP
            流失用户数 LU
            充值用户流失用户数 CLP 
            充值平台币的RMB总金额 CG 
            所有用户平均在线时长 AOH
            游戏税收 GT
            游戏币市场价值 TG
            RMB与游戏币的兑换比例 Rate
            RMB与平台币的兑换比例 CurrencyRate
            系统剩余平台币总数 Currency
            系统游戏币总数（不包括机器人） GoldTotal
        */
        #endregion
    }
}