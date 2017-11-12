using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using Game.Facade;
using System.Text;
using Game.Entity.NativeWeb;
using Game.Entity.Accounts;
using Game.Utils;
using Game.Facade.Mail;
using Game.Entity.Enum;
using Game.Utils.Cache;

namespace Game.Web.Module.AccountManager
{
    public partial class AccountsLossReportInfo : AdminPage
    {
        protected string reason = string.Empty;
        protected int userID = 0;
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                BindData();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BindData()
        {
            int reportId = IntParam;
            if( reportId != 0 )
            {
                StringBuilder sb = new StringBuilder();
                LossReport report = FacadeManage.aideNativeWebFacade.GetLossReport( reportId );
                AccountsInfo account = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID( report.UserID );
                IndividualDatum datum = FacadeManage.aideAccountsFacade.GetAccountDetailByUserID( report.UserID );
                AccountsProtect prote = FacadeManage.aideAccountsFacade.GetAccountsProtectByUserID( report.UserID );

                userID = report.UserID;

                #region 绑定申诉资料
                string right = "（&nbsp;<font class=\"lanse fontTip\"><strong>√</strong></font>&nbsp;）";
                string error = "（&nbsp;<font class=\"hong fontTip\"><strong>×</strong></font>&nbsp;）";
                int rightNum = 0, emptNum = 0;

                this.lblAccounts.Text = report.Accounts;

                ///判断身份证号
                if( string.IsNullOrEmpty( report.PassportID ) )
                {
                    emptNum = emptNum + 1;
                    lblPassportID.Text = "";
                    sb.Append( "身份证号（未填写）<br/>" );
                }
                else
                {
                    if( report.PassportID == account.PassPortID )
                    {
                        lblPassportID.Text = right + report.PassportID;
                        rightNum = rightNum + 1;
                        sb.Append( "身份证号（正确）<br/>" );
                    }
                    else
                    {
                        lblPassportID.Text = error + report.PassportID;
                        sb.Append( "身份证号（错误）<br/>" );
                    }
                }

                ///判断移动电话
                if( string.IsNullOrEmpty( report.MobilePhone ) )
                {
                    emptNum = emptNum + 1;
                    lblLinkPhone.Text = "";
                    sb.Append( "移动电话（未填写）<br/>" );
                }
                else
                {
                    //if( report.MobilePhone == datum.MobilePhone )
                    //{
                    //    lblLinkPhone.Text = right + report.MobilePhone;
                    //    rightNum = rightNum + 1;
                    //    sb.Append( "移动电话（正确）<br/>" );
                    //}
                    //else
                    //{
                    //    lblLinkPhone.Text = error + report.MobilePhone;
                    //    sb.Append( "移动电话（错误）<br/>" );
                    //}
                    if (datum == null || report.MobilePhone != datum.MobilePhone)
                    {
                        lblLinkPhone.Text = error + report.MobilePhone;
                        sb.Append("移动电话（错误）<br/>");
                    }
                    else
                    {
                        lblLinkPhone.Text = right + report.MobilePhone;
                        rightNum = rightNum + 1;
                        sb.Append("移动电话（正确）<br/>");
                    }
                }

                ///判断真实姓名
                if( string.IsNullOrEmpty( report.Compellation ) )
                {
                    emptNum = emptNum + 1;
                    lblCompellation.Text = "";
                    sb.Append( "移动电话（未填写）<br/>" );
                }
                else
                {
                    if( report.Compellation == account.Compellation )
                    {
                        lblCompellation.Text = right + report.Compellation;
                        rightNum = rightNum + 1;
                        sb.Append( "移动电话（正确）<br/>" );
                    }
                    else
                    {
                        lblCompellation.Text = error + report.Compellation;
                        sb.Append( "移动电话（错误）<br/>" );
                    }
                }
                if( string.IsNullOrEmpty( report.RegisterDate ) )
                {
                    emptNum = emptNum + 1;
                    lblRegisterTime.Text = "";
                    sb.Append( "注册时间（未填写）<br/>" );
                }
                else
                {
                    if( Convert.ToDateTime( report.RegisterDate ).Date == account.RegisterDate.Date )
                    {
                        lblRegisterTime.Text = right + report.RegisterDate;
                        rightNum = rightNum + 1;
                        sb.Append( "注册时间（正确）<br/>" );
                    }
                    else
                    {
                        lblRegisterTime.Text = error + report.RegisterDate;
                        sb.Append( "注册时间（错误）<br/>" );
                    }
                }

                //判断密保1
                if( string.IsNullOrEmpty( report.OldQuestion1 ) && string.IsNullOrEmpty( report.OldResponse1 ) )
                {
                    emptNum = emptNum + 1;
                    lblOldProtect1.Text = "";
                    sb.Append( "申诉密保1（未填写）<br/>" );
                }
                else
                {
                    if( ( report.OldQuestion1 == prote.Question1 && report.OldResponse1 == prote.Response1 )
                        || ( report.OldQuestion1 == prote.Question2 && report.OldResponse1 == prote.Response2 )
                        || ( report.OldQuestion1 == prote.Question3 && report.OldResponse1 == prote.Response3 ) )
                    {
                        lblOldProtect1.Text = right + "问：" + report.OldQuestion1 + " 答：" + report.OldResponse1;
                        rightNum = rightNum + 1;
                        sb.Append( "申诉密保1（正确）<br/>" );
                    }
                    else
                    {
                        lblOldProtect1.Text = error + "问：" + report.OldQuestion1 + " 答：" + report.OldResponse1;
                        sb.Append( "申诉密保1（错误）<br/>" );
                    }
                }

                //判断密保2
                if( string.IsNullOrEmpty( report.OldQuestion2 ) && string.IsNullOrEmpty( report.OldResponse2 ) )
                {
                    emptNum = emptNum + 1;
                    lblOldProtect2.Text = "";
                    sb.Append( "申诉密保2（未填写）<br/>" );
                }
                else
                {
                    if( ( report.OldQuestion2 == prote.Question1 && report.OldResponse2 == prote.Response1 )
                        || ( report.OldQuestion2 == prote.Question2 && report.OldResponse2 == prote.Response2 )
                        || ( report.OldQuestion2 == prote.Question3 && report.OldResponse2 == prote.Response3 ) )
                    {
                        lblOldProtect2.Text = right + "问：" + report.OldQuestion2 + " 答：" + report.OldResponse2;
                        rightNum = rightNum + 1;
                        sb.Append( "申诉密保2（正确）<br/>" );
                    }
                    else
                    {
                        lblOldProtect2.Text = error + "问：" + report.OldQuestion2 + " 答：" + report.OldResponse2;
                        sb.Append( "申诉密保2（错误）<br/>" );
                    }
                }

                //判断密保3
                if( string.IsNullOrEmpty( report.OldQuestion3 ) && string.IsNullOrEmpty( report.OldResponse3 ) )
                {
                    emptNum = emptNum + 1;
                    lblOldProtect2.Text = "";
                    sb.Append( "申诉密保3（未填写）<br/>" );
                }
                else
                {
                    if( ( report.OldQuestion3 == prote.Question1 && report.OldResponse3 == prote.Response1 )
                        || ( report.OldQuestion3 == prote.Question2 && report.OldResponse3 == prote.Response2 )
                        || ( report.OldQuestion3 == prote.Question3 && report.OldResponse3 == prote.Response3 ) )
                    {
                        lblOldProtect3.Text = right + "问：" + report.OldQuestion3 + " 答：" + report.OldResponse3;
                        rightNum = rightNum + 1;
                        sb.Append( "申诉密保3（正确）<br/>" );
                    }
                    else
                    {
                        lblOldProtect3.Text = error + "问：" + report.OldQuestion3 + " 答：" + report.OldResponse3;
                        sb.Append( "申诉密保3（错误）<br/>" );
                    }
                }

                #region 判断历史昵称
                //判断历史昵称
                Dictionary<int, string> dic = FacadeManage.aideRecordFacade.GetOldNickNameOrAccountsList( report.UserID, 1 );
                if( string.IsNullOrEmpty( report.OldNickName1 ) )
                {
                    emptNum = emptNum + 1;
                    lblOldNickName1.Text = "";
                    sb.Append( "绑定历史昵称1（未填写）<br/>" );
                }
                else
                {
                    if( dic.ContainsValue( report.OldNickName1 ) )
                    {
                        lblOldNickName1.Text = right + report.OldNickName1;
                        rightNum = rightNum + 1;
                        sb.Append( "绑定历史昵称1（正确）<br/>" );
                    }
                    else
                    {
                        lblOldNickName1.Text = error + report.OldNickName1;
                        sb.Append( "绑定历史昵称1（错误）<br/>" );
                    }
                }
                if( string.IsNullOrEmpty( report.OldNickName2 ) )
                {
                    emptNum = emptNum + 1;
                    lblOldNickName2.Text = "";
                    sb.Append( "绑定历史昵称2（未填写）<br/>" );
                }
                else
                {
                    if( dic.ContainsValue( report.OldNickName2 ) )
                    {
                        lblOldNickName2.Text = right + report.OldNickName2;
                        rightNum = rightNum + 1;
                        sb.Append( "绑定历史昵称2（正确）<br/>" );
                    }
                    else
                    {
                        lblOldNickName2.Text = error + report.OldNickName2;
                        sb.Append( "绑定历史昵称2（错误）<br/>" );
                    }
                }

                if( string.IsNullOrEmpty( report.OldNickName3 ) )
                {
                    emptNum = emptNum + 1;
                    lblOldNickName3.Text = "";
                    sb.Append( "绑定历史昵称3（未填写）<br/>" );
                }
                else
                {
                    if( dic.ContainsValue( report.OldNickName3 ) )
                    {
                        lblOldNickName3.Text = right + report.OldNickName3;
                        rightNum = rightNum + 1;
                        sb.Append( "绑定历史昵称3（正确）<br/>" );
                    }
                    else
                    {
                        lblOldNickName3.Text = error + report.OldNickName3;
                        sb.Append( "绑定历史昵称3（错误）<br/>" );
                    }
                }
                #endregion

                #region 判断历史登陆密码
                //判断历史登录密码
                dic = FacadeManage.aideRecordFacade.GetOldLogonPassList( report.UserID );
                if( string.IsNullOrEmpty( report.OldLogonPass1 ) )
                {
                    emptNum = emptNum + 1;
                    lblOldLogonPass1.Text = "";
                    sb.Append( "绑定历史登录密码1（未填写）<br/>" );
                }
                else
                {
                    if( dic.ContainsValue( report.OldLogonPass1 ) )
                    {
                        lblOldLogonPass1.Text = right + report.OldLogonPass1;
                        rightNum = rightNum + 1;
                        sb.Append( "绑定历史登录密码1（正确）<br/>" );
                    }
                    else
                    {
                        lblOldLogonPass1.Text = error + report.OldLogonPass1;
                        sb.Append( "绑定历史登录密码1（错误）<br/>" );
                    }
                }

                if( string.IsNullOrEmpty( report.OldLogonPass2 ) )
                {
                    emptNum = emptNum + 1;
                    lblOldLogonPass2.Text = "";
                    sb.Append( "绑定历史登录密码2（未填写）<br/>" );
                }
                else
                {
                    if( dic.ContainsValue( report.OldLogonPass2 ) )
                    {
                        lblOldLogonPass2.Text = right + report.OldLogonPass2;
                        rightNum = rightNum + 1;
                        sb.Append( "绑定历史登录密码2（正确）<br/>" );
                    }
                    else
                    {
                        lblOldLogonPass2.Text = error + report.OldLogonPass2;
                        sb.Append( "绑定历史登录密码2（错误）<br/>" );
                    }
                }

                if( string.IsNullOrEmpty( report.OldLogonPass3 ) )
                {
                    emptNum = emptNum + 1;
                    lblOldLogonPass3.Text = "";
                    sb.Append( "绑定历史登录密码3（未填写）<br/>" );
                }
                else
                {
                    if( dic.ContainsValue( report.OldLogonPass3 ) )
                    {
                        lblOldLogonPass3.Text = right + report.OldLogonPass3;
                        rightNum = rightNum + 1;
                        sb.Append( "绑定历史登录密码3（正确）<br/>" );
                    }
                    else
                    {
                        lblOldLogonPass3.Text = error + report.OldLogonPass3;
                        sb.Append( "绑定历史登录密码3（错误）<br/>" );
                    }
                }
                #endregion

                #endregion

                this.lblReportDate.Text = report.ReportDate.ToString();
                this.lblReportIP.Text = report.ReportIP;
                this.lblReportNo.Text = report.ReportNo;
                this.lblSuppInfo.Text = report.SuppInfo;


                #region 绑定密保资料
                string html = prote != null ? string.Format( "<a href=\"#\" class=\"l\" onclick=\"javascript:openWindowOwn('AccountsProtectInfo.aspx?param={0}','{1}',660,320);\">已申请密码保护</a>",
                    account.ProtectID, "密码保护" ) : "<font class=\"hong\">未申请密码保护</font>";
                PassProtect.InnerHtml = html;
                #endregion

                #region 获取失败原因

                reason = "<div class=\"reasonSS\"><strong>在您提供的17笔证明材料中:</strong><br/>其中," + ( 14 - emptNum - rightNum ) + "项（错误），" + emptNum + "项（未填写），" + rightNum + "项（正确）<br/>";
                reason += sb.ToString() + "</div>";
                CheckInfo.InnerHtml = reason;
                WHCache.Default.Save<SessionCache>( "reason", reason, 5 );
                #endregion
            }
        }

        /// <summary>
        /// 发送重置密码邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton1_Click( object sender, EventArgs e )
        {
            AuthUserOperationPermission( Permission.SendMail );
            int reportid = IntParam;
            if( reportid != 0 )
            {
                LossReport report = FacadeManage.aideNativeWebFacade.GetLossReport( reportid );
                AccountsInfo account = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID( report.UserID );
                string site = FacadeManage.aideNativeWebFacade.GetConfigInfo( EnumerationList.SiteConfigKey.SiteConfig.ToString() ).Field2;
                string sign = TextEncrypt.MD5EncryptPassword( report.ReportNo + report.UserID + report.ReportDate.ToString().Trim() + report.Random + ApplicationSettings.Get( "ReportForgetPasswordKey" ) );
                string url = Game.Utils.Utility.UrlEncode(site + string.Format("/Member/Complaint-Setp-4.aspx?param={0}&sign={1}&test=test", report.ReportNo, sign));

                //邮箱配置
                ConfigInfo mail = FacadeManage.aideNativeWebFacade.GetConfigInfo( EnumerationList.SiteConfigKey.EmailConfig.ToString() );
                MailConfigInfo mailConfig = new MailConfigInfo();
                mailConfig.Accounts = mail.Field1.Trim();
                mailConfig.Password = mail.Field2.Trim();
                mailConfig.Port = Convert.ToInt32( mail.Field4 );
                mailConfig.SmtpServer = mail.Field3.Trim();
                mailConfig.SmtpSenderEmail = mail.Field1.Trim();
                mailConfig.LossreportUrl = "";

                Dictionary<string, string> renderVals = new Dictionary<string, string>();
                renderVals.Add( "reportNO", report.ReportNo );
                renderVals.Add( "userName", report.Accounts );
                renderVals.Add( "url", url );
                renderVals.Add( "mail", mail.Field1 );
                renderVals.Add( "sitename", FacadeManage.aideNativeWebFacade.GetConfigInfo( EnumerationList.SiteConfigKey.SiteConfig.ToString() ).Field1 );
                renderVals.Add( "reason", "" );
                string file = TextUtility.GetFullPath("/Config/lossReportSuccess.config");
                TMLForgetConfigManager.ConfigFilePath = file;              
                MailTMLConfigInfo tmlMail = new MailTMLConfigInfo(TMLForgetConfigManager.LoadConfig().MailContent.Text, TMLForgetConfigManager.LoadConfig().MailTitle);
                Game.Facade.Mail.EmailForgetPassword emailFoget = new Game.Facade.Mail.EmailForgetPassword( mailConfig, tmlMail, renderVals );

                //发送邮件
                try
                {
                    emailFoget.Send( report.ReportEmail );
                    report.ProcessStatus = 1;
                    FacadeManage.aideNativeWebFacade.UpdateLossReport( report );
                    MessageBox( "成功发送“申诉成功”邮件" );
                }
                catch( Exception ex )
                {
                    MessageBox( "邮件发送失败" + ex.ToString() );
                }
            }
        }
    }
}