using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using Game.Entity.Accounts;
using Game.Entity.Enum;

namespace Game.Web.Module.AccountManager
{
    public partial class AccountsControlList : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                BindData();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 删除黑白名单
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Delete);
            string strQuery = "WHERE UserID IN (" + StrCIdList + ")";
            try
            {
                FacadeManage.aideAccountsFacade.DeleteAccountsControl(strQuery);
                ShowInfo("删除成功");
            }
            catch
            {
                ShowError("删除失败");
            }
            BindData();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BindData()
        {
            // Where条件
            string seachKey = CtrlHelper.GetTextAndFilter(txtSearch);
            BuildWheres wheres = new BuildWheres();
            if(!string.IsNullOrEmpty(seachKey)) 
            {
                if(ddlSearchType.SelectedValue == "1")
                {
                    wheres.Append(string.Format("Accounts='{0}'", seachKey));
                }
                else 
                {
                    if(!Utils.Validate.IsPositiveInt(seachKey))
                    {
                        ShowError("用户ID输入不正确");
                        return;
                    }
                    wheres.Append(string.Format("UserID={0}", seachKey));
                }
            }
            if(ddlControlStatus.SelectedValue != "0")
            {
                wheres.Append(string.Format("ControlStatus={0}", ddlControlStatus.SelectedValue));
            }

            // 查询并绑定数据
            PagerSet pagerSet = FacadeManage.aideAccountsFacade.GetList("AccountsControl", PageIndex, anpPage.PageSize, wheres.ToString(), "ORDER BY ActiveDateTime DESC");
            if(pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                litNoData.Visible = false;
                rptData.Visible = true;
                rptData.DataSource = pagerSet.PageSet;
                rptData.DataBind();
                anpPage.RecordCount = pagerSet.RecordCount;
            }
            else
            {
                litNoData.Visible = true;
                rptData.Visible = false;
            }
        }
    }
}