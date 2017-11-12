using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using Game.Facade;
using Game.Utils.Cache;
using Game.Entity.Enum;
using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Facade.Mail;
using Game.Utils;

namespace Game.Web.Module.AccountManager
{
    public partial class SendLossReportInfo : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IntParam == 0)
            {
                return;
            }
            txtReceive.Text = FacadeManage.aideNativeWebFacade.GetLossReport(IntParam).ReportEmail;
            txtReceive.Enabled = false;
            object obj = WHCache.Default.Get<SessionCache>("reason");
            if(obj != null)
            {
                txtContent.Text = obj.ToString();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.SendMail);
            int reportid = IntParam;
            if(reportid != 0)
            {
                LossReport report = FacadeManage.aideNativeWebFacade.GetLossReport(reportid);
                AccountsInfo account = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(report.UserID);
                string site = FacadeManage.aideNativeWebFacade.GetConfigInfo(EnumerationList.SiteConfigKey.SiteConfig.ToString()).Field2;

                //邮箱配置
                ConfigInfo mail = FacadeManage.aideNativeWebFacade.GetConfigInfo(EnumerationList.SiteConfigKey.EmailConfig.ToString());
                MailConfigInfo mailConfig = new MailConfigInfo();
                mailConfig.Accounts = mail.Field1.Trim();
                mailConfig.Password = mail.Field2.Trim();
                mailConfig.Port = Convert.ToInt32(mail.Field4);
                mailConfig.SmtpServer = mail.Field3.Trim();
                mailConfig.SmtpSenderEmail = mail.Field1.Trim();
                mailConfig.LossreportUrl = "";

                Dictionary<string, string> renderVals = new Dictionary<string, string>();
                renderVals.Add("reportNO", report.ReportNo);
                renderVals.Add("userName", report.Accounts);
                renderVals.Add("mail", mail.Field1);
                renderVals.Add("url", "");
                renderVals.Add("sitename", FacadeManage.aideNativeWebFacade.GetConfigInfo(EnumerationList.SiteConfigKey.SiteConfig.ToString()).Field1);
                renderVals.Add("reason", txtContent.Text);
                string file = TextUtility.GetFullPath("/Config/lossReportFailure.config");
                TMLForgetConfigManager.ConfigFilePath = file;
                MailTMLConfigInfo tmlMail = new MailTMLConfigInfo(TMLForgetConfigManager.LoadConfig().MailContent.Text, TMLForgetConfigManager.LoadConfig().MailTitle);
                Game.Facade.Mail.EmailForgetPassword emailFoget = new Game.Facade.Mail.EmailForgetPassword(mailConfig, tmlMail, renderVals);

                //发送邮件
                try
                {
                    emailFoget.Send(report.ReportEmail);
                    report.ProcessStatus = 2;
                    FacadeManage.aideNativeWebFacade.UpdateLossReport(report);
                    MessageBox("成功发送“申诉失败”邮件");
                }
                catch(Exception ex)
                {
                    MessageBox("邮件发送失败" + ex.ToString());
                }
            }
        }
    }
}