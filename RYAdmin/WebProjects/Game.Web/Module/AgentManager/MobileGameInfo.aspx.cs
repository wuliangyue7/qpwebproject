using Game.Entity.Accounts;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Module.AgentManager
{
    public partial class MobileGameInfo : AdminPage
    {
        #region Fields

        int agentID = GameRequest.GetQueryInt("AgentID", 0);
        #endregion

        #region 窗口事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGameList();
                BindData();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ProcessData();           
        }
        #endregion

        #region 数据加载

        private void BindGameList()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetMobileKindItemList(1, Int32.MaxValue, "WHERE Nullity=0", "ORDER BY KindID ASC");
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                ddlKindID.DataSource = pagerSet.PageSet;
                ddlKindID.DataTextField = "KindName";
                ddlKindID.DataValueField = "KindID";
                ddlKindID.DataBind();
            }
        }

        private void BindData()
        {
            if (StrCmd != "add")
            {
                ddlKindID.Enabled = false;
            }

            if (IntParam <= 0)
            {
                return;
            }

            //获取代理游戏列表实体
            AccountsAgentGame model = FacadeManage.aideAccountsFacade.GetAccountsAgentGameInfo(IntParam);
            if (model == null)
            {
                MessageBox("游戏信息不存在");                
                return;
            }

            ddlKindID.SelectedValue = model.KindID.ToString();
            CtrlHelper.SetText(txtSortID, model.SortID.ToString());
        }
        #endregion

        #region 处理方法

        private void ProcessData()
        {
            int kindID = Convert.ToInt32(ddlKindID.SelectedValue);
            int sortID = CtrlHelper.GetInt(txtSortID, 0);

            AccountsAgentGame model = new AccountsAgentGame();
            model.AgentID = agentID;
            model.KindID = Convert.ToInt32(ddlKindID.SelectedValue);
            model.DeviceID = 2;
            model.SortID = sortID;

            Message msg = new Message();
            if (StrCmd == "add")
            {
                msg = FacadeManage.aideAccountsFacade.InsertAccountsAgentGame(model);
            }
            else
            {
                model.ID = IntParam;
                msg = FacadeManage.aideAccountsFacade.UpdateAccountsAgentGame(model);
            }

            if (msg.Success)
            {
                if (StrCmd == "add")
                {
                    MessageBoxCloseRef("类型信息增加成功");
                }
                else
                {
                    MessageBoxCloseRef("类型信息修改成功");
                }
            }
            else
            {
                MessageBox("操作失败");
            }
        }
        #endregion
    }
}