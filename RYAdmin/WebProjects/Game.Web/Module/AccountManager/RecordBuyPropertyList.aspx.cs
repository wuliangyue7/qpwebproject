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
using Game.Facade;
using Game.Entity.Enum;
using Game.Entity.Platform;

namespace Game.Web.Module.AccountManager
{
    public partial class RecordBuyPropertyList : AdminPage
    {
        #region 窗口事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-") + "01";
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindData();
            }
        }

        // 查询2       
        protected void btnQuery1_Click(object sender, EventArgs e)
        {
            string queryContent = txtSearch.Text.Trim();
            StringBuilder condition = new StringBuilder();

            if (!string.IsNullOrEmpty(queryContent))
            {
                condition.AppendFormat(" WHERE UserID={0}", GetUserIDByAccount(queryContent));
            }

            ViewState["SearchItems"] = condition.ToString();
            BindData();
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        private void SetCondition(string startDate, string endDate)
        {
            StringBuilder condition = new StringBuilder(" WHERE 1=1 ");

            if (!String.IsNullOrEmpty(startDate) && endDate != "")
            {
                condition.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", startDate, Convert.ToDateTime(endDate).AddDays(1).ToString("yyyy-MM-dd"));
            }
            ViewState["SearchItems"] = condition.ToString();
        }

        // 查询1
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string startDate = txtStartDate.Text.Trim();
            string endDate = txtEndDate.Text.Trim();
            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE (1=1) ");

            SetCondition(startDate, endDate);
            BindData();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewState["SearchItems"] = null;

            txtEndDate.Text = "";
            txtStartDate.Text = "";
            BindData();
        }
        #endregion

        #region 数据绑定

        //绑定数据
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aideRecordFacade.GetList("RecordBuyProperty", PageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            anpNews.CurrentPageIndex = PageIndex;
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