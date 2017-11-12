using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using Game.Entity.Accounts;
using Game.Facade;

namespace Game.Web.Module.AccountManager
{
    public partial class AccountsControlConfig : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                BindData();
        }

        protected void BindData()
        {
            ControlConfig model = FacadeManage.aideAccountsFacade.GetControlConfig();
            if(model != null)
            {
                rblAutoControlEnable.SelectedValue = model.AutoControlEnable.ToString();
                txtJoinBlackWinScore.Text = model.JoinBlackWinScore.ToString();
                txtJoinWhiteLoseScore.Text = model.JoinWhiteLoseScore.ToString();
                ddlBlackControlType.SelectedValue = model.BlackControlType.ToString();
                txtBSustainedTimeCount.Text = model.BSustainedTimeCount.ToString();
                txtQuitBlackLoseScore.Text = model.QuitBlackLoseScore.ToString();
                ddlWhiteControlType.SelectedValue = model.WhiteControlType.ToString();
                txtWSustainedTimeCount.Text = model.WSustainedTimeCount.ToString();
                txtQuitWhiteWinScore.Text = model.QuitWhiteWinScore.ToString();
                txtBlackWinRate.Text = model.BlackWinRate.ToString();
                txtWhiteWinRate.Text = model.WhiteWinRate.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(ddlBlackControlType.SelectedValue == "0")
            {
                ShowError("请选择黑名单结束方式");
                return;
            }

            if(ddlWhiteControlType.SelectedValue == "0")
            {
                ShowError("请选择白名单结束方式");
                return;
            }

            ControlConfig model = new ControlConfig();
            model.AutoControlEnable = Convert.ToByte(rblAutoControlEnable.SelectedValue);
            model.JoinBlackWinScore = Convert.ToInt32(txtJoinBlackWinScore.Text);
            model.JoinWhiteLoseScore = Convert.ToInt32(txtJoinWhiteLoseScore.Text);
            model.BlackControlType = Convert.ToByte(ddlBlackControlType.SelectedValue);
            model.BSustainedTimeCount = Convert.ToInt32(txtBSustainedTimeCount.Text);
            model.QuitBlackLoseScore = Convert.ToInt32(txtQuitBlackLoseScore.Text);
            model.WhiteControlType = Convert.ToByte(ddlWhiteControlType.SelectedValue);
            model.WSustainedTimeCount = Convert.ToInt32(txtWSustainedTimeCount.Text);
            model.QuitWhiteWinScore = Convert.ToInt32(txtQuitWhiteWinScore.Text);
            model.BlackWinRate = Convert.ToByte(txtBlackWinRate.Text);
            model.WhiteWinRate = Convert.ToByte(txtWhiteWinRate.Text);

            try
            {
                FacadeManage.aideAccountsFacade.UpdateControlConfig(model);
                ShowInfo("修改成功");
            }
            catch(Exception ex)
            {
                ShowError("修改失败" + ex.ToString());
            }
        }
    }
}