using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;
using Game.Entity.Enum;
using Game.Entity.Treasure;
using Game.Web.UI;
using Game.Entity.NativeWeb;
using Game.Facade;

namespace Game.Web.Module.WebManager
{
    public partial class AdsWebHomeListInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                if( IntParam != 0 )
                {
                    BindData();
                }
            }
        }

        //保存
        protected void btnSave_Click( object sender, EventArgs e )
        {
            Ads ads;
            if( IntParam <= 0 )
            {
                ads = new Ads();
            }
            else
            {
                ads = FacadeManage.aideNativeWebFacade.GetAds( IntParam );
            }

            ads.Title = CtrlHelper.GetText( txtTitle );
            ads.SortID = CtrlHelper.GetInt( txtSortID, 0 );
            ads.LinkURL = CtrlHelper.GetText( txtLinkURL );
            ads.Remark = CtrlHelper.GetText( txtRemark );
            ads.Type = 0;

            #region 图片验证
            //缩率图上传
            try
            {
                ads.ResourceURL = upImage.FilePath;
            }
            catch( Exception ex )
            {
                ShowError( "缩率图上传失败：" + ex.Message );
                return;
            }
            #endregion

            if( IntParam <= 0 )
            {
                //判断权限
                AuthUserOperationPermission( Permission.Add );
                FacadeManage.aideNativeWebFacade.InsertAds( ads );
                ShowInfo( "新增成功", "AdsWebHomeList.aspx", 1200 );
            }
            else
            {
                //判断权限
                AuthUserOperationPermission( Permission.Edit );
                FacadeManage.aideNativeWebFacade.UpdateAds( ads );
                ShowInfo( "更新成功", "AdsWebHomeList.aspx", 1200 );
            }
        }

        #endregion

        #region 数据加载

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            Ads ads = FacadeManage.aideNativeWebFacade.GetAds( IntParam );
            if( ads == null )
                return;

            CtrlHelper.SetText( txtTitle, ads.Title );
            CtrlHelper.SetText( txtLinkURL, ads.LinkURL );
            CtrlHelper.SetText( txtSortID, ads.SortID.ToString() );
            CtrlHelper.SetText( txtRemark, ads.Remark );
            if( !string.IsNullOrEmpty( ads.ResourceURL ) )
            {
                upImage.FilePath = ads.ResourceURL;
            }
        }
        #endregion
    }
}
