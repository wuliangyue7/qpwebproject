using Game.Entity.Accounts;
using Game.Facade;
using Game.Kernel;
using Game.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Module.AgentManager
{
    public partial class PcGameList : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GameDataBind();
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strQuery = "WHERE ID in (" + StrCIdList + ")";
            try
            {
                FacadeManage.aideAccountsFacade.DeleteAccountsAgentGame(strQuery);
                MessageBox("删除成功");
            }
            catch
            {
                MessageBox("删除失败");
            }
            GameDataBind();
        }
        #endregion

        #region 数据绑定

        //绑定数据
        private void GameDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideAccountsFacade.GetList(AccountsAgentGame.Tablename, 1, int.MaxValue, string.Format("WHERE AgentID={0} AND DeviceID=1", IntParam), "ORDER BY SortID ASC");
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                rptGameList.DataSource = pagerSet.PageSet;
                rptGameList.DataBind();
                rptGameList.Visible = true;
                litNoData.Visible = false;
            }
            else
            {
                rptGameList.Visible = false;
                litNoData.Visible = true;
            }
        }

        #endregion
    }
}