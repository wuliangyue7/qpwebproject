using Game.Entity.Accounts;
using Game.Entity.Enum;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
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
    public partial class AgentList : AdminPage
    {
        #region 窗口事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AgentDataBind();
            }
        }
        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            AgentDataBind();
        }
        /// <summary>
        /// 查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {

            ViewState["SearchItems"] = GetQueryString();// condition.ToString( );

            AgentDataBind();
        }

        //本日
        protected void btnQueryTD_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetTodayTime().Split('$')[0].ToString();
            string endDate = Fetch.GetTodayTime().Split('$')[1].ToString();

            ViewState["SearchItems"] = string.Format("WHERE CollectDate BETWEEN '{0}' AND '{1}'", startDate, endDate);
            AgentDataBind();
        }

        //本周
        protected void btnQueryTW_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetWeekTime().Split('$')[0].ToString();
            string endDate = Fetch.GetWeekTime().Split('$')[1].ToString();

            ViewState["SearchItems"] = string.Format("WHERE CollectDate BETWEEN '{0}' AND '{1}'", startDate, endDate);
            AgentDataBind();
        }

        //本月
        protected void btnQueryTM_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetMonthTime().Split('$')[0].ToString();
            string endDate = Fetch.GetMonthTime().Split('$')[1].ToString();

            ViewState["SearchItems"] = string.Format("WHERE CollectDate BETWEEN '{0}' AND '{1}'", startDate, endDate);
            AgentDataBind();
        }

        //本年
        protected void btnQueryTY_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetYearTime().Split('$')[0].ToString();
            string endDate = Fetch.GetYearTime().Split('$')[1].ToString();

            ViewState["SearchItems"] = string.Format("WHERE CollectDate BETWEEN '{0}' AND '{1}'", startDate, endDate);
            AgentDataBind();
        }
        
        /// <summary>
        /// 获取查询串
        /// </summary>
        /// <returns></returns>
        protected string GetQueryString()
        {
            string queryContent = CtrlHelper.GetTextAndFilter(txtSearch);
            StringBuilder condition = new StringBuilder(" WHERE 1=1");

            if (!string.IsNullOrEmpty(queryContent))
            {
                if (ckbIsLike.Checked)
                {
                    condition.AppendFormat(" AND (AgentID LIKE '%{0}%' OR Compellation LIKE '%{0}%' OR UserID IN (SELECT UserID FROM AccountsInfo WHERE Accounts LIKE '%{0}%' OR GameID LIKE '%{0}%'))", queryContent);
                }
                else
                {
                    if (Utils.Validate.IsPositiveInt(queryContent))
                        condition.AppendFormat(" AND (AgentID={0} OR Compellation='{0}' OR UserID IN (SELECT UserID FROM AccountsInfo WHERE Accounts='{0}' OR GameID='{0}'))", queryContent);
                    else
                        condition.AppendFormat(" AND (Compellation='{0}' OR UserID IN (SELECT UserID FROM AccountsInfo WHERE Accounts='{0}'))", queryContent);
                }
            }
            return condition.ToString();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewState["SearchItems"] = null;
            txtSearch.Text = "";
            ckbIsLike.Checked = false;
            AgentDataBind();
        }

        /// <summary>
        /// 批量冻结玩家
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDongjie_Click(object sender, EventArgs e)
        {
            string strQuery = "WHERE AgentID in (" + StrCIdList + ")";
            try
            {
                FacadeManage.aideAccountsFacade.DongjieAgent(strQuery);
                ShowInfo("冻结成功");
            }
            catch
            {
                ShowError("冻结失败");
            }
            AgentDataBind();
        }
        /// <summary>
        /// 批量解冻玩家
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnJiedong_Click(object sender, EventArgs e)
        {
            string strQuery = "WHERE AgentID in (" + StrCIdList + ")";
            try
            {
                FacadeManage.aideAccountsFacade.JieDongAgent(strQuery);
                ShowInfo("解冻成功");
            }
            catch
            {
                ShowError("解冻失败");
            }
            AgentDataBind();
        }

        #endregion

        #region 数据绑定

        //绑定数据      
        private void AgentDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideAccountsFacade.GetList(AccountsAgent.Tablename, anpPage.CurrentPageIndex, anpPage.PageSize, SearchItems, Orderby);
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
                    ViewState["Orderby"] = "ORDER BY AgentID ASC";
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