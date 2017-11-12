using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Utils;
using Game.Web.UI;
using Game.Kernel;
using Game.Entity.NativeWeb;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.WebManager
{
    public partial class FeedbackInfo : AdminPage
    {

        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            if( !Page.IsPostBack )
            {
                GameFeedbackDataBind();
            }
        }

        protected void btnSave_Click( object sender, EventArgs e )
        {
            //判断权限
            AuthUserOperationPermission( Permission.Edit );
            ProcessData();
        }
        #endregion

        #region 数据加载

        private void GameFeedbackDataBind()
        {
            if( StrCmd == "add" )
            {
                litInfo.Text = "新增";
            }
            else
            {
                litInfo.Text = "更新";
            }

            if( IntParam <= 0 )
            {
                return;
            }

            //获取反馈信息
            GameFeedbackInfo feedback = FacadeManage.aideNativeWebFacade.GetGameFeedbackInfo( IntParam );
            if( feedback == null )
            {
                ShowError( "反馈信息不存在" );
                Redirect( "FeedbackList.aspx" );
                return;
            }

            string domain = "";
            ConfigInfo ci = FacadeManage.aideNativeWebFacade.GetConfigInfo("SiteConfig");
            if (ci != null)
            {
                domain = ci.Field2;
            }

            CtrlHelper.SetText( lblFeedbackTitle, feedback.FeedbackTitle );
            CtrlHelper.SetText(lblFeedbackContent, feedback.FeedbackContent.Replace("/Upload/", domain + "/Upload/"));
            CtrlHelper.SetText(lblAccounts, feedback.UserID == 0 ? "匿名提问" : GetAccounts(feedback.UserID));
            CtrlHelper.SetText( lblFeedbackDate, feedback.FeedbackDate.ToString() );
            CtrlHelper.SetText( lblClientIP, feedback.ClientIP );
            CtrlHelper.SetText( txtRevertContent, feedback.RevertContent );
            CtrlHelper.SetText( lblRevertUserID, GetMasterName( feedback.RevertUserID ) );
            CtrlHelper.SetText( lblRevertDate, feedback.RevertUserID == 0 ? "" : feedback.RevertDate.ToString() );
            rbtnNullity.SelectedValue = feedback.Nullity.ToString();
            CtrlHelper.SetText( lbIsProcessed, Convert.ToByte( feedback.IsProcessed ) == 0 ? "未处理" : "已处理" );
        }
        #endregion

        #region 处理方法

        private void ProcessData()
        {
            GameFeedbackInfo feedback = new GameFeedbackInfo();
            feedback.RevertContent = CtrlHelper.GetText( txtRevertContent );
            feedback.RevertUserID = userExt.UserID;
            feedback.RevertDate = DateTime.Now;
            feedback.Nullity = Convert.ToByte( rbtnNullity.SelectedValue.Trim() );
            feedback.IsProcessed = 1;

            Message msg = new Message();
            if( StrCmd == "edit" )
            {
                feedback.FeedbackID = IntParam;
                msg = FacadeManage.aideNativeWebFacade.RevertGameFeedback( feedback );
            }

            if( msg.Success )
            {
                ShowInfo( "成功处理反馈", "FeedbackList.aspx", 1200 );
            }
            else
            {
                ShowError( msg.Content );
            }
        }
        #endregion
    }
}
