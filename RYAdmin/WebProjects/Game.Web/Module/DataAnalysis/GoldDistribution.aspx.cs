using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;

using Game.Web.UI;
using Game.Facade;

namespace Game.Web.Admin.Module.Data_Analysis
{
    public partial class GoldDistribution : AdminPage
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
                BindData();
            }
        }

        protected void ddlSiteList_SelectedIndexChanged( object sender, EventArgs e )
        {
            BindData();
        }
        #endregion

        #region 绑定数据
        private void BindData()
        {
            //检查目录
            DataSet ds = FacadeManage.aideTreasureFacade.GetGoldDistribution();

            //显示图表
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;    //开启3D效果
            Chart1.Height = 350;
            Chart1.Width = 800;
            Chart1.BorderSkin.SkinStyle = BorderSkinStyle.None;    //设置图表边框为浮雕效果
            Chart1.BorderlineDashStyle = ChartDashStyle.Dash;    //设置图表边框为实线
            Chart1.BorderlineWidth = 1;    //设置图表边框的宽度
            Chart1.Series[0].ChartType = SeriesChartType.Pie;    //设置图表类型为饼图
            Chart1.Series[0]["PieLabelStyle"] = "Outside";   //将文字移到外侧
            Chart1.Series[0]["PieLineColor"] = "Black";   //绘制黑色的连线
            if( ds.Tables[0].Rows.Count > 0 )
            {
                DataRow row = ds.Tables[0].Rows[0];
                string[] xValue = { "1万以下", "1万-10万", "10万-50万", "50万-100万", "100万-500万", "500万-1000万", "1000万-3000万", "3000万以上" };
                Dictionary<string, int> dic = new Dictionary<string, int>();
                int userTotal = Convert.ToInt32( row[8] );    //总玩家数
                int userArea = 0;   //某个游戏币区域玩家数
                for( int i = 0; i < 8; i++ )
                {
                    userArea = Convert.ToInt32( row[i] );
                    if( userArea > 0 )
                    {
                        dic.Add( xValue[i] + "(" + ( Convert.ToDouble( userArea ) / Convert.ToDouble( userTotal ) * 100 ).ToString( "0.00" ) + "%)", Convert.ToInt32( row[i] ) );
                    }
                }
                Chart1.Series[0].Points.DataBindXY( dic.Keys, dic.Values );  //绘图

                //绑定数据
                Label1.Text = row[0].ToString();
                Label2.Text = row[1].ToString();
                Label3.Text = row[2].ToString();
                Label4.Text = row[3].ToString();
                Label5.Text = row[4].ToString();
                Label6.Text = row[5].ToString();
                Label7.Text = row[6].ToString();
                Label8.Text = row[7].ToString();
            }

            //货币系统计算
            if( ds.Tables[1].Rows.Count > 0 )
            {
                DataRow rowSta = ds.Tables[1].Rows[0];
                decimal GoldTotal = Convert.ToDecimal( rowSta["GoldTotal"] );               //系统总游戏币
               
                tbCalc.Visible = true;
                tbRefer.Visible = true;
                divGoldCount.Visible = false;
                lbGoldTotal.Text = GoldTotal.ToString( "N0" );
                decimal PayAmountTotal = Convert.ToDecimal( rowSta["PayAmountTotal"] );     //总充值金额
                decimal CurrencyTotal = Convert.ToDecimal( rowSta["CurrencyTotal"] );       //玩家身上平台币
                decimal RMBRate = Convert.ToDecimal( rowSta["RMBRate"] );                   //RMB与平台币的兑换比例
                decimal CurrencyRate = Convert.ToDecimal( rowSta["CurrencyRate"] );         //平台币与游戏币的比例

                lbGoldRate.Text = ( RMBRate * CurrencyRate ).ToString();
                lbGoldRate2.Text = ( RMBRate * CurrencyRate ).ToString();
                if( GoldTotal == 0 || CurrencyRate == 0 )
                {
                    return;
                }
                decimal goldValue = ( ( PayAmountTotal - CurrencyTotal / CurrencyRate ) * RMBRate * CurrencyRate ) / GoldTotal;
                lbGoldTrueValue.Text = goldValue.ToString( "0.00" );
                decimal expansionRate = goldValue == 0 ? 0 : 1 / goldValue;
                if( expansionRate <= 2.5M )
                {
                    lbExpansionRate.Text = expansionRate.ToString( "0.00" );
                }
                else
                {
                    lbExpansionRate.Text = expansionRate.ToString( "0.00" ) + "<span style='color:red;'>     警告！通胀率大于2.5<span>";
                }
                lbGoldEstimatedValue.Text = ( goldValue * goldValue * 2.5M ).ToString( "0.00" );
                
            }
        }
        #endregion

        #region 公式说明
        /*
        公式：
        游戏币市场价值公式： 市场价值 = ( ( PayAmountTotal - CurrencyTotal / CurrencyRate ) * RMBRate * CurrencyRate ) / GoldTotal;
        参数：
        充值平台币的RMB总金额 PayAmountTotal 
        所有用户平均在线时长 AOH
        平台币与游戏币的兑换比例 RMBRate
        RMB与平台币的兑换比例 CurrencyRate
        系统剩余平台币总数 CurrencyTotal
        游戏币总数 GoldTotal
        */
        #endregion
    }
}