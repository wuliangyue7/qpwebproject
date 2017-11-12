using Game.Entity.Accounts;
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
    public partial class AgentChildInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-") + "01";
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                ViewState["SearchItems"] = string.Format("WHERE SpreaderID={0}", IntParam);
                AgentChildDataBind();
            }
        }

        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            AgentChildDataBind();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewState["SearchItems"] = string.Format("WHERE SpreaderID={0}", IntParam);
            AgentChildDataBind();
        }

        /// <summary>
        /// 查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string startDate = txtStartDate.Text.Trim();
            string endDate = txtEndDate.Text.Trim();

            ViewState["SearchItems"] = string.Format("WHERE SpreaderID={0} AND RegisterDate BETWEEN '{1}' AND '{2}'", IntParam, startDate, Convert.ToDateTime(endDate).AddDays(1).ToString());
            AgentChildDataBind();
        }

        //本日
        protected void btnQueryTD_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetTodayTime().Split('$')[0].ToString();
            string endDate = Fetch.GetTodayTime().Split('$')[1].ToString();

            ViewState["SearchItems"] = string.Format("WHERE SpreaderID={0} AND RegisterDate BETWEEN '{1}' AND '{2}'", IntParam, startDate, endDate);
            AgentChildDataBind();
        }

        //本周
        protected void btnQueryTW_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetWeekTime().Split('$')[0].ToString();
            string endDate = Fetch.GetWeekTime().Split('$')[1].ToString();

            ViewState["SearchItems"] = string.Format("WHERE SpreaderID={0} AND RegisterDate BETWEEN '{1}' AND '{2}'", IntParam, startDate, endDate);
            AgentChildDataBind();
        }

        //本月
        protected void btnQueryTM_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetMonthTime().Split('$')[0].ToString();
            string endDate = Fetch.GetMonthTime().Split('$')[1].ToString();

            ViewState["SearchItems"] = string.Format("WHERE SpreaderID={0} AND RegisterDate BETWEEN '{1}' AND '{2}'", IntParam, startDate, endDate);
            AgentChildDataBind();
        }

        //本年
        protected void btnQueryTY_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetYearTime().Split('$')[0].ToString();
            string endDate = Fetch.GetYearTime().Split('$')[1].ToString();

            ViewState["SearchItems"] = string.Format("WHERE SpreaderID={0} AND RegisterDate BETWEEN '{1}' AND '{2}'", IntParam, startDate, endDate);
            AgentChildDataBind();
        }
        #endregion

        #region 数据绑定

        //绑定数据
        private void AgentChildDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideAccountsFacade.GetList(AccountsInfo.Tablename, anpPage.CurrentPageIndex, anpPage.PageSize, SearchItems, Orderby);
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
                    ViewState["Orderby"] = "ORDER BY RegisterDate DESC";
                }

                return (string)ViewState["Orderby"];
            }

            set
            {
                ViewState["Orderby"] = value;
            }
        }
        #endregion

        #region 公共方法

        /// <summary>
        /// 贡献税收
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected Int64 GetChildRevenueProvide(int userID)
        {
            return FacadeManage.aideTreasureFacade.GetChildRevenueProvide(userID);
        }

        /// <summary>
        /// 贡献充值
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected Int64 GetChildPayProvide(int userID)
        {
            return FacadeManage.aideTreasureFacade.GetChildPayProvide(userID);
        }
        #endregion
    }
}