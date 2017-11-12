using Game.Entity.Treasure;
using Game.Facade;
using Game.Kernel;
using Game.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Module.AgentManager
{
    public partial class ChildRevenueInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-") + "01";
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                ViewState["SearchItems"] = string.Format("WHERE UserID={0}", IntParam);
                ChildRevenueDataBind();
            }
        }

        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            ChildRevenueDataBind();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewState["SearchItems"] = string.Format("WHERE UserID={0}", IntParam);
            ChildRevenueDataBind();
        }

        /// <summary>
        /// 查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string startDate = txtStartDate.Text.Trim();
            string endDate = txtEndDate.Text.Trim();

            ViewState["SearchItems"] = string.Format("WHERE UserID={0} AND DateID BETWEEN '{1}' AND '{2}'", IntParam, Fetch.GetDateID(Convert.ToDateTime(startDate)), Fetch.GetDateID(Convert.ToDateTime(endDate)));
            ChildRevenueDataBind();
        }

        //本日
        protected void btnQueryTD_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetTodayTime().Split('$')[0].ToString();
            string endDate = Fetch.GetTodayTime().Split('$')[1].ToString();

            ViewState["SearchItems"] = string.Format("WHERE UserID={0} AND DateID BETWEEN '{1}' AND '{2}'", IntParam, Fetch.GetDateID(Convert.ToDateTime(startDate)), Fetch.GetDateID(Convert.ToDateTime(endDate)));
            ChildRevenueDataBind();
        }

        //本周
        protected void btnQueryTW_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetWeekTime().Split('$')[0].ToString();
            string endDate = Fetch.GetWeekTime().Split('$')[1].ToString();

            ViewState["SearchItems"] = string.Format("WHERE UserID={0} AND DateID BETWEEN '{1}' AND '{2}'", IntParam, Fetch.GetDateID(Convert.ToDateTime(startDate)), Fetch.GetDateID(Convert.ToDateTime(endDate)));
            ChildRevenueDataBind();
        }

        //本月
        protected void btnQueryTM_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetMonthTime().Split('$')[0].ToString();
            string endDate = Fetch.GetMonthTime().Split('$')[1].ToString();

            ViewState["SearchItems"] = string.Format("WHERE UserID={0} AND DateID BETWEEN '{1}' AND '{2}'", IntParam, Fetch.GetDateID(Convert.ToDateTime(startDate)), Fetch.GetDateID(Convert.ToDateTime(endDate)));
            ChildRevenueDataBind();
        }

        //本年
        protected void btnQueryTY_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetYearTime().Split('$')[0].ToString();
            string endDate = Fetch.GetYearTime().Split('$')[1].ToString();

            ViewState["SearchItems"] = string.Format("WHERE UserID={0} AND DateID BETWEEN '{1}' AND '{2}'", IntParam, Fetch.GetDateID(Convert.ToDateTime(startDate)), Fetch.GetDateID(Convert.ToDateTime(endDate)));
            ChildRevenueDataBind();
        }
        #endregion

        #region 数据绑定

        //绑定数据
        private void ChildRevenueDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(RecordUserRevenue.Tablename, anpPage.CurrentPageIndex, anpPage.PageSize, SearchItems, Orderby);
            anpPage.RecordCount = pagerSet.RecordCount;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
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

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchItems
        {
            get
            {
                if (ViewState["SearchItems"] == null)
                {
                    StringBuilder condition = new StringBuilder();
                    condition.Append(" WHERE 1=1 ");
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
                if (ViewState["Orderby"] == null)
                {
                    ViewState["Orderby"] = "ORDER BY CollectDate DESC";
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