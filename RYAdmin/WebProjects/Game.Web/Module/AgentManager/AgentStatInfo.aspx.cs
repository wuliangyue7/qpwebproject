using Game.Entity.Accounts;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Web.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Module.AgentManager
{
    public partial class AgentStatInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        #endregion

        #region 数据加载

        /// <summary>
        /// 加载数据
        /// </summary>
        private void BindData()
        {
            AccountsAgent agent = FacadeManage.aideAccountsFacade.GetAccountAgentByUserID(IntParam);
            if (agent.AgentID != 0)
            {
                CtrlHelper.SetText(lblAgentID, agent.AgentID.ToString());                
                CtrlHelper.SetText(lblGameID, GetGameID(IntParam));
                CtrlHelper.SetText(lblAccounts, GetAccounts(IntParam));
            }

            CtrlHelper.SetText(lblChildCount, FacadeManage.aideAccountsFacade.GetAgentChildCount(IntParam).ToString());

            //获取代理分成详情
            DataSet ds = FacadeManage.aideTreasureFacade.GetAgentFinance(IntParam);
            Int64 agentRevenue = Convert.ToInt64(ds.Tables[0].Rows[0]["AgentRevenue"]);
            Int64 agentPay = Convert.ToInt64(ds.Tables[0].Rows[0]["AgentPay"]);
            Int64 agentPayBack = Convert.ToInt64(ds.Tables[0].Rows[0]["AgentPayBack"]);
            Int64 agentIn = agentRevenue + agentPay + agentPayBack;
            Int64 agentOut = Convert.ToInt64(ds.Tables[0].Rows[0]["AgentOut"]);
            Int64 agentRemain = agentIn - agentOut;

            CtrlHelper.SetText(lblRevenue, agentRevenue.ToString());
            CtrlHelper.SetText(lblPay, agentPay.ToString());
            CtrlHelper.SetText(lblPayBack, agentPayBack.ToString());
            CtrlHelper.SetText(lblIn, agentIn.ToString());
            CtrlHelper.SetText(lblOut, agentOut.ToString());
            CtrlHelper.SetText(lblRemain, agentRemain.ToString());
        }
        #endregion
    }
}