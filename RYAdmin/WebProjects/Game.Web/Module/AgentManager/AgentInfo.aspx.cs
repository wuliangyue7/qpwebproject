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
    public partial class AgentInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ProcessData();
        }
        #endregion

        #region 处理方法

        private void ProcessData()
        {
            int gameID = CtrlHelper.GetInt(txtGameID, 0);
            string accounts = "";
            int userID = 0;
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

            //判断用户是否存在
            AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountInfoByGameID(gameID);
            if (info.UserID == 0)
            {
                MessageBox("无效的用户信息");
                return;
            }
            else
            {
                userID = info.UserID;
                accounts = info.Accounts;
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
            agent.UserID = userID;
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

            Message msg = FacadeManage.aideAccountsFacade.AddAgent(agent);
            if (msg.Success)
            {
                MessageBoxCloseRef(msg.Content);
            }
            else
            {
                MessageBox(msg.Content);
            }
        }
        #endregion        
    }
}