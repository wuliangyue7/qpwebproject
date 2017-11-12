using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
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
    public partial class AgentManager : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ProcessData();
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
                CtrlHelper.SetText(txtCompellation, agent.Compellation);
                CtrlHelper.SetText(txtDomain, agent.Domain);
                rblAgentType.SelectedValue = agent.AgentType.ToString();
                CtrlHelper.SetText(txtAgentScale, Convert.ToInt32(agent.AgentScale * 1000).ToString());
                CtrlHelper.SetText(txtDayBackAllPay, agent.PayBackScore.ToString());
                CtrlHelper.SetText(txtDayBackScale, Convert.ToInt32(agent.PayBackScale * 1000).ToString());
                CtrlHelper.SetText(txtMobilePhone, agent.MobilePhone);
                CtrlHelper.SetText(txtEMail, agent.EMail);
                CtrlHelper.SetText(txtDwellingPlace, agent.DwellingPlace);
                CtrlHelper.SetText(txtAgentNote, agent.AgentNote);

                CtrlHelper.SetText(lblGameID, GetGameID(IntParam));
                CtrlHelper.SetText(lblAccounts, GetAccounts(IntParam));
            }
        }
        #endregion

        #region 处理方法

        private void ProcessData()
        {
            string compellation = CtrlHelper.GetText(txtCompellation);
            string domain = CtrlHelper.GetText(txtDomain);
            int agentType = Convert.ToInt32(rblAgentType.SelectedValue.Trim());
            int agentScale = CtrlHelper.GetInt(txtAgentScale, 0);
            int dayBackAllPay = CtrlHelper.GetInt(txtDayBackAllPay, 0);
            int dayBackScale = CtrlHelper.GetInt(txtDayBackScale, 0);
            string mobilePhone = CtrlHelper.GetText(txtMobilePhone);
            string eMail = CtrlHelper.GetText(txtEMail);
            string dwellingPlace = CtrlHelper.GetText(txtDwellingPlace);
            string agentNote = CtrlHelper.GetText(txtAgentNote);

            //判断代理域名唯一
            AccountsAgent model = FacadeManage.aideAccountsFacade.GetAccountAgentByDomain(domain);
            if (model != null && model.UserID != IntParam)
            {
                MessageBox("代理域名已经存在");
                return;
            }

            if (compellation == "")
            {
                MessageBox("真实姓名不能为空");
                return;
            }
            if (domain == "")
            {
                MessageBox("代理域名不能为空");
                return;
            }
            if (agentScale < 0 || agentScale > 1000)
            {
                MessageBox("分成比例输入非法");
                return;
            }
            if (dayBackAllPay < 0)
            {
                MessageBox("日累计充值返现输入非法");
                return;
            }
            if (dayBackScale < 0 || dayBackScale > 1000)
            {
                MessageBox("返现比例输入非法");
                return;
            }
            if (mobilePhone == "")
            {
                MessageBox("联系电话不能为空");
                return;
            }

            AccountsAgent agent = new AccountsAgent();
            agent.UserID = IntParam;
            agent.Compellation = compellation;
            agent.Domain = domain;
            agent.AgentType = agentType;
            agent.AgentScale = Convert.ToDecimal(agentScale) / 1000;
            agent.PayBackScore = dayBackAllPay;
            agent.PayBackScale = Convert.ToDecimal(dayBackScale) / 1000;
            agent.MobilePhone = mobilePhone;
            agent.EMail = eMail;
            agent.DwellingPlace = dwellingPlace;
            agent.AgentNote = agentNote;

            bool ret = FacadeManage.aideAccountsFacade.UpdateAgent(agent);
            if (ret)
            {
                MessageBoxCloseRef("代理信息更新成功");
            }
            else
            {
                MessageBox("代理信息更新失败");
            }
        }
        #endregion        
    }
}