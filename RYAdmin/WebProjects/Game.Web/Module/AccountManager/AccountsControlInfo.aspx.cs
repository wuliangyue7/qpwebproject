using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using Game.Entity.Accounts;
using Game.Facade;
using Game.Utils;

namespace Game.Web.Module.AccountManager
{
    public partial class AccountsControlInfo : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                BindData();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BindData()
        {
            if(IntParam != 0)
            {
                ddlAddType.Visible = false;
                txtUser.Visible = false;
                rfvUser.Visible = false;
                rfvUser.Enabled = false;

                AccountsControl model = FacadeManage.aideAccountsFacade.GetAccountsControl(IntParam);
                if(model != null)
                {
                    lbUser.Text = model.Accounts + "（ID：" + model.UserID + "）";
                    ddlControlStatus.SelectedValue = model.ControlStatus.ToString();
                    txtActiveDateTime.Text = model.ActiveDateTime.ToString();
                    ddlControlType.SelectedValue = model.ControlType.ToString();
                    txtChangeScore.Text = model.ChangeScore.ToString();
                    txtSustainedTimeCount.Text = model.SustainedTimeCount.ToString();
                    txtWinRate.Text = model.WinRate.ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(ddlControlStatus.SelectedValue == "0")
            {
                ShowError("请选择控制状态");
                return;
            }
            if(ddlControlType.SelectedValue == "0")
            {
                ShowError("请选择控制类型");
                return;
            }

            AccountsControl model = new AccountsControl();
            model.ActiveDateTime = Convert.ToDateTime(CtrlHelper.GetText(txtActiveDateTime));
            model.ChangeScore = Convert.ToInt64(txtChangeScore.Text);
            model.SustainedTimeCount = Convert.ToInt32(txtSustainedTimeCount.Text);
            model.WinRate = Convert.ToByte(txtWinRate.Text);
            model.ControlStatus = Convert.ToInt16(ddlControlStatus.SelectedValue);
            model.ControlType = Convert.ToInt16(ddlControlType.SelectedValue);

            try
            {
                if(IntParam == 0)
                {
                    Game.Entity.Accounts.AccountsInfo accountsInfo;
                    if(ddlAddType.SelectedValue == "0")
                    {
                        model.Accounts = CtrlHelper.GetText(txtUser);
                        accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByAccount(model.Accounts);
                        if(accountsInfo.UserID == 0)
                        {
                            ShowError("你输入的用户帐号不存在");
                            return;
                        }
                        else 
                        {
                            model.UserID = accountsInfo.UserID;
                            model.Accounts = accountsInfo.Accounts;
                        }
                    }
                    else 
                    {
                        int userId = CtrlHelper.GetInt(txtUser, 0);
                        if(userId==0)
                        {
                            ShowError("输入的用户ID错误");
                            return;
                        }
                        model.Accounts = FacadeManage.aideAccountsFacade.GetAccountByUserID(userId);
                        if(string.IsNullOrEmpty(model.Accounts))
                        {
                            ShowError("你输入的用户ID不存在");
                            return;
                        }
                        model.UserID = userId;
                    }
                    if(FacadeManage.aideAccountsFacade.GetAccountsControl(model.UserID)!=null)
                    {
                        ShowError("选择的用户已存在黑白名单之中");
                        return;
                    }
                    FacadeManage.aideAccountsFacade.AddAccountsControl(model);
                }
                else
                {
                    model.UserID = IntParam;
                    FacadeManage.aideAccountsFacade.UpdateAccountsControl(model);
                }
                ShowInfo("操作成功", "AccountsControlList.aspx", 1000);
            }
            catch(Exception ex)
            {
                ShowError("添加失败");
                Response.Write(ex.ToString());
                return;
            }
        }
    }
}