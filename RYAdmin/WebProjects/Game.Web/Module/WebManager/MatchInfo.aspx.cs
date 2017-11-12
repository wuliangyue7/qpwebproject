using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Utils;
using Game.Web.UI;
using Game.Kernel;
using Game.Entity.GameMatch;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.WebManager
{
    public partial class MatchInfo : AdminPage
    {

        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            if( !Page.IsPostBack )
            {
                BindData();
            }
        }

        protected void btnSave_Click( object sender, EventArgs e )
        {
            ProcessData();
        }
        #endregion

        #region 数据加载

        private void BindData()
        {
            if( IntParam <= 0 )
            {
                return;
            }

            //获取问题信息
            //string commandText = "SELECT * FROM MatchPublic Where MatchID =" + IntParam;
            //MatchPublic matchInfo = FacadeManage.aideGameMatchFacade.GetEntity<MatchPublic>( commandText );
            //if( matchInfo == null )
            //{
            //    ShowError( "比赛信息不存在" );
            //    Redirect( "MatchList.aspx" );
            //    return;
            //}
            //CtrlHelper.SetText( txtMatchTitle, matchInfo.MatchName );
            //CtrlHelper.SetText( txtMatchSummary, matchInfo.MatchSummary );
            //CtrlHelper.SetText( txtMatchDate, matchInfo.MatchDate );
            //rbtnNullity.SelectedValue = matchInfo.Nullity.ToString().Trim();
            //rblMatchStatus.SelectedValue = matchInfo.MatchStatus.ToString().Trim();
            //CtrlHelper.SetText( txtContent, matchInfo.MatchContent );
            //upThumbnail.FilePath = matchInfo.ThumbnailUrl;
            //upShowPicture.FilePath = matchInfo.ShowPictureUrl;
            //rblIsRecommend.SelectedValue = matchInfo.IsRecommend ? "1" : "0";
        }
        #endregion

        #region 处理方法

        private void ProcessData()
        {
            //Game.Entity.GameMatch.MatchInfo matchInfo = new Game.Entity.GameMatch.MatchInfo();
            //matchInfo.MatchName = CtrlHelper.GetText( txtMatchTitle );
            //matchInfo.MatchSummary = CtrlHelper.GetText( txtMatchSummary );
            //matchInfo.MatchDate = CtrlHelper.GetText( txtMatchDate );
            //matchInfo.Nullity = byte.Parse( rbtnNullity.SelectedValue );
            //matchInfo.MatchStatus = byte.Parse( rblMatchStatus.SelectedValue );
            //matchInfo.MatchContent = txtContent.Text;
            //matchInfo.IsRecommend = rblIsRecommend.SelectedValue == "1" ? true : false;

            ////缩率图上传
            //if( string.IsNullOrEmpty( upThumbnail.FilePath ) ) 
            //{
            //    ShowError( "请上传缩略图" );
            //    return;
            //}
            //try
            //{
            //    matchInfo.ThumbnailUrl = upThumbnail.FilePath;
            //}
            //catch( Exception ex )
            //{
            //    ShowError( "缩率图上传失败：" + ex.Message );
            //    return;
            //}

            ////展示图上传
            //if( string.IsNullOrEmpty( upShowPicture.FilePath ) ) 
            //{
            //    ShowError( "请上传展示图" );
            //    return;
            //}
            //try
            //{
            //    matchInfo.ShowPictureUrl = upShowPicture.FilePath;
            //}
            //catch( Exception ex )
            //{
            //    ShowError( "展示图上传失败：" + ex.Message );
            //    return;
            //}

            //if( IntParam <= 0 )
            //{
            //    //判断权限
            //    AuthUserOperationPermission( Permission.Add );
            //    try
            //    {
            //        FacadeManage.aideGameMatchFacade.InsertMatchInfo( matchInfo );
            //        ShowInfo( "增加成功", "MatchList.aspx", 1200 );
            //        BindData();
            //    }
            //    catch
            //    {
            //        ShowError( "增加失败" );
            //    }
            //}
            //else
            //{
            //    //判断权限
            //    AuthUserOperationPermission( Permission.Edit );
            //    try
            //    {
            //        matchInfo.MatchID = IntParam;
            //        FacadeManage.aideGameMatchFacade.UpdateMatchInfo( matchInfo );
            //        ShowInfo( "修改成功", "MatchList.aspx", 1200 );
            //    }
            //    catch
            //    {
            //        ShowError( "修改失败" );
            //    }
            //}
        }
        #endregion
    }
}
