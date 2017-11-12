using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Game.Utils;
using Game.Web.UI;
using Game.Kernel;
using Game.Entity.NativeWeb;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.WebManager
{
    public partial class RulesInfo : AdminPage
    {

        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            if( !Page.IsPostBack )
            {
                BindKindList();
                GameFeedbackDataBind();
            }
        }

        protected void btnSave_Click( object sender, EventArgs e )
        {
            ProcessData();
        }
        #endregion

        #region 数据加载

        //绑定游戏
        private void BindKindList()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetGameKindItemList( 1, Int32.MaxValue, "", "ORDER BY KindID ASC" );
            if( pagerSet.PageSet.Tables[0].Rows.Count > 0 )
            {
                ddlKind.DataSource = pagerSet.PageSet;
                ddlKind.DataTextField = "KindName";
                ddlKind.DataValueField = "KindID";
                ddlKind.DataBind();
            }
            ddlKind.Items.Insert(0, new ListItem("==请选择游戏==", "0"));
        }

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

            //获取规则信息
            GameRulesInfo gameRules = FacadeManage.aideNativeWebFacade.GetGameRulesInfo( IntParam );
            if( gameRules == null )
            {
                ShowError( "规则信息不存在" );
                Redirect( "RulesList.aspx" );
                return;
            }
            ddlKind.SelectedValue = gameRules.KindID.ToString().Trim();
            CtrlHelper.SetText(txtHelpIntro, gameRules.HelpIntro);
            CtrlHelper.SetText( txtHelpRule, gameRules.HelpRule );
            CtrlHelper.SetText( txtHelpGrade, gameRules.HelpGrade );
            rbtnNullity.SelectedValue = gameRules.Nullity.ToString().Trim();

            rbtnIsJoin.SelectedValue = gameRules.JoinIntro.ToString().Trim();
            CtrlHelper.SetText( lblCollectDate, gameRules.CollectDate.ToString() );
            CtrlHelper.SetText( lblModifyDate, gameRules.ModifyDate.ToString() );
            upThumbnail.FilePath = gameRules.ThumbnailUrl;
            upShowPicture.FilePath = gameRules.ImgRuleUrl;
            upMobilePicture.FilePath = gameRules.MobileImgUrl;

            foreach(ListItem lt in cblMoblieGameType.Items)
            {
                if((Convert.ToInt32(lt.Value) & gameRules.MobileGameType) == Convert.ToInt32(lt.Value))
                {
                    lt.Selected = true;
                }
            }
            CtrlHelper.SetText(txtAndroidDownloadUrl, gameRules.AndroidDownloadUrl);
            CtrlHelper.SetText(txtIOSDownloadUrl, gameRules.IOSDownloadUrl);
            CtrlHelper.SetText(txtMoileSize, gameRules.MobileSize);
            CtrlHelper.SetText(txtMobileDate, gameRules.MobileDate);
            CtrlHelper.SetText(txtMobileVersion, gameRules.MobileVersion);

            StringBuffer bufferFileView = new StringBuffer();
        }
        #endregion

        #region 处理方法

        private void ProcessData()
        {
            GameRulesInfo gameRules = new GameRulesInfo();
            gameRules.KindID = Convert.ToInt32( ddlKind.SelectedValue.Trim() );
            gameRules.KindID = Convert.ToInt32(ddlKind.SelectedValue.Trim());
            if(gameRules.KindID == 0)
            {
                ShowError("请选择游戏");
                return;
            }
            gameRules.KindName = ddlKind.SelectedItem.Text;
            gameRules.HelpIntro = CtrlHelper.GetText( txtHelpIntro );
            gameRules.HelpRule = CtrlHelper.GetText( txtHelpRule );
            gameRules.HelpGrade = CtrlHelper.GetText( txtHelpGrade );
            gameRules.JoinIntro = Convert.ToByte( rbtnIsJoin.SelectedValue.Trim() );
            gameRules.Nullity = Convert.ToByte( rbtnNullity.SelectedValue.Trim() );
            gameRules.AndroidDownloadUrl = CtrlHelper.GetText(txtAndroidDownloadUrl);
            gameRules.IOSDownloadUrl = CtrlHelper.GetText(txtIOSDownloadUrl);
            gameRules.MobileSize = CtrlHelper.GetText(txtMoileSize);
            gameRules.MobileDate = CtrlHelper.GetText(txtMobileDate);
            gameRules.MobileVersion = CtrlHelper.GetText(txtMobileVersion);
            int type = 0;
            foreach(ListItem lt in cblMoblieGameType.Items)
            {
                if(lt.Selected)
                {
                    type = type | Convert.ToInt32(lt.Value);
                }
            }
            gameRules.MobileGameType = Convert.ToByte(type);

            //缩率图上传
            try
            {
                gameRules.ThumbnailUrl = upThumbnail.FilePath;
                if( string.IsNullOrEmpty( gameRules.ThumbnailUrl ) ) 
                {
                    ShowError("请选择一个游戏ICO图标！");
                    return;
                }
            }
            catch( Exception ex )
            {
                ShowError( "缩略图上传失败：" + ex.Message );
                return;
            }

            //展示图上传
            try
            {
                gameRules.ImgRuleUrl = upShowPicture.FilePath;
                if( string.IsNullOrEmpty( gameRules.ImgRuleUrl ) )
                {
                    ShowError("请选择一张PC网站效果图！");
                    return;
                }
            }
            catch( Exception ex )
            {
                ShowError( "游戏截图上传失败：" + ex.Message );
                return;
            }

            //移动端效果图上传
            try
            {
                gameRules.MobileImgUrl = upMobilePicture.FilePath;
            }
            catch(Exception ex)
            {
                ShowError("游戏截图上传失败：" + ex.Message);
                return;
            }

            if( string.IsNullOrEmpty( gameRules.HelpIntro ) ) 
            {
                ShowError( "请输入游戏介绍" );
                return;
            }
            if( string.IsNullOrEmpty( gameRules.HelpRule ) )
            {
                ShowError( "请输入游戏规则介绍" );
                return;
            }
            if( string.IsNullOrEmpty( gameRules.HelpGrade ) )
            {
                ShowError( "请输入游戏等级介绍" );
                return;
            }

            Message msg = new Message();
            if( StrCmd == "add" )
            {
                //判断权限
                AuthUserOperationPermission( Permission.Delete );
                if( FacadeManage.aideNativeWebFacade.JudgeRulesIsExistence( gameRules.KindID ) )
                {
                    ShowError( "该游戏规则已存在" );
                    return;
                }
                msg = FacadeManage.aideNativeWebFacade.InsertGameRules( gameRules );
            }
            else
            {
                //判断权限
                AuthUserOperationPermission( Permission.Edit );
                if( FacadeManage.aideNativeWebFacade.JudgeRulesIsExistence( gameRules.KindID ) && gameRules.KindID != IntParam )
                {
                    ShowError( "该游戏规则已存在" );
                    return;
                }
                msg = FacadeManage.aideNativeWebFacade.UpdateGameRules( gameRules, IntParam );
            }

            if( msg.Success )
            {
                if( StrCmd == "add" )
                {
                    ShowInfo( "规则增加成功", "RulesList.aspx", 1200 );
                }
                else
                {
                    ShowInfo( "规则修改成功", "RulesList.aspx", 1200 );
                }
            }
            else
            {
                ShowError( msg.Content );
            }
        }
        #endregion

    }
}
