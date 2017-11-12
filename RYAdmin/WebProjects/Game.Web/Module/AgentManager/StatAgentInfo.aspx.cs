using Game.Facade;
using Game.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Module.AgentManager
{
    public partial class StatAgentInfo : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRevenue_Click(object sender, EventArgs e)
        {
            try
            {
                FacadeManage.aideTreasureFacade.StatRevenueHand();
                MessageBox("统计成功");
            }
            catch
            {
                MessageBox("统计失败");
            }
        }

        protected void btnPayBack_Click(object sender, EventArgs e)
        {
            try
            {
                FacadeManage.aideTreasureFacade.StatAgentPayHand();
                MessageBox("统计成功");
            }
            catch
            {
                MessageBox("统计失败");
            }
        }
    }
}