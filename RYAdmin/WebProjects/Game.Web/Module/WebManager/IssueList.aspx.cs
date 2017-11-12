using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Game.Web.UI;
using Game.Kernel;
using Game.Utils;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.WebManager
{
    public partial class IssueList : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IssueDataBind();
            }
        }

        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            IssueDataBind();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission( Permission.Delete );
            string strQuery = "WHERE IssueID in (" + StrCIdList + ")";
            try
            {
                FacadeManage.aideNativeWebFacade.DeleteGameIssue( strQuery );
                ShowInfo("删除成功");
            }
            catch
            {
                ShowError("删除失败");
            }
            IssueDataBind();
        }

        #endregion

        #region 数据绑定

        //绑定数据
        private void IssueDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetGameIssueList( anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby );
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                litNoData.Visible = false;
            }
            else
            {
                litNoData.Visible = true;
            }

            rptIssue.DataSource = pagerSet.PageSet;
            rptIssue.DataBind();
            anpNews.RecordCount = pagerSet.RecordCount;
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

            set { ViewState["SearchItems"] = value; }
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
                    ViewState["Orderby"] = "ORDER BY IssueID ASC";
                }

                return (string)ViewState["Orderby"];
            }

            set { ViewState["Orderby"] = value; }
        }

        #endregion                       

        #region 公共方法

        /// <summary>
        /// 获取问题类型
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        protected string GetTypeName(int typeID)
        {
            string rValue = "";
            switch (typeID)
            {
                case 1:
                    rValue = "常见问题";
                    break;
                case 2:
                    rValue = "充值问题";
                    break;
                case 3:
                    rValue = "高级教程";
                    break;
                case 4:
                    rValue = "功能说明";
                    break;
                default:
                    rValue = "常见问题";
                    break;
            }
            return rValue; 
        }
        #endregion
    }
}
